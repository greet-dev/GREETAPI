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
            IGDataDictionary<int, IResource> resources = ResultsAccess.controler.CurrentProject.Data.Resources;
            IGDataDictionary<int, IPathway> pathways = ResultsAccess.controler.CurrentProject.Data.Pathways;
            IGDataDictionary<int, IMix> mixes = ResultsAccess.controler.CurrentProject.Data.Mixes;

            this.treeView1.Nodes.Clear();

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
                {
                    IPathway path = tag as IPathway;
                    productID = path.MainOutputResourceID;
                    Dictionary<int, IResults> availableResults = path.GetUpstreamResults(ResultsAccess.controler.CurrentProject.Data);
                    if (availableResults.ContainsKey(productID))
                        result = availableResults[productID];
                    name = path.Name;
                }
                else if (tag is IMix)
                {
                    IMix mix = tag as IMix;
                    productID = mix.MainOutputResourceID;
                    Dictionary<int, IResults> availableResults = mix.GetUpstreamResults(ResultsAccess.controler.CurrentProject.Data);
                    if (availableResults.ContainsKey(productID))
                        result = availableResults[productID];
                    name = mix.Name;
                }

                if (result != null && productID != -1)
                    this.resultsControl1.SetResults(name, result, productID);
            }
        }
    }
}
