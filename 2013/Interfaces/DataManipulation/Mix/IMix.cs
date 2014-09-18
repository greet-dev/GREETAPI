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

namespace PlugInsInterfaces.DataTypes.Mix
{
    /// <summary>
    /// A mix defines fuel production entities and shares for them
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IMix : IXmlObj
    {
        /// <summary>
        /// Name for this mix as it is going to show on the graphical interface
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Name { get; set; }

        /// <summary>
        /// Unique ID for this mix among the mixes
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Id { get; set; }

        /// <summary>
        /// Returns a list of fuel production entities the mix uses
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<IProductionItem> FuelProductionEntities { get; }

        /// <summary>
        /// Unique ID for this mix among the mixes
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int MainOutputResourceID { get; }

        /// <summary>
        /// Upstream results associated to that mix
        /// <para>This is the prefered way to get the upstream for a product from any pathway. 
        /// This is equivalent of getting the upstream of the last process in this pathway</para>
        /// </summary>
        /// <param name="data">Data object needed for references</param>
        /// <returns>Dictionary containing the allocated results for the resource created by this mix</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IResults> GetUpstreamResults(IData data);

    }
}
