using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using Autodesk.Navisworks.Api;

namespace Info9DProject
{
    public partial class MainForm : Form
    {
        private const string sqlConnectInfo = "data source=192.168.0.69;Initial Catalog=ZYCT_Project;User ID=sa;PWD=sasa;";
        private const string mainTableName = "BIM_9DMain";
        private const string qualityTableName = "BIM_9DQualityInfo";
        private const string safetyTableName = "BIM_9DSafetyInfo";
        public MainForm()
        {
            InitializeComponent();
        }

        private void BUTShow_Click(object sender, EventArgs e)
        {
            DGVTotal.Rows.Clear();
            /**操作数据库**/
            SqlConnection con = new SqlConnection(sqlConnectInfo);
            con.Open();
            string sqlCmd = string.Format("SELECT ID,Name FROM {0}",mainTableName);
            SqlCommand cmd = new SqlCommand(sqlCmd, con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            int a = DGVTotal.Rows.Count;
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                DGVTotal.Rows.Add();
                for (int j = 0; j < dataSet.Tables[0].Columns.Count; j++)
                {
                    DGVTotal.Rows[i].Cells[j].Value = dataSet.Tables[0].Rows[i][j];
                }
            }
            con.Close();
        }

        //查询数据库中是否存在该ID的记录，有的话选中当前窗口tab页的该行
        private bool HasRecordInSqlTable(string strId)
        {
            //入库查询
            SqlConnection con = new SqlConnection(sqlConnectInfo);
            con.Open();
            string cmdText = string.Format("SELECT * FROM {0} WHERE ID='{1}'",mainTableName,strId);
            SqlCommand sqlCommand = new SqlCommand(cmdText, con);

            if (sqlCommand.ExecuteScalar() == null)//查询数据库中是否有此记录
                return false;
            else
            {
                for (int i = 0; i < DGVTotal.Rows.Count; i++)
                {
                    if (DGVTotal.Rows[i].Cells[0].Value.ToString() == strId)
                    {
                        DGVTotal.Rows[i].Selected = true;//设置改行选中
                        DGVTotal.CurrentCell = DGVTotal.Rows[i].Cells[0];//焦点到该行                       
                    }
                }
            }                             
                
            con.Close();
            return true;
        }

        private void BUTAdd_Click(object sender, EventArgs e)
        {
            //规定只能选择单个构件添加记录
            Document doc = Autodesk.Navisworks.Api.Application.ActiveDocument;
            Selection selection = new Selection();
            selection = doc.CurrentSelection.ToSelection();
            ModelItemCollection modelItems = selection.ExplicitSelection;

            if (modelItems.Count != 1)
            {
                MessageBox.Show("请只选择一个图元新增记录！");
                return;
            }
            ModelItem modelItem = modelItems.First;
            int nId = -1;
            string strName = "";
            try
            {
                nId = modelItem.PropertyCategories.FindPropertyByDisplayName("元素", "Id").Value.ToInt32();
                strName = modelItem.PropertyCategories.FindPropertyByDisplayName("元素", "名称").Value.ToDisplayString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询错误" + ex.Message);
                return;
            }
            string strId = nId.ToString();
            //查询是否有记录，有的话聚焦并选择
            if (HasRecordInSqlTable(strId))
            {
                MessageBox.Show("已有对应记录，显示在表中！", "提示");
            }
            else//添加记录
            {
                SqlConnection con = new SqlConnection(sqlConnectInfo);
                con.Open();
                string strTotalSqlText = string.Format("INSERT INTO {0} VALUES ('{1}','{2}')", mainTableName,strId, strName);
                string qualityDescribeText = "";
                if (TBDescribeQuality.Text.Trim() != string.Empty)
                    qualityDescribeText = TBDescribeQuality.Text;
                string strQualitySqlText = string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}')", qualityTableName, strId, strName, qualityDescribeText);
                string safetyDescribeText = "";
                if (TBDescribeSafety.Text.Trim() != string.Empty)
                    safetyDescribeText = TBDescribeSafety.Text;
                string strSafetySqlText = string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}')", safetyTableName, strId, strName, safetyDescribeText);
                SqlCommand cmdTotalSqlText = new SqlCommand(strTotalSqlText, con);
                SqlCommand cmdQuality = new SqlCommand(strQualitySqlText, con);
                SqlCommand cmdSafety = new SqlCommand(strSafetySqlText, con);
                try
                {
                    cmdTotalSqlText.ExecuteNonQuery();
                    cmdQuality.ExecuteNonQuery();
                    cmdSafety.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("提交错误：" + ex.Message);
                    return;
                }
                
