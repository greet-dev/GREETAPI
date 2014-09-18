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
using PlugInsInterfaces.DataTypes;
using System.ComponentModel;

namespace PlugInsInterfaces.ResultTypes
{
    /// <summary>
    /// Set of results for a pathway or process or vehicle
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IResults 
    {
        /// <summary>
        /// string representing the kind of result object we have, the type can be
        /// mix, pathway, process, vehicle, emissions, resources, lifecycle, onsite, group
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        PluginEnums.itemType ObjectType { get; }

        /// <summary>
        /// The ID associated with the objectType if any is available, otherwise this value can be set as -1
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int ObjectID { get; }
        
        /// <summary>
        /// Immutable and created during the calculations, stores the unit associated with the functional amount for the associated results, usually this would be "joule", "cu_meter", "kilogram" or "meter"
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string FunctionalUnit {get;}

        /// <summary>
        /// Emissions that do not includes any upstream
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> OnSiteEmissions();

        /// <summary>
        /// Resources used without including any upstream
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> OnSiteResources();

        /// <summary>
        /// Urban emissions without including any upstream
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> OnSiteUrbanEmissions();

        /// <summary>
        /// Emissions including all upstreams
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> LifeCycleEmissions();

        /// <summary>
        /// Resources used including all upstreams
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> LifeCycleResources();

        /// <summary>
        /// Urban emissions including all upstreams
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> LifeCycleUrbanEmissions();

        /// <summary>
        /// This object represents groupped on site emissions results urban only, for example gases that are members of the
        /// greenhouse gases group will all be summed up in a single value here. 
        /// <para> Emissions that were produced on site for a process or a vehicle, basically it includes anything relevant
        /// to the current object except the upstream values</para>
        /// <para>The same groupping is going to be done for other gases groups like criteria pollutants, or vehicle specific emissions</para>
        /// <para>The IDs for each of these groups can be matched with the collection of groups available in PlugInsInterfaces.DataTypes.IData.GasGroups</para>
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> OnSiteEmissionsGroups(IData data);

        /// <summary>
        /// This object represents groupped resources used results, for example gases that are members of the
        /// petroleum group will all be summed up in a single value here. 
        /// <para>Resources that were used on site for a process or a vehicle, basically it includes anything relevant
        /// to the current object except the upstream values</para>
        /// <para>The same groupping is going to be done for other gases groups like natrual gas, or renewables</para>
        /// <para>The IDs for each of these groups can be matched with the collection of groups available in PlugInsInterfaces.DataTypes.IData.ResourceGroups</para>
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> OnSiteResourcesGroups(IData data);

        /// <summary>
        /// This object represents groupped on site emissions results urban only, for example gases that are members of the
        /// greenhouse gases group will all be summed up in a single value here. 
        /// <para>Urban emissions that were produced on site for a process or a vehicle, basically it includes anything relevant
        /// to the current object except the upstream values</para>
        /// <para>The same groupping is going to be done for other gases groups like criteria pollutants, or vehicle specific emissions</para>
        /// <para>The IDs for each of these groups can be matched with the collection of groups available in PlugInsInterfaces.DataTypes.IData.GasGroups</para>
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> OnSiteUrbanEmissionsGroups(IData data);

        /// <summary>
        /// This object represents groupped life cycle emissions results, for example gases that are members of the
        /// greenhouse gases group will all be summed up in a single value here. 
        /// <para>The same groupping is going to be done for other gases groups like criteria pollutants, or vehicle specific emissions</para>
        /// <para>The IDs for each of these groups can be matched with the collection of groups available in PlugInsInterfaces.DataTypes.IData.GasGroups</para>
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> LifeCycleEmissionsGroups(IData data);

        /// <summary>
        /// This object represents groupped resources used results, for example gases that are members of the
        /// petroleum group will all be summed up in a single value here. 
        /// <para>The same groupping is going to be done for other gases groups like natrual gas, or renewables</para>
        /// <para>The IDs for each of these groups can be matched with the collection of groups available in PlugInsInterfaces.DataTypes.IData.ResourceGroups</para>
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> LifeCycleResourcesGroups(IData data);

        /// <summary>
        /// This object represents groupped life cycle emissions results for urban only, for example gases that are members of the
        /// greenhouse gases group will all be summed up in a single value here. 
        /// <para>The same groupping is going to be done for other gases groups like criteria pollutants, or vehicle specific emissions</para>
        /// <para>The IDs for each of these groups can be matched with the collection of groups available in PlugInsInterfaces.DataTypes.IData.GasGroups</para>
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> LifeCycleUrbanEmissionsGroups(IData data);

    }
}
