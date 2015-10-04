using System.Reflection;
using System.Windows.Forms;
using Example2.UI;
using Greet.DataStructureV4.Interfaces;
using Greet.Model.Interfaces;

namespace Example2
{
    /// <summary>
    /// Lets the user navigate though all the parameter and modify their values
    /// The the user is free to run the simulations again by clicking a button
    /// </summary>
    internal class ParametersExample : APlugin
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
            ParametersExample.controler = controler;

            //init menu items collection for this example
            ToolStripMenuItem ex = new ToolStripMenuItem("Example 2");
            ex.Click += (s, e) =>
            {
                ParameterExplorer form = new ParameterExplorer();
                form.InitializeControls();
                form.Show();
            };
            this.items[0] = ex;

            return true;
        }

        public override string GetPluginName
        {
            get { return "API Example 2 : Editing Parameters"; }
        }

        public override string GetPluginDescription
        {
            get { return "Enables the user to select and modify parameters then run the simulations"; }
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
