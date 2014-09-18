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
using PlugInsInterfaces.DataTypes.Process.Transportation;
using PlugInsInterfaces.DataTypes.Mix;
using PlugInsInterfaces.DataTypes.Process;
using PlugInsInterfaces.DataTypes.Resource;
using PlugInsInterfaces.DataTypes.Technology;
using PlugInsInterfaces.DataTypes.Pathway;

namespace PlugInsInterfaces.DataTypes
{
    /// <summary>
    /// Contains the database necessary to perform calculations
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IData
    {
        /// <summary>
        /// Returns a dictionary type object containing all resources; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<int, IResource> Resources { get; }

        /// <summary>
        /// Returns a dictionary type object containing all mixes; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<int, IMix> Mixes { get; }

        /// <summary>
        /// Returns a dictionary type object containing all technologies; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<int, ITechnology> Technologies { get; }

        /// <summary>
        /// Returns a dictionary type object containing all pathways; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<int, IPathway> Pathways { get; }

        /// <summary>
        /// Returns a dictionary type object containing all processes; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<int, IProcess> Processes { get; }

        /// <summary>
        /// Returns a dictionary type object containing all locations; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<int, ILocation> Locations { get; }

        /// <summary>
        /// Returns a dictionary type object containing all gases; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<int, IGas> Gases { get; }

        /// <summary>
        /// Returns a dictionary type object containing all modes; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<int, IAMode> Modes { get; }

        /// <summary>
        /// Returns a dictionary type object containing all vehicles; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<int, IVehicle> Vehicles { get; }

        /// <summary>
        /// Returns a dictionary type object containing all input tables; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<string, IInputTable> InputTables { get; }

        /// <summary>
        /// Returns a dictionary type object containing Pictures; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<string, IPicture> Pictures { get; }

        /// <summary>
        /// Returns a dictionary type object containing Parameters; see IGDataDictionary for details on implementation
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IGDataDictionary<string, IParameter> Parameters { get; }

        /// <summary>
        /// Returns a dictionary type object containing  parameters aliases
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<string, string> ParametersAliases { get; }

        /// <summary>
        /// Returns the available groups for Resources
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<IGroup> ResourceGroups
        {
            get;
        }
        /// <summary>
        /// Returns the available groups for Emissions/Gases
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<IGroup> GasGroups
        {
            get;
        }


        /// <summary>
        ///This method checks all the relationships in the data just after the data has been loaded 
        /// </summary>
        /// <returns>The errors that are found in the Data</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string CheckIntegrity(bool showIds, bool fixFixableIssues = true);

        
        /// <summary>
        /// This is a list of all mixes and pathways that output the given resource.
        /// </summary>
        /// <param name="resourceId">The resource id whose list of possible ways is needed</param>
        /// <returns>The return type is a list of InputResourceReference's because this object can represent a pathway or a mix depending on the source attribute of the object</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<IInputResourceReference> ProduceResource(int resourceId);

    }
}
