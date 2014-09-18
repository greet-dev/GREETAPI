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
using System.Xml;
using PlugInsInterfaces.PluginTypes;

namespace PlugInsInterfaces.DataTypes
{
    /// <summary>
    /// Object representing the collection of entities of a loaded project
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IProject
    {
        /// <summary>
        /// Entities of the loaded database
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IData Data { get; set; }

        /// <summary>
        /// Monitored values
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        List<IMonitor> MonitorValues { get; }

        /// <summary>
        /// <para>Returns a copy of the node reserved for this plugin. Modifications to this node will not be saved to the file</para>
        /// <para>In order to save to the database, please use the method PlugInsInterfaces.DataTypes.IProject.PushPluginXML(APlugin plugin, XmlNode node);</para>
        /// </summary>
        /// <param name="plugin">The current plugin</param>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        XmlNode GetPluginXML(APlugin plugin);

        /// <summary>
        /// Returns a copy of the project document. Modifications to this document will not be saved to the file
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        XmlDocument GetProjectDocument();

        /// <summary>
        /// <para>Pushing an XML node to the project will automatically set a "project modified" flag, that way the user will be prompted to save his file before quitting</para>
        /// <para>In order to save the project and all the data it contains to a file, please use the method  string PlugInsInterfaces.IGREETController.SaveProject(IProject project, string fileName)</para>
        /// </summary>
        /// <param name="plugin">The current plugin</param>
        /// <param name="node">The node to be pushed to the database</param>
        /// <returns><para>-1 if the name of the node, 0 if the node has been inserted, +1 if the node replaces an existing one</para>
        /// <para>May throw an exceltion if the XmlNode is not created using the same Owner document or contains XMLDeclarations that should no be inserted, see Inner Exception</para>
        /// </returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int PushPluginXML(APlugin plugin, XmlNode node);

        /// <summary>
        /// Get or sets the version number of the project
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Version { get; set; }


    }
}
