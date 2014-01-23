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
    /// A unit group represents "energy" or "volume" the default unit used are the unit used by the calculations engine, they're all SI. The user prefered
    /// unit represents the unit that the user selected to be presented on the GUI
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IUnitGroup
    {
        /// <summary>
        /// Unique name of the unit group
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Name { get; }
        /// <summary>
        /// The default unit used for this group in SI unit. This is the unit that is used across the calculations
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string SIUnitStr { get; }
        /// <summary>
        /// The prefered unit used for this group in order to represent a value on the user interface
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string DisplayUnitStr { get; }
        /// <summary>
        /// Takes a value in the override unit, and converts it to the default unit
        /// Usefull to parse user inputs in textboxes
        /// </summary>
        /// <param name="valueToConvert">Value in override unit to be converted</param>
        /// <returns>Value converted in default unit<returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        double ConvertFromOverrideToDefault(double valueToConvert);
        /// <summary>
        /// Takes a value in the default unit and converts it to the user prefered unit
        /// </summary>
        /// <param name="valueToConvert">Value in default unit to be converted</param>
        /// <returns>Value converted in user prefered unit</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        double ConvertFromDefaultToOverride(double valueToConvert);
        /// <summary>
        /// Converts a value represented in default unit to a specified unit (this must be a unit that belongs to the same group)
        /// </summary>
        /// <param name="valueToConvert">Value in default unit to be converted</param>
        /// <param name="unit">Unit that belongs to that group in which the value will be converted</param>
        /// <returns>The converted value in the new unit</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        double ConvertFromDefaultToSpecific(double valueToConvert, string unit);
    }
}
