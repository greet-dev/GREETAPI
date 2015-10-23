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
using System.Windows.Forms;
using Greet.DataStructureV4.Interfaces;
using Greet.UnitLib3;

namespace Example2.UI
{
    /// <summary>
    /// Displays the properties of a parameter
    /// </summary>
    internal partial class ParameterProperties : UserControl
    {
        //Member used to store a reference to the instance of the parameter being displayed
        IParameter iParameterRef;

        public ParameterProperties()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets all the controls according to the given parameter
        /// </summary>
        /// <param name="iParameter"></param>
        internal void SetParameter(IParameter iParameter)
        {
            iParameterRef = iParameter;


            //Unique ID for the parameter, this is set when a new parameter is created and cannot be changed in the database
            this.labelID.Text = iParameter.Id;

            //Tries to find a parameter name (or alias) for this IParameter object
            this.labelAlias.Text = iParameter.Name;
            
            //Retrieves the unit group name ("mass", "volume", "em_factor") that is used for this parameter
            //the unit group name is used to match with a SIUnit (unit used by the system which are only SI units)
            //and an DisplayUnit used to render the value when it is displayed in the GUI
            this.labelUnitGroup.Text = Units.QuantityList.ByDim(iParameter.Dim).Name;

            if(Units.QuantityList.ByDim(iParameter.Dim) != null){
                IQuantity grp = Units.QuantityList.ByDim(iParameter.Dim);

                //Sets the text of the label with the unit that is going to be used internally for calculating and storing this IParameter
                this.labelSiUnit2.Text = this.labelSIUnit.Text = grp.SiUnit.Expression;
                
                //Sets the text of the label with the unit that is going to be used for displaying this IParameter
                this.labelDisplayUnit.Text = grp.Units[grp.PreferedUnitIdx].Expression;
                
                //Formatting the value using the default unit of the parameter and the user preferences defined in the option of the main GREET preferences windows
                string paramVal = "";
                try 
                {
                    paramVal = ParametersExample.controler.FormatValue(iParameter.GreetValue, grp.SiUnit.Expression, 0);
                }
                catch(Exception e)
                {
                    paramVal = e.Message;
                }
                this.labelGREETValue.Text = paramVal;

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
                try 
                {
                    paramVal = ParametersExample.controler.FormatValue(preferedValue, grp.SiUnit.Expression, 0);
                }
                catch(Exception e)
                {
                    paramVal = e.Message;
                }

                this.labelFormatedPreferedValue.Text = paramVal;
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
