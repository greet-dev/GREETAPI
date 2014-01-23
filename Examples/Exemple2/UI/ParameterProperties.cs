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
    public partial class ParameterProperties : UserControl
    {
        PlugInsInterfaces.DataTypes.IParameter iParameterRef;

        public ParameterProperties()
        {
            InitializeComponent();
        }

        internal void SetParameter(PlugInsInterfaces.DataTypes.IParameter iParameter)
        {
            iParameterRef = iParameter;

            Dictionary<string,string> data = ParametersExample.controler.CurrentProject.Data.ParametersAliases;

            this.labelID.Text = iParameter.Id;
            if (data != null && data.ContainsKey(iParameter.Id))
                this.labelAlias.Text = data[iParameter.Id];
            else
                this.labelAlias.Text = "No Alias for this parameter";
            this.labelUnitGroup.Text = iParameter.UnitGroupName;

            Dictionary<string, IUnitGroup> groups = ParametersExample.controler.UnitGroupsAvailable;
            if(groups != null && groups.ContainsKey(iParameter.UnitGroupName)){
                IUnitGroup grp = groups[iParameter.UnitGroupName];
                
                this.labelSiUnit2.Text = this.labelSIUnit.Text = grp.SIUnitStr;
                
                this.labelDisplayUnit.Text = grp.DisplayUnitStr;
                
                this.labelGREETValue.Text = ParametersExample.controler.FormatValue(iParameter.GreetValue, grp.SIUnitStr, 0);
                
                this.textBox1.TextChanged -= new EventHandler(textBox1_TextChanged);
                this.textBox1.Text = iParameter.UserValue.ToString();
                this.textBox1.TextChanged += new EventHandler(textBox1_TextChanged);

                this.labelPreferGreet.Text = iParameter.UseOriginal.ToString();

                double preferedValue = (iParameter.UseOriginal) ? iParameter.GreetValue : iParameter.UserValue;
                this.labelFormatedPreferedValue.Text = ParametersExample.controler.FormatValue(preferedValue, grp.SIUnitStr, 0);
            }
            else{
                this.labelSiUnit2.Text = this.labelSIUnit.Text = "Unknown Group";
                this.labelUnitGroup.Text = "Unknown Group";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double temp = 0;
            if (iParameterRef != null && Double.TryParse(this.textBox1.Text, out temp))
            {
                iParameterRef.UserValue = temp;
                this.SetParameter(iParameterRef);
            }
        }

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
