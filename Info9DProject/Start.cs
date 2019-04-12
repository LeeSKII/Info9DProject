using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;

namespace Info9DProject
{
    [PluginAttribute("Info9D", "CIEPMIP", DisplayName = "9D信息", ToolTip = "BIM模型9D信息管理")]
    public class Start:AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();

            return 0;
        }
    }
}
