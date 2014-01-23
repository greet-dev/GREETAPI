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
    /// An idependent item represents the chain of dependency between entities in GREET
    /// for instance, a Pathway depends on the Process within that pathway.
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IDependentItem : IEquatable<IDependentItem>
    {
        /// <summary>
        /// ID among the other dependent items of the parent
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Id { get; }
        /// <summary>
        /// A name for that dependent item
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        String Name { get; }
        /// <summary>
        /// Resource if the dependent item has one
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Resource { get; }
        /// <summary>
        /// Resource name if the dependent item has one
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        String ResourceName { get; }
        /// <summary>
        /// Type name to define which data type is necessary for this dependent item
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        String TypeName { get; }
        /// <summary>
        /// Type name to define which data type is necessary for this dependent item
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        PlugInsInterfaces.DataTypes.PluginEnums.itemType Type { get; }
        /// <summary>
        /// Returns true if the dependent item passed as an argument is equal to the current instance
        /// they can be different instances but represents the same object
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        bool Equals(IDependentItem other);
    }
}