                BUTShow_Click(sender, e);//刷新总表
                con.Close();
            }
        }

        private void RefreshTabControl()//刷新tabControl的内容
        {
            try
            {
                if (DGVTotal.SelectedRows.Count <= 0)
                {
                    MessageBox.Show("请确认是否选中行记录!");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("请确认是否选中行记录!");
                return;
            }
            DataGridViewRow selectedRow = DGVTotal.SelectedRows[0];         
            string sId = selectedRow.Cells[0].Value.ToString();
            SqlConnection con = new SqlConnection(sqlConnectInfo);
            con.Open();
            string strQualitySqlText = string.Format("SELECT Describe FROM {0} WHERE ID = {1}", qualityTableName, sId);
            string strSafetySqlText = string.Format("SELECT Describe FROM {0} WHERE ID={1}", safetyTableName, sId);
            SqlCommand cmdQuality = new SqlCommand(strQualitySqlText, con);
            SqlCommand cmdSafety = new SqlCommand(strSafetySqlText, con);
            SqlDataReader reader;
            string strDescribeQuality = "";
            string strDescribeSafety = "";
            try
            {
                reader = cmdQuality.ExecuteReader();
                reader.Read();//只读唯一的一条记录
                if (!reader.IsDBNull(reader.GetOrdinal("Describe")))//db中有数据
                    strDescribeQuality = reader.GetString(reader.GetOrdinal("Describe"));
                reader.Close();//如需要开启另一个reader，必须关闭之前的reader
                reader = cmdSafety.ExecuteReader();
                reader.Read();
                if (!reader.IsDBNull(reader.GetOrdinal("Describe")))//db中有数据
                    strDescribeSafety = reader.GetString(reader.GetOrdinal("Describe"));
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库数据加载错误信息:\n" + ex.Message);
                return;
            }

            TBDescribeQuality.Text = strDescribeQuality;
            TBDescribeSafety.Text = strDescribeSafety;
            con.Close();
        }

        private void DGVTotal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RefreshTabControl();//刷新tabControl的内容
        }

        private void BUTFocus_Click(object sender, EventArgs e)
        {
            Document Doc = Autodesk.Navisworks.Api.Application.ActiveDocument;                        
            List<string> listId = new List<string>();
            if (DGVTotal.SelectedRows.Count == 0)
            {
                MessageBox.Show("未选中行！");
                return;
            }
            for (int i = 0; i < DGVTotal.SelectedRows.Count; i++)
            {
                listId.Add(DGVTotal.SelectedRows[i].Cells[0].Value.ToString());
            }

            ModelItemCollection itemsCol = new ModelItemCollection();
            //元素ID是int32类型，所以Displaystring找不到
            for (int i = 0; i < DGVTotal.SelectedRows.Count; i++)
            {
                Search search = new Search();
                search.Selection.SelectAll();
                search.SearchConditions.Add(SearchCondition.HasPropertyByDisplayName("元素", "Id").EqualValue(VariantData.FromInt32(Int32.Parse(listId[i]))));
                ModelItemCollection items = search.FindAll(Doc, true);
                itemsCol.Add(items.First);
            }
            
                     
            Doc.CurrentSelection.Clear();
            Doc.CurrentSelection.CopyFrom(itemsCol);
            if (itemsCol.Count > 0)
            {
                MessageBox.Show("已在视图中选中该元素！", "提示");
            }
            
        }

        private void BUTQuery_Click(object sender, EventArgs e)
        {
            Document Doc = Autodesk.Navisworks.Api.Application.ActiveDocument;
            ModelItemCollection modelItems = Doc.CurrentSelection.SelectedItems;
            if (modelItems.Count != 1)
            {
                MessageBox.Show("请选择一个构件查询", "提示");
                return;
            }
            ModelItem modelItem = modelItems.First;
            int nId = -1;
            try
            {
                nId = modelItem.PropertyCategories.FindPropertyByDisplayName("元素", "Id").Value.ToInt32();
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询错误" + ex.Message);
                return;
            }
            string strId = nId.ToString();
            if (!HasRecordInSqlTable(strId))//查询并聚焦
                MessageBox.Show("该构件未查询到信息!", "信息");
            else
                RefreshTabControl();
        }

        private void BUTChange_Click(object sender, EventArgs e)
        {
            int nSelectedRowsCount = -1;
            nSelectedRowsCount = DGVTotal.SelectedRows.Count;
            if (nSelectedRowsCount != 1)
            {
                MessageBox.Show("只允许单行修改信息！","提示");
                return;
            }
            string sId = DGVTotal.SelectedRows[0].Cells[0].Value.ToString();//取到ID
            string sQualityDescribeText = TBDescribeQuality.Text.Trim();
            string sSafetyDescribeText = TBDescribeSafety.Text.Trim();
            SqlConnection con = new SqlConnection(sqlConnectInfo);
            con.Open();
            string strSqlTextQ = string.Format("UPDATE {0} SET Describe='{1}' WHERE ID='{2}'",qualityTableName, sQualityDescribeText,sId);
            string strSqlTextS = string.Format("UPDATE {0} SET Describe='{1}' WHERE ID='{2}'", safetyTableName, sSafetyDescribeText, sId);
            SqlCommand sqlCommandQ = new SqlCommand(strSqlTextQ, con);
            SqlCommand sqlCommandS = new SqlCommand(strSqlTextS, con);
            try
            {
                sqlCommandQ.ExecuteNonQuery();
                sqlCommandS.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改发生错误：\n" + ex.Message);
                con.Close();
            }
            MessageBox.Show("修改成功！", "提示");
            con.Close();
        }
    }
}
