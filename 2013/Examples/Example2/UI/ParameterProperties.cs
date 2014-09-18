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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlugInsInterfaces.DataTypes;

namespace Example2.UI
{
    /// <summary>
    /// Displays the properties of a parameter
    /// </summary>
    public partial class ParameterProperties : UserControl
    {
        //Member used to store a reference to the instance of the parameter beeing displayed
        PlugInsInterfaces.DataTypes.IParameter iParameterRef;

        public ParameterProperties()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets all the controls according to the given parameter
        /// </summary>
        /// <param name="iParameter"></param>
        internal void SetParameter(PlugInsInterfaces.DataTypes.IParameter iParameter)
        {
            iParameterRef = iParameter;

            //Parameters aliases is a dictionary indexed by IParameter.ID that can match to a value representing a new ID for this same parameter
            //For example, if the IParameter was created with the ID=123456789 and the user wants to see something nicer, the user can set a "Parameter Name"
            //in the main UI of GREET by right clicking on it. Then a new entry is added to the ParameterAliases <"123456789","New Name Set By User">
            Dictionary<string,string> data = ParametersExample.controler.CurrentProject.Data.ParametersAliases;

            //Unique ID for the parameter, this is set when a new parameter is created and cannot be changed in the database
            this.labelID.Text = iParameter.Id;

            //Tries to find a parameter name (or alias) for this IParameter object
            if (data != null && data.ContainsKey(iParameter.Id))
                this.labelAlias.Text = data[iParameter.Id];
            else
                this.labelAlias.Text = "No Alias for this parameter";

            //Retrieves the unit group name ("mass", "volume", "em_factor") that is used for this parameter
            //the unit group name is used to match with a SIUnit (unit used by the system which are only SI units)
            //and an DisplayUnit used to render the value when it is displayed in the GUI
            this.labelUnitGroup.Text = iParameter.UnitGroupName;

            Dictionary<string, IUnitGroup> groups = ParametersExample.controler.UnitGroupsAvailable;
            if(groups != null && groups.ContainsKey(iParameter.UnitGroupName)){
                IUnitGroup grp = groups[iParameter.UnitGroupName];

                //Sets the text of the label with the unit that is going to be used internally for calculating and storing this IParameter
                this.labelSiUnit2.Text = this.labelSIUnit.Text = grp.SIUnitStr;
                
                //Sets the text of the label with the unit that is going to be used for displaying this IParameter
                this.labelDisplayUnit.Text = grp.DisplayUnitStr;
                
                //Formatting the value using the default unit of the parameter and the user preferences defined in the option of the main GREET preferences windows
                this.labelGREETValue.Text = ParametersExample.controler.FormatValue(iParameter.GreetValue, grp.SIUnitStr, 0);
                //Here we are using the GREET Value which corresponds to the default value for that parameter provided by the GREET team
                //When the user changes the value of a parameter, it is only stored in the UserValue field. That way the user has the possiblity
                //to switch between GREETValue or UserValue for any parameter of the model

                //Removing events on the text box before setting the text and the event back
                this.textBox1.TextChanged -= new EventHandler(textBox1_TextChanged);
                this.textBox1.Text = iParameter.UserValue.ToString();
                this.textBox1.TextChanged += new EventHandler(textBox1_TextChanged);

                //Sets the text of the label as to show weather we are using the GREET original value for this parameter or the user entered value
                this.labelPreferGreet.Text = iParameter.UseOriginal.ToString();

                //Finds the value that the user wants to use for the calculations for this IParameter (GREETValue or UserValue) and displays it nicely formatted
                double preferedValue = (iParameter.UseOriginal) ? iParameter.GreetValue : iParameter.UserValue;
                this.labelFormatedPreferedValue.Text = ParametersExample.controler.FormatValue(preferedValue, grp.SIUnitStr, 0);
            }
            else{
                this.labelSiUnit2.Text = this.labelSIUnit.Text = "Unknown Group";
                this.labelUnitGroup.Text = "Unknown Group";
            }
        }

        /// <summary>
        /// Invoked when the user changes the value of the IParameter
        /// This sets the new UserValue of the parameter as well as the UseOriginal attribute to false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double temp = 0;
            if (iParameterRef != null && Double.TryParse(this.textBox1.Text, out temp))
            {
                iParameterRef.UserValue = temp;
                this.SetParameter(iParameterRef);
            }
        }

        /// <summary>
        /// Toogles the use of the GreetValue or UserValue for the parameter.
        /// When simulations are running, the algorithms will choose which value to choose based on the UseOriginal attribute of the parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (iParameterRef != null)
            {
                iParameterRef.UseOriginal = !iParameterRef.UseOriginal;
                this.SetParameter(iParameterRef);
            }
        }
    }
}
