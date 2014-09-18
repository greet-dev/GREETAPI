/*********************************************************************** 
mail contact: greet@anl.gov 
Copyright (c) 2012, UChicago Argonne, LLC 
All Rights Reserved

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this
  list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this
  list of conditions and the following disclaimer in the documentation and/or
  other materials provided with the distribution.

* Neither the name of the {organization} nor the names of its
  contributors may be used to endorse or promote products derived from
  this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlugInsInterfaces.DataTypes;

namespace Example2.UI
{
    /// <summary>
    /// Allows the user to explore IParameters that are inputs of the model, modify a value and run the simulations
    /// </summary>
    public partial class ParameterExplorer : Form
    {
        public ParameterExplorer()
        {
            InitializeComponent();

            this.listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
        }

        /// <summary>
        /// Creates the list of available pathways and mixes
        /// </summary>
        public void InitializeControls()
        {
            //Gets the dictionary of IParameters object indexed by IParameters.Id
            //Parameters are used to store all input data necessary to the model (flows quantities, shares, heating values, carbon ratios, emission factors...
            IGDataDictionary<string, IParameter> parameters = ParametersExample.controler.CurrentProject.Data.Parameters;

            this.listBox1.Items.Clear();

            //Adding all parameters to the list box items so user can select a parameter
            foreach (IParameter param in parameters.AllValues.OrderBy(item => item.Id))
            {
                this.listBox1.Items.Add(param);       
            }
        }

        /// <summary>
        /// Invoked when the user selects a new parameter in the list
        /// Displays the attributes for that parameter in the user control ParameterProperties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Object selectedItem = this.listBox1.SelectedItem;

            //If the user selected something that corresponds to a parameter we call a method in the ParametersProperty control
            if (selectedItem != null && selectedItem is IParameter)
                this.parameterProperties1.SetParameter(selectedItem as IParameter);
        }

        /// <summary>
        /// Invoked when the user clicks on the button with the text "Run Simulation"
        /// This then calls for a method in the IGreetControler in order to re-run the simulations
        /// 
        /// Results may be different if the user modified one of the parameters using the Parameters Property control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Asks the controler to run the simulations asynchronouly
            //ParametersExample.controler.RunSimalationAsync();

            //Asks the controler to run the simulation and wait until they end before continuing
            ParametersExample.controler.RunSimalation(false);

            MessageBox.Show("Simulation Complete");
        }
    }
}
