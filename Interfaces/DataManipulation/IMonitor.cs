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
using PlugInsInterfaces.ResultTypes;
using System.Reflection;
using System.ComponentModel;

namespace PlugInsInterfaces.DataTypes
{
    /// <summary>
    /// An interface for the monitored results in GREET
    /// <para>Monitored items are a way to record upstream results across multiple iterations of the calculations.
    /// For example one might like to record results between multiple calculations and change parameter to see their influence on this monitored results</para>
    /// <para>Another example of use may be for stochastic simulations, one can monitor a result item, run thousands of simulations, and see the distribution of results for that monitored item</para>
    /// </summary>
    public interface IMonitor
    {
        /// <summary>
        /// Clears all values stored from previous simulations
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        void Clear();

        /// <summary>
        /// Results stored from previous simulations
        /// </summary>
        [Browsable(false)]
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<int, IValue> Values { get; }

        /// <summary>
        /// Returns a short description of the monitored value
        /// </summary>
        [Browsable(true), DisplayName("Description")]
        [Obfuscation(Feature = "renaming", Exclude = true)]
        String Description { get; }

        /// <summary>
        /// Returns the number of saved values for this monitored item
        /// </summary>
        [Browsable(true), DisplayName("Values Count")]
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int ResultsCount { get; }

        /// <summary>
        /// A unique name for that monitored value item
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string UniqueId { get; set; }
    }
}
