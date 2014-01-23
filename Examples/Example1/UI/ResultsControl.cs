using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlugInsInterfaces.ResultTypes;
using PlugInsInterfaces.DataTypes;

namespace Example1.UI
{
    public partial class ResultsControl : UserControl
    {
        const int ghgGroupID = 1;

        public ResultsControl()
        {
            InitializeComponent();
        }

        public int SetResults(string name, IResults results, int productID)
        {
            if (results == null)
                return -1;

            IData data = ResultsAccess.controler.CurrentProject.Data;

            this.labelName.Text = name;

            if (data.Resources.KeyExists(productID))
                this.labelProduct.Text = data.Resources.ValueForKey(productID).Name;
            else
                this.labelProduct.Text = "Product ID not found: " + productID;

            Dictionary<int, IValue> emissionGroups = results.LifeCycleEmissionsGroups(ResultsAccess.controler.CurrentProject.Data);
            if (emissionGroups != null && emissionGroups.ContainsKey(ghgGroupID))
            {
                IValue quantity = emissionGroups[ghgGroupID];
                this.labelGHGs.Text = ResultsAccess.controler.FormatValue(quantity.Value, quantity.Unit, 0)
                    + " or " + ResultsAccess.controler.FormatValue(quantity.Value, quantity.Unit, 1, false);
            }

            this.labelFunctionalUnit.Text = "Per " + results.FunctionalUnit;

            return 0;
        }
    }
}
