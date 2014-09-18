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

namespace PlugInsInterfaces.DataTypes.Process
{
    /// <summary>
    /// <para>Definition of a process</para>
    /// <para>In order to get upstream results associated with the output of a process, one must check the process reference interface PlugInsInterfaces.DataTypes.Pathway.IProcessReference</para>
    /// <para>As a process can be used in different pathway, the results cannot be associated with an instance of a process, but with an instance of a process reference PlugInsInterfaces.DataTypes.Pathway.IProcessReference within a PlugInsInterfaces.DataTypes.Pathway.IPathway</para>
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IProcess : IXmlObj
    {
        /// <summary>
        /// Name for this process as it is going to show on the graphical interface
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Name { get; set; }
        /// <summary>
        /// Unique ID for this vprocess among the processes
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Id { get; set; }
        /// <summary>
        /// Returns the Resource Id of the Main output of the process
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int MainOutputResourceID { get; }
        /// <summary>
        /// Returns the list of all the output emissions of the process
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<int> OutputEmissionsIds { get; }
        /// <summary>
        /// Returns a flatten list of all the inputs in a process. Includes inputs from Group and individual inputs.
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<IInput> FlattenInputList { get; }
        /// <summary>
        /// Returns the list of Co-Product resource id's
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<int> CoProductIds { get; }

    }
}
