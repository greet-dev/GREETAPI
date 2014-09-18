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
using System.IO;
using PlugInsInterfaces.ResultTypes;
using PlugInsInterfaces.DataTypes;
using System.Reflection;
using PlugInsInterfaces.DataTypes.Vehicle;

namespace PlugInsInterfaces
{
    /// <summary>
    /// An interface that defines what methods and events can be used in order to interact with the GREET software
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IGREETController
    {
        /// <summary>
        /// Get the default folder used by GREET to store data, by default the MyDocument/Greet folder
        /// </summary>
        /// <returns></returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string GetDefaultFolder();
        
        /// <summary>
        /// Asks GREET to run the simulation and computes the model
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        void RunSimalationAsync();

        /// <summary>
        /// Asks GREET to run the simulation and computes the model
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        void RunSimalation(bool textMinMaxMonitors);
        
        /// <summary>
        /// When the calculations are done, this event fires up to warn any plugin that
        /// we are going to have some results available now.
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        event EventHandler CalculationFinished;

        /// <summary>
        /// Returns the current modeling year chosen by the user
        /// </summary>
        /// <returns></returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int GetModelYear();

        /// <summary>
        /// Uses an input stream and decompresses it
        /// </summary>
        /// <param name="inp">Compressed input stream to be compressed</param>
        /// <param name="outp">Decompressed stream using deflate</param>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        void Decompress(Stream inp, Stream outp);
        
        /// <summary>
        /// Uses an input stream and compresses it using deflate
        /// </summary>
        /// <param name="inp">Not compressed stream</param>
        /// <param name="outp">Compressed stream</param>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        void Compress(Stream inp, Stream outp);

        /// <summary>
        /// Orders the software to download a specific version of the database from the webservice
        /// </summary>
        /// <param name="revNumber"></param>
        /// <returns></returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string DownloadDatabaseFromServer(int revNumber = 0);

        /// <summary>
        /// Orders the main GUI to load a specific file as the current project
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IProject LoadProjectFile(string filename);

        /// <summary>
        /// Saves the given project to a file
        /// </summary>
        /// <param name="project">The project to save, can be the current project</param>
        /// <param name="fileName">Filename to be used to create the data file</param>
        /// <returns></returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string SaveProject(IProject project, string fileName);

        /// <summary>
        /// Returns the project currently in use
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IProject CurrentProject { get; }

        /// <summary>
        /// Returns a dictionary with units available in GREET
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<string, IUnit> UnitsAvailable { get; }
        
        /// <summary>
        /// Returns a dictionary with unit groups available in GREET
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Dictionary<string, IUnitGroup> UnitGroupsAvailable { get; }

        /// <summary>
        /// Formats a value according to the options chosen by the user in the main form
        /// </summary>
        /// <param name="valueSIUnit">The numerical value to be formated</param>
        /// <param name="SIUnitOrGroup">The unit or unit group in which this value is represented</param>
        /// <param name="format">Format: 0 automatic user preference, 1 scientific notation, 2 all digits</param>
        /// <returns>Formatted value according to the desired format option</returns>
        string FormatValue(double valueSIUnit, string SIUnitOrGroup, int format, bool scale = true);
    }
}
