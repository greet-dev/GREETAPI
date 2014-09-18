using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlugInsInterfaces.PluginTypes;
using PlugInsInterfaces;
using System.Reflection;
using System.Windows.Forms;
using Example1.UI;

namespace Example1
{
    /// <summary>
    /// This plugin example shows a very simple way to grab results from the GREET calculated pathways
    /// </summary>
    public class ResultsAccess : APlugin
    {
        /// <summary>
        /// Controller that allows access to the data and functions
        /// </summary>
        public static IGREETController controler;

        /// <summary>
        /// A array of the menu items for this plugin
        /// </summary>
        ToolStripMenuItem[] items = new ToolStripMenuItem[1];

        #region APlugin
        /// <summary>
        /// Initialize the plugin, called once after the DLL is loaded into GREET
        /// </summary>
        /// <param name="controler"></param>
        /// <returns></returns>
        public override bool InitializePlugin(IGREETController controler)
        {
            //init the controller that is used to send action and data requests to GREET
            ResultsAccess.controler = controler;

            //init menu items collection for this example
            ToolStripMenuItem ex = new ToolStripMenuItem("Example 1");
            ex.Click += (s, e) =>
            {
                ResultsSelectionForm form = new ResultsSelectionForm();
                form.InitializeControls();
                form.Show();
            };
            this.items[0] = ex;

            return true;
        }

        public override string GetPluginName
        {
            get { return "API Example1 : Retriving Results"; }
        }

        public override string GetPluginDescription
        {
            get { return "Allows the user to select pathways and mixes, displays the results of the selected item"; }
        }

        public override string GetPluginVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public override System.Drawing.Image GetPluginIcon
        {
            get { return null; }
        }
        #endregion

        #region menu items

        /// <summary>
        /// Called when the GREET main form is initializing, returns the menu items for this plugin
        /// </summary>
        /// <returns></returns>
        public override System.Windows.Forms.ToolStripMenuItem[] GetMainMenuItems()
        {
            return this.items;
        }

        #endregion
    }
}
