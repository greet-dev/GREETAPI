using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Greet.DataStructureV4.Interfaces;

namespace Greet.Plugins.Example3.UI
{
    
    public partial class Form1 : Form
    {
        const int CRUDE_PATHWAY_ID = 34;
        const int COAL_PATHWAY_ID = 17;
        const int LSDIESEL_PATHWAY_ID = 61;

        public Form1()
        {
            InitializeComponent();
            pullGREETParameterValues();
        }
        public void InitializeControls()
        {
        }
        public void pullGREETParameterValues()
        {
            //You need to create these dictionaries in order to pull out the values you want
            IGDataDictionary<int, IResource> resources = ResultsAccess.controler.CurrentProject.Data.Resources;
            IGDataDictionary<int, IPathway> pathways = ResultsAccess.controler.CurrentProject.Data.Pathways;

            //The number value that goes into ValueForKey is the ID number for the pathway you are looking to use 
            IPathway myPathway = pathways.ValueForKey(CRUDE_PATHWAY_ID);
            // Grab the int id for the resource
            int productID = myPathway.MainOutputResourceID;
            IResource resource = resources.ValueForKey(productID);
            label4.Text = resource.Density.GreetValue.ToString();

            //The number value that goes into ValueForKey is the ID number for the pathway you are looking to use 
            myPathway = pathways.ValueForKey(COAL_PATHWAY_ID);
            // Grab the int id for the resource
            productID = myPathway.MainOutputResourceID;
            resource = resources.ValueForKey(productID);
            label5.Text = resource.LowerHeatingValue.GreetValue.ToString();

            //The number value that goes into ValueForKey is the ID number for the pathway you are looking to use 
            myPathway = pathways.ValueForKey(LSDIESEL_PATHWAY_ID);
            // Grab the int id for the resource
            productID = myPathway.MainOutputResourceID;
            resource = resources.ValueForKey(productID);
            label7.Text = resource.SulfurRatio.GreetValue.ToString();
        }

        //This function pulls in the data from GREET that can be classified as user input, meaning it was a custom user value
        //In order to change the values in GREET you go into the data editor tab, and select either the pathway or resource
        //You want to change (in this case we are only looking at resources) and then change the values and hit apply
        //After you apply the changes, you can hit the pull user values button and it will allow you to update values in real time
        public void pullUserParameterValues()
        {
            IGDataDictionary<int, IResource> resources = ResultsAccess.controler.CurrentProject.Data.Resources;
            IGDataDictionary<int, IPathway> pathways = ResultsAccess.controler.CurrentProject.Data.Pathways;

            //The number value that goes into ValueForKey is the ID number for the pathway you are looking to use 
            IPathway myPathway = pathways.ValueForKey(CRUDE_PATHWAY_ID);
            // Grab the int id for the resource
            int productID = myPathway.MainOutputResourceID;
            IResource resource = resources.ValueForKey(productID);
            label11.Text = resource.Density.UserValue.ToString();

            //The number value that goes into ValueForKey is the ID number for the pathway you are looking to use 
            myPathway = pathways.ValueForKey(COAL_PATHWAY_ID);
            // Grab the int id for the resource
            productID = myPathway.MainOutputResourceID;
            resource = resources.ValueForKey(productID);
            label10.Text = resource.LowerHeatingValue.UserValue.ToString();

            //The number value that goes into ValueForKey is the ID number for the pathway you are looking to use 
            myPathway = pathways.ValueForKey(LSDIESEL_PATHWAY_ID);
            // Grab the int id for the resource
            productID = myPathway.MainOutputResourceID;
            resource = resources.ValueForKey(productID);
            label9.Text = resource.SulfurRatio.UserValue.ToString();
        }

        //You click this button, and you pull forth all of the user entered data in GREET
        private void button2_Click(object sender, EventArgs e)
        {
            pullUserParameterValues();
        }
    }
}
