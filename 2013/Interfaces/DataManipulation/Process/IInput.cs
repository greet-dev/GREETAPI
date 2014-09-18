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

namespace PlugInsInterfaces.DataTypes.Process
{
    /// <summary>
    /// Input of a process
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IInput
    {
        /// <summary>
        /// Returns the source of the Input which might be coming from a Pathway, Pathway Mix, Well, Previous Process
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IInputResourceReference ResourceReference { get; }

        /// <summary>
        /// Returns the List of Technology ids for the input
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<int> TechnologyIds { get; }

        /// <summary>
        /// Sequestration parameters that may be attached to an input
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        ISequestration sequestrationParameter { get; }

        /// <summary>
        /// Source for this input, specifies if the resource is coming from previous, a mix, a pathway or a well
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        PluginEnums.SourceType Source { get; }

        /// <summary>
        /// ID in order that points to the source for this input
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int SourceMixOrPathwayID { get; }

        /// <summary>
        /// Resource ID for that input
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int ResourceId { get; }

        /// <summary>
        /// Unique ID of that input within the limits of the process (used for biogenic carbon propagation)
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Id { get; }
    }

    /// <summary>
    /// Sequestration object that might be used with an input
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface ISequestration
    {
         /// <summary>
        /// Returns the source of the Sequestration which might be coming from a Pathway, Pathway Mix, Well, Previous Process
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IInputResourceReference ResourceReference { get; }

    }


}
