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
    /// A unit in our unit system
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IUnit
    {
        /// <summary>
        /// Defines the name for this unit, this name must be unique across units in the system, it's used as a Key in the units collections
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Name { get; }
        /// <summary>
        /// The display name for this unit can be more descriptive and is what is displayed in the unit configuration
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string DisplayName { get; }
        /// <summary>
        /// The abbreviation is used at the tail of numeric values that are displayed in the GUI
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Abbrev { get; set; }
        /// <summary>
        /// The formula that is used to convert this unit to the default unit of the group in which this unit is member of
        /// Example: If this unit is degrees farenheit, how to convert to degrees kelvin
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string ToDefaultStr { get; }
        /// <summary>
        /// The formula that is used to convert from the default unit of the group in which this unit is member of to this unit
        /// Example: If this unit is degrees farenheit, how to convert degrees kelvin to farenheit
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string FromDefaultStr { get; }

    }
}
