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
using PlugInsInterfaces.DataTypes.Resource;
using PlugInsInterfaces.ResultTypes;

namespace PlugInsInterfaces.DataTypes.Pathway
{
    /// <summary>
    /// <para>A pathway represents a flow of resources though processes in series. Each process is represented by a PlugInsInterfaces.DataTypes.Pathway.IProcessReference</para>
    /// <para>In order to get the results, or upstream associated with the production of a resource from a pathway, the prefered way is to get the results associated with the last process reference in that pathway</para>
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IPathway : IXmlObj
    {
        /// <summary>
        /// Name for this pathway as it is going to show on the graphical interface
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Name { get; set; }
        /// <summary>
        /// Unique ID for this pathway among the pathways
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Id { get; set; }
        /// <summary>
        /// Returns and ordered list of the process IDs used in this pathway
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<IProcessReference> Processes { get; }
        /// <summary>
        /// <para>The pathway may not begin with a process exctracting resources from a well, or a crop farming process.</para>
        /// <para>It is possible to begin a pathway with a feed, that is going to represent another pathway or mix. This feed is an instance of an IInputResourceReference and then the first process in the pathway must have an input source from "previous".</para>
        /// <para>This feature is mostly used for visualization purposes, but is mathematically equivalent to having the first processing defining it's inputs from a pathway source or mix source, instead of defining the feed as a memember of the pathway.</para>
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IInputResourceReference Feed { get; }
        /// <summary>
        /// Unique ID for this pathway among the pathways
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int MainOutputResourceID { get; }
        /// <summary>
        /// Upstream results associated to that pathway
        /// <para>This is the prefered way to get the upstream for a product from any pathway. 
        /// This is equivalent of getting the upstream of the last process in this pathway</para>
        /// </summary>
        /// <param name="data">Data object needed for references</param>
        /// <returns>Dictionary containing the allocated results for each output of pathway</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IResults> GetUpstreamResults(IData data);
    }
}
