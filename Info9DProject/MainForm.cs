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
        private const string costTableName = "BIM_9DCostInfo";
        private const string energyTableName = "BIM_9DEnergyInfo";
        private const string facilitiesTableName = "BIM_9DFacilitiesInfo";

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
                string costDescribeText = "";
                if (TBDescribeCost.Text.Trim() != string.Empty)
                    costDescribeText = TBDescribeCost.Text;
                string strCostSqlText = string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}')", costTableName, strId, strName, costDescribeText);
                string energyDescribeText = "";
                if (TBDescribeEnergy.Text.Trim() != string.Empty)
                    energyDescribeText = TBDescribeEnergy.Text;
                string strEnergySqlText = string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}')", energyTableName, strId, strName, energyDescribeText);
                string facilitiesDescribeText = "";
                if (TBDescribeFacilities.Text.Trim() != string.Empty)
                    facilitiesDescribeText = TBDescribeFacilities.Text;
                string strFacilitiesSqlText = string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}')", facilitiesTableName, strId, strName, facilitiesDescribeText);
                SqlCommand cmdTotalSqlText = new SqlCommand(strTotalSqlText, con);
                SqlCommand cmdQuality = new SqlCommand(strQualitySqlText, con);
                SqlCommand cmdSafety = new SqlCommand(strSafetySqlText, con);
                SqlCommand cmdCost = new SqlCommand(strCostSqlText, con);
                SqlCommand cmdEnergy = new SqlCommand(strEnergySqlText, con);
                SqlCommand cmdFacilities = new SqlCommand(strFacilitiesSqlText, con);
                try
                {
                    cmdTotalSqlText.ExecuteNonQuery();
                    cmdQuality.ExecuteNonQuery();
                    cmdSafety.ExecuteNonQuery();
                    cmdCost.ExecuteNonQuery();
                    cmdEnergy.ExecuteNonQuery();
                    cmdFacilities.ExecuteNonQuery();
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
            string strCostSqlText = string.Format("SELECT Describe FROM {0} WHERE ID={1}", costTableName, sId);
            string strEnergySqlText = string.Format("SELECT Describe FROM {0} WHERE ID={1}", energyTableName, sId);
            string strFcilitiesSqlText = string.Format("SELECT Describe FROM {0} WHERE ID={1}", facilitiesTableName, sId);
            SqlCommand cmdQuality = new SqlCommand(strQualitySqlText, con);
            SqlCommand cmdSafety = new SqlCommand(strSafetySqlText, con);
            SqlCommand cmdCost = new SqlCommand(strCostSqlText, con);
            SqlCommand cmdEnergy = new SqlCommand(strEnergySqlText, con);
            SqlCommand cmdFacilities = new SqlCommand(strFcilitiesSqlText, con);
            SqlDataReader reader;
            string strDescribeQuality = "";
            string strDescribeSafety = "";
            string strDescribeCost = "";
            string strDescribeEnergy = "";
            string strDescribeFacilities = "";
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

                reader = cmdCost.ExecuteReader();
                reader.Read();
                if (!reader.IsDBNull(reader.GetOrdinal("Describe")))//db中有数据
                    strDescribeCost = reader.GetString(reader.GetOrdinal("Describe"));
                reader.Close();

                reader = cmdEnergy.ExecuteReader();
                reader.Read();
                if (!reader.IsDBNull(reader.GetOrdinal("Describe")))//db中有数据
                    strDescribeEnergy = reader.GetString(reader.GetOrdinal("Describe"));
                reader.Close();

                reader = cmdFacilities.ExecuteReader();
                reader.Read();
                if (!reader.IsDBNull(reader.GetOrdinal("Describe")))//db中有数据
                    strDescribeFacilities = reader.GetString(reader.GetOrdinal("Describe"));
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库数据加载错误信息:\n" + ex.Message);
                return;
            }

            TBDescribeQuality.Text = strDescribeQuality;
            TBDescribeSafety.Text = strDescribeSafety;
            TBDescribeCost.Text = strDescribeCost;
            TBDescribeEnergy.Text = strDescribeEnergy;
            TBDescribeFacilities.Text = strDescribeFacilities;
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
                if(items.First!=null)//不为空才添加
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
            {
                MessageBox.Show("该构件未查询到信息!", "信息");
                //清空TabControl面板信息
                TBDescribeQuality.Text = "";
                TBDescribeSafety.Text = "";
            }
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
            string sQualityDescribeText = TBDescribeQuality.Text.Trim();//质量
            string sSafetyDescribeText = TBDescribeSafety.Text.Trim();//安全
            string sCostDescribeText = TBDescribeCost.Text.Trim();
            string sEnergyDescribeText = TBDescribeEnergy.Text.Trim();//能耗
            string sFacilitiesDescribeText = TBDescribeFacilities.Text.Trim();
            SqlConnection con = new SqlConnection(sqlConnectInfo);
            con.Open();
            string strSqlTextQ = string.Format("UPDATE {0} SET Describe='{1}' WHERE ID='{2}'",qualityTableName, sQualityDescribeText,sId);
            string strSqlTextS = string.Format("UPDATE {0} SET Describe='{1}' WHERE ID='{2}'", safetyTableName, sSafetyDescribeText, sId);
            string strSqlTextC = string.Format("UPDATE {0} SET Describe='{1}' WHERE ID='{2}'", costTableName, sCostDescribeText, sId);
            string strSqlTextE = string.Format("UPDATE {0} SET Describe='{1}' WHERE ID='{2}'", energyTableName, sEnergyDescribeText, sId);
            string strSqlTextF = string.Format("UPDATE {0} SET Describe='{1}' WHERE ID='{2}'", facilitiesTableName, sFacilitiesDescribeText, sId);
            SqlCommand sqlCommandQ = new SqlCommand(strSqlTextQ, con);
            SqlCommand sqlCommandS = new SqlCommand(strSqlTextS, con);
            SqlCommand sqlCommandC = new SqlCommand(strSqlTextC, con);
            SqlCommand sqlCommandE = new SqlCommand(strSqlTextE, con);
            SqlCommand sqlCommandF = new SqlCommand(strSqlTextF, con);
            try
            {
                sqlCommandQ.ExecuteNonQuery();
                sqlCommandS.ExecuteNonQuery();
                sqlCommandC.ExecuteNonQuery();
                sqlCommandE.ExecuteNonQuery();
                sqlCommandF.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改发生错误：\n" + ex.Message);
                con.Close();
            }
            MessageBox.Show("修改成功！", "提示");
            con.Close();
        }

        private void BUTDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = DGVTotal.SelectedRows;
            if (selectedRows.Count <= 0)
            {
                MessageBox.Show("未选中任何行数据删除", "提示");
                return;
            }
            DialogResult result = MessageBox.Show("该删除操作会删除该构件下所有的记录信息且不可恢复，确认执行？", "删除",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(sqlConnectInfo);
                con.Open();
                for (int i = 0; i < selectedRows.Count; i++)
                {
                    try
                    {
                        string id = selectedRows[i].Cells[0].Value.ToString();
                        string cmdTextM = string.Format("DELETE FROM {0} WHERE ID='{1}'", mainTableName,id);
                        string cmdTextQ = string.Format("DELETE FROM {0} WHERE ID='{1}'", qualityTableName, id);
                        string cmdTextS = string.Format("DELETE FROM {0} WHERE ID='{1}'", safetyTableName, id);
                        string cmdTextC = string.Format("DELETE FROM {0} WHERE ID='{1}'", costTableName, id);
                        string cmdTextE = string.Format("DELETE FROM {0} WHERE ID='{1}'", energyTableName, id);
                        string cmdTextF = string.Format("DELETE FROM {0} WHERE ID='{1}'", facilitiesTableName, id);
                        SqlCommand cmdM = new SqlCommand(cmdTextM, con);
                        SqlCommand cmdQ = new SqlCommand(cmdTextQ, con);
                        SqlCommand cmdS = new SqlCommand(cmdTextS, con);
                        SqlCommand cmdC = new SqlCommand(cmdTextC, con);
                        SqlCommand cmdE = new SqlCommand(cmdTextE, con);
                        SqlCommand cmdF = new SqlCommand(cmdTextF, con);
                        cmdM.ExecuteNonQuery();
                        cmdQ.ExecuteNonQuery();
                        cmdS.ExecuteNonQuery();
                        cmdC.ExecuteNonQuery();
                        cmdE.ExecuteNonQuery();
                        cmdF.ExecuteNonQuery();
                        DGVTotal.Rows.Remove(selectedRows[i]);//在grid中删除
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除发生错误" + ex.Message, "提示");
                    }
                }
                con.Close();
            }
        }
    }
}
