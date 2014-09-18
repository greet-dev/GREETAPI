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
using System.Linq;
using System.Text;
using System.Reflection;

namespace PlugInsInterfaces.DataTypes
{
    /// <summary>
    /// Class that represents a value and unit
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IParameter
    {
        /// <summary>
        /// Unique ID for the parameter.
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Id { get; }
        /// <summary>
        /// Unique Name for the parameter.
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Name { get; }

        /// <summary>
        /// Unit or group of the parameter.
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string UnitGroupName { get; }

        /// <summary>
        /// Default or "original" value of the parameter in default unit
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        double GreetValue { get; set; }

        /// <summary>
        /// Use override value for the parameter in default unit
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        double UserValue { get; set; }

        /// <summary>
        ///Returns the formula in use for the parameter (in default unit)
        ///In case a formula is used there will be a string here and the GreetValue and UserValue needs to be calculated from that formula
        ///If there is no string in here, it means that the parameter is not using a formula, therefore the UserValue and GreetValue do not need to be refreshed
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string CurrentFormula { get; set; }

        /// <summary>
        /// Defines weather the calculations should use the original value or the user value
        /// True uses the original, false uses the override
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        bool UseOriginal { get; set; }
    }
}
