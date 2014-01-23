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
    /// An interface wrapper around a mutable dictionary to expose common methods such as CreateValue, DeleteValue, AllValues
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IGDataDictionary<TKey, TValue>
    {
        /// <summary>
        /// Add the given value to the dictionary
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        void AddValue(TValue value);

        /// <summary>
        /// Returns the value associated with a given key; 
        /// Returns Null if the key does not exist
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        TValue ValueForKey(TKey key);

        /// <summary>
        /// Creates a new instance of an object of the same type that the dictionary holds
        /// </summary>
        /// <param name="type">Acts as an identifier when needed.
        /// <para>List of cases where identifier is needed:</para>
        /// <para>Identifier for creating new instances of processes (returned as Process.IProcess) - <paramref name="type"/> = 1: stationary process. <paramref name="type"/> = 2: transportation process. Default: stationary process</para>
        /// <para>Identifier for creating new instances of mode for transportation processes (returned as Process.Transportation.IAMode) - <paramref name="type"/> = 1: tankerBarge. <paramref name="type"/> = 2:truck. type = 3:pipeline. <paramref name="type"/> = 4:rail. <paramref name="type"/> = 5:connector. Default: connector</para>
        /// </param>
        /// <code region="Example 1" language="cs" title="Example for creating a transportation process">
        /// IProject project = myControler.CurrentProject;\n
        /// Process.IProcess proc = project.Data.Processes.CreateValue(data, 2);
        /// </code>
        /// <returns>Created instance</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        TValue CreateValue(IData data, int type = 0);

        /// <summary>
        /// Returns true if the key exists in the dictionary; returns false otherwise
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        bool KeyExists(TKey key);

        /// <summary>
        /// Deletes the given value from the dictionary
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        bool DeleteValue(IData data, TKey key);

        /// <summary>
        /// Returns an enumerable sequence of all values contained in the dictionary
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IEnumerable<TValue> AllValues { get; }
    }
}