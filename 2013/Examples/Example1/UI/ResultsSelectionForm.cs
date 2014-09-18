using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlugInsInterfaces.DataTypes.Resource;
using PlugInsInterfaces.DataTypes;
using PlugInsInterfaces.DataTypes.Pathway;
using PlugInsInterfaces.DataTypes.Mix;
using PlugInsInterfaces.ResultTypes;

namespace Example1.UI
{
    /// <summary>
    /// A simple form that shows a list of available pathways and mixes
    /// and display the associated GHGs results when an element is selected
    /// </summary>
    public partial class ResultsSelectionForm : Form
    {
        public ResultsSelectionForm()
        {
            InitializeComponent();

            this.treeView1.MouseDown += new MouseEventHandler(treeView1_MouseDown);
        }

        /// <summary>
        /// Creates the list of available pathways and mixes
        /// </summary>
        public void InitializeControls()
        {
            //Gets the dictionary of IResource object indexed by IResource.Id
            IGDataDictionary<int, IResource> resources = ResultsAccess.controler.CurrentProject.Data.Resources;
            //Gets the dictionary of IPathways object indexed by IPathway.Id
            IGDataDictionary<int, IPathway> pathways = ResultsAccess.controler.CurrentProject.Data.Pathways;
            //Gets the dictionary of IMixes object indexed by IMid.Id
            IGDataDictionary<int, IMix> mixes = ResultsAccess.controler.CurrentProject.Data.Mixes;

            this.treeView1.Nodes.Clear();

            //Adds pathways and mixes to the list so the user can select one
            foreach (IResource resource in resources.AllValues.OrderBy(item => item.Name))
            {
                TreeNode resourceTreeNode = new TreeNode(resource.Name);
                resourceTreeNode.Tag = resource;

                foreach(IPathway pathway in pathways.AllValues.Where(item => item.MainOutputResourceID == resource.Id))
                {
                    TreeNode pathwayNode = new TreeNode("Pathway: "+ pathway.Name);
                    pathwayNode.Tag = pathway;
                    resourceTreeNode.Nodes.Add(pathwayNode);
                }

                foreach (IMix mix in mixes.AllValues.Where(item => item.MainOutputResourceID == resource.Id))
                {
                    TreeNode mixNode = new TreeNode("Mix: " + mix.Name);
                    mixNode.Tag = mix;
                    resourceTreeNode.Nodes.Add(mixNode);
                }

                if(resourceTreeNode.Nodes.Count >0)
                    this.treeView1.Nodes.Add(resourceTreeNode);
            }
        }

        /// <summary>
        /// Invoked when the user click on an item in the tree list view
        /// Retrieve the IPathway or IMix object stored in the tag and sends it to the ResultsControl 
        /// for displaying the results associated with that pathway or mix main output
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            treeView1.SelectedNode = treeView1.GetNodeAt(e.Location);

            if (this.treeView1.SelectedNode != null)
            {
                Object tag = this.treeView1.SelectedNode.Tag;
                IResults result = null;
                int productID = -1;
                string name = "";

                //if the retrieved object is a pathway
                if (tag is IPathway)
                {
                    IPathway path = tag as IPathway;
                    //We ask the pathway what is the product defined as the main product for this pathway
                    //then store an integer that corresponds to an IResource.ID
                    productID = path.MainOutputResourceID;
                    //We use the ID of the Resource that corresponds to the main output of the pathway to get the correct results
                    Dictionary<int, IResults> availableResults = path.GetUpstreamResults(ResultsAccess.controler.CurrentProject.Data);
                    if (availableResults.ContainsKey(productID))
                        result = availableResults[productID];
                    //We set the string variable as the name of the pathway
                    name = path.Name;
                }
                //if the retrieved object is a pathway
                else if (tag is IMix)
                {
                    IMix mix = tag as IMix;
                    //We ask the mix what is the product defined as the main product for this mix
                    //then store an integer that corresponds to an IResource.ID
                    productID = mix.MainOutputResourceID;
                    //We use the ID of the Resource that corresponds to the main output of the pathway to get the correct results
                    Dictionary<int, IResults> availableResults = mix.GetUpstreamResults(ResultsAccess.controler.CurrentProject.Data);
                    if (availableResults.ContainsKey(productID))
                        result = availableResults[productID];
                    //We set the string variable as the name of the pathway
                    name = mix.Name;
                }

                //if we found a pathway or a mix and we have all the necessary parameters 
                //we Invoke the SetResults method of our user control in charge of displaying the life cycle upstream results
                if (result != null && productID != -1 && !String.IsNullOrEmpty(name))
                    this.resultsControl1.SetResults(name, result, productID);
            }
        }
    }
}
