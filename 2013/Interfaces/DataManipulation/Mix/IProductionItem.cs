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

namespace PlugInsInterfaces.DataTypes.Mix
{
    /// <summary>
    /// A production item can represent a patway or a mix
    /// It contains an Id and a type that allows the user to know if we are referencing a pathway or a mix
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IProductionItem
    {
        /// <summary>
        /// The ID of the pathway or the mix
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Id { get; }
        /// <summary>
        /// Can be Pathway or Mix
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        PluginEnums.SourceType SourceType { get; }
        /// <summary>
        /// In the case of that production item is used in parallel of other, there can be a share associated with this item
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        double Share { get; }
        /// <summary>
        /// If they are any child items (in the example of a mix for instance) we send the list of child items.
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<IProductionItem> Childs { get; }
    }
}
