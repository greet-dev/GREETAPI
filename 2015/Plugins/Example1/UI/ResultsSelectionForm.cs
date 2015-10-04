using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Greet.DataStructureV4.Interfaces;

namespace Example1.UI
{
    /// <summary>
    /// A simple form that shows a list of available pathways and mixes
    /// and display the associated GHGs results when an element is selected
    /// </summary>
    internal partial class ResultsSelectionForm : Form
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

                foreach (IPathway pathway in pathways.AllValues.Where(item => ResultsAccess.controler.CurrentProject.Data.Helper.PathwayMainOutputResouce(item.Id) == resource.Id))
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

                if (tag is IPathway)
                {//if the retrieved object is a pathway
                    IPathway path = tag as IPathway;
                    //We ask the pathway what is the product defined as the main product for this pathway
                    //then store an integer that corresponds to an IResource.ID
                    productID = ResultsAccess.controler.CurrentProject.Data.Helper.PathwayMainOutputResouce(path.Id);
                    //We use the ID of the Resource that corresponds to the main output of the pathway to get the correct results
                    Dictionary<IIO, IResults> availableResults = path.GetUpstreamResults(ResultsAccess.controler.CurrentProject.Data);
                    Guid desiredOutput = new Guid();
                    if (null == availableResults.Keys.SingleOrDefault(item => item.ResourceId == productID))
                    {
                        MessageBox.Show("Selected pathway does not produce the fuel selected. Please remove it from the Fule Types list");
                        return;
                    }
                    else
                    {
                        foreach (IIO io in availableResults.Keys.Where(item => item.ResourceId == productID))
                        {
                            desiredOutput = io.Id;
                            if (io.Id == path.MainOutput)
                            {
                                desiredOutput = io.Id;
                                break;
                            }
                        }
                    }
                    result = availableResults.SingleOrDefault(item => item.Key.Id == desiredOutput).Value;
                    //We set the string variable as the name of the pathway
                    name = path.Name;
                }
                else if (tag is IMix)
                {//if the retrieved object is a mix
                    IMix mix = tag as IMix;
                    //We ask the mix what is the product defined as the main product for this mix
                    //then store an integer that corresponds to an IResource.ID
                    productID = mix.MainOutputResourceID;
                    //We use the ID of the Resource that corresponds to the main output of the pathway to get the correct results
                    var upstream = mix.GetUpstreamResults(ResultsAccess.controler.CurrentProject.Data);

                    if (null == upstream.Keys.SingleOrDefault(item => item.ResourceId == productID))
                    {
                        MessageBox.Show("Selected mix does not produce the fuel selected. Please remove it from the Fule Types list");
                        return;
                    }

                    //a mix has a single output so we can safely do the folowing
                    result = upstream.SingleOrDefault(item => item.Key.ResourceId == productID).Value;

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
