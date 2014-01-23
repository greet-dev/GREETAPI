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
using PlugInsInterfaces.DataTypes.Vehicle; 

namespace PlugInsInterfaces.DataTypes.Vehicle
{
    /// <summary>
    /// Representation of a vehicle in GREET
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IVehicle : IXmlObj
    {
        /// <summary>
        /// Name for this vehicle as it is going to show on the graphical interface
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Name { get; set; }
        /// <summary>
        /// Unique ID for this vehicle among the vehicles
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Id { get; set; }
        /// <summary>
        /// Returns a IEnumberable of the different vehicle operating modes
        /// usually modes types are "cd" or "cs"
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<string,IVehicleMode> OperatingModes { get; }
        /// <summary>
        /// True if the calculations for that vehicle where correclty done
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        bool CalulatedCorrectly { get; }

        /// <summary>
        /// The base vehicle for energy consumption
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int BaseMpgVehicleId
        {
            get;
            set;
        }

        /// <summary>
        /// The base vehicle for emissions
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int BaseEmissionVehicleId
        {
            get;
            set;
        }

        /// <summary>
        /// Image Name that represents the Vehicle
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string PictureName { get; set; }

    }
}
