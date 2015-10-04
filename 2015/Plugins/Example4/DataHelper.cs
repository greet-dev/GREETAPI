using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Greet.DataStructureV4.Interfaces;
using Greet.Model.Interfaces;
using System.Windows.Forms;

namespace Greet.Plugins.Example4
{
    /// <summary>
    /// This plugin demonstrates some of the uses of the DataHelper, then some advanced features to create a pathway
    /// </summary>
    public class DataHelper : APlugin
    {
        IGREETController _controler;

        public override bool InitializePlugin(IGREETController controler)
        {
            _controler = controler;
            return true;
        }

        public override string GetPluginName
        {
            get { return "Exemple4: DataHelper demo"; }
        }

        public override string GetPluginDescription
        {
            get { return "Demonstrates how to programatically create a new pathway and edit some of the data"; }
        }

        public override string GetPluginVersion
        {
            get { return "1.0.0.0"; }
        }

        public override System.Drawing.Image GetPluginIcon
        {
            get { return null; }
        }

        public override System.Windows.Forms.ToolStripMenuItem[] GetMainMenuItems()
        {
            ToolStripMenuItem[] items = new ToolStripMenuItem[1];
            ToolStripMenuItem showForm = new ToolStripMenuItem();
            showForm.Text = "DataHelper Examples";
            showForm.Click += (s, e) =>
            {
                DataHelperForm form = new DataHelperForm(_controler.CurrentProject.Data.Helper, _controler);
                form.Show();
            };
            items[0] = showForm;
            return items;
        }
    }
}
