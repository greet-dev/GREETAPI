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
using PlugInsInterfaces.ResultTypes;

namespace PlugInsInterfaces.DataTypes.Resource
{
    /// <summary>
    /// The resource object contains the physical property and the name of a resource
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IResource : IXmlObj
    {
        /// <summary>
        /// Name for this resource as it is going to show on the graphical interface
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Name { get; set; }
        /// <summary>
        /// Unique ID for this resource among the resources
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Id { get; set; }
        /// <summary>
        /// List of all the gases that the resource evaporates into
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<IEvaporatedGas> EvaporatedGasess { get; set; }
        /// <summary>
        /// The Sulfur ratio of the resource
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IParameter SulfurRatio { get; }
        /// <summary>
        /// The Carbon ratio of the resource
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IParameter CarbonRatio { get; set; }
        /// <summary>
        /// The Density of the resource
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IParameter Density { get; set; }
        /// <summary>
        /// The Low Heating Value of the resource
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IParameter HigherHeatingValue { get; set; }
        /// <summary>
        /// The Hight Heating Value of the resource
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IParameter LowerHeatingValue { get; set; }
        /// <summary>
        /// List of compatible resource Ids
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<int> CompatibilityIds { get; set; }
        /// <summary>
        /// Image Name that represents the resource
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string PictureName { get; set; }
        /// <summary>
        /// Converts a result value in a certain unit into another result value in the desired unit
        /// </summary>
        /// <param name="unit_name">The desired unit</param>
        /// <param name="value">Value to be converted</param>
        /// <returns>Converted value in desired unit</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IValue ConvertTo(string unit_name, IValue value);
        /// <summary>
        /// Converts a parameter value in a certain unit into another result value in the desired unit
        /// The GREET value or USER value will be used depending on the UseOriginal attribute of the IParameter
        /// </summary>
        /// <param name="unit_name">The desired unit</param>
        /// <param name="value">Value to be converted</param>
        /// <returns>Converted value in desired unit</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IValue ConvertTo(string unit_name, IParameter value);
        /// <summary>
        /// Lists the memberships of that gas
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<int> Memberships { get; set; }
    }
}

