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
        /// <summary>
        /// Hard-coded "Greenhouse Gases" group ID
        /// </summary>
        const int ghgGroupID = 1;

        public ResultsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when a pathway is selected in order to represent the life cycle results
        /// for the product produced by this pathway and defined as it's main output (which is
        /// equivalent to the main output of the last process in the pathway)
        /// </summary>
        /// <param name="name">Name of the pathway, will simply be displayed as is</param>
        /// <param name="results">Result object from the pathway for the desired productID</param>
        /// <param name="productID">ProductID represents the ID of a resource in the IData.Resources</param>
        /// <returns>Returns 0 if succeed</returns>
        public int SetResults(string name, IResults results, int productID)
        {
            //Check that the resuls object is non null
            if (results == null)
                return -1;
            
            //Set the label text as the name of the pathway
            this.labelName.Text = name;

            //Get an instance of the data object that we are going to use to look for a Resource
            IData data = ResultsAccess.controler.CurrentProject.Data;

            //Set the label text as the Resource.Name for the produced product of the pathway
            //i.e. If the pathway is producing Gasoline, we're going to get the Resource object using the value productID 
            //which should correspond to the Gasoline resource object in the database
            if (data.Resources.KeyExists(productID))
                this.labelProduct.Text = data.Resources.ValueForKey(productID).Name;
            else
                this.labelProduct.Text = "Product ID not found: " + productID;

            //Dictionary of emission group, indexed by IEmissionGroup.Id
            //Emission groups may be created by the user and contains a couple of default group such as "Greenhouse Gases", "Criteria Pollutants"
            //In this case we are going to use the GHGs group and display the life cycle results for this pathway
            Dictionary<int, IValue> emissionGroups = results.LifeCycleEmissionsGroups(ResultsAccess.controler.CurrentProject.Data);
            if (emissionGroups != null && emissionGroups.ContainsKey(ghgGroupID))
            {
                IValue quantity = emissionGroups[ghgGroupID];
                //Format the value nicely using the quantity and the unit as well as the preferences defined by the user in the main UI GREET preferences
                this.labelGHGs.Text = ResultsAccess.controler.FormatValue(quantity.Value, quantity.Unit, 0)
                    + " or " + ResultsAccess.controler.FormatValue(quantity.Value, quantity.Unit, 1, false);
            }

            //Displays the functional unit for this results, very important in order to know if we are looking at results
            //per joule of product, or per cubic meters of product, or per kilograms of prododuct
            this.labelFunctionalUnit.Text = "Per " + results.FunctionalUnit;
            //If the user wants to see results in a different functional unit, the IValue quantity must be converted to the desired functional unit

            return 0;
        }
    }
}
