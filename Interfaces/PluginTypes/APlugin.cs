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
using System.Drawing;
using System.Windows.Forms;
using PlugInsInterfaces.DataTypes;

namespace PlugInsInterfaces.PluginTypes
{
    /// <summary>
    /// Abstact class to implement in order to be loaded in GREET as a plugin
    /// </summary>
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public abstract class APlugin
    {
        /// <summary>
        /// Initialize the default parameters for the plugin
        /// </summary>
        /// <returns>True if the initialization was done correctly</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public abstract bool InitializePlugin(PlugInsInterfaces.IGREETController controler);
        /// <summary>
        /// Name of the plugin that will be used in the plugin manager
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public abstract string GetPluginName { get; }
        /// <summary>
        /// Short description of the plugin with a possible link to a specific website
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public abstract string GetPluginDescription { get; }
        /// <summary>
        /// Version of the plugin
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public abstract string GetPluginVersion { get; }
        /// <summary>
        /// Icon to be used in the plugin manager
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public abstract Image GetPluginIcon { get; }
        /// <summary>
        /// Messages that the plugins stacks to be displayed by the main form
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual Stack<String> GetPluginMessages { get { return new Stack<string>(); } }
        /// <summary>
        /// Called when GREET main routine ends. This is the last call before everything gets unallocated from the memory
        /// At this points the Main form no longer exists
        /// </summary>
        /// <returns></returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual void onFinalizePlugin() { }
        /// <summary>
        /// Called when the main form is closing, just before we check for any data changes in loaded the database
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual void onMainFormClosing() { }
        /// <summary>
        /// A collection of tool strip menu items that will be added to the existing main menu of the GREET main form
        /// or null
        /// </summary>
        /// <returns>Array of menu items to be displayed</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual ToolStripMenuItem[] GetMainMenuItems() { return null; }
        /// <summary>
        /// A collection of menu items that will be added to the context menus shown for a processe (stationary or transportation)
        /// or null
        /// </summary>
        /// <returns>Array of menu items to be displayed</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual ToolStripMenuItem[] GetProcessMenuItems(int processID) { return null; }
        /// <summary>
        /// A collection of menu items that will be added to the context menus shown for a pathway
        /// or null
        /// </summary>
        /// <returns>Array of menu items to be displayed</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual ToolStripMenuItem[] GetPathwayMenuItems(int pathwayID) { return null; }
        /// <summary>
        /// A collection of menu items that will be added to the context menus shown for a mix
        /// or null
        /// </summary>
        /// <returns>Array of menu items to be displayed</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual ToolStripMenuItem[] GetMixMenuItems(int mixID) { return null; }
        /// <summary>
        /// A collection of menu items that will be added to the context menus shown for a vehicle
        /// or null
        /// </summary>
        /// <returns>Array of menu items to be displayed</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual ToolStripMenuItem[] GetVehiclesMenuItems(int vehicleID) { return null; }
        /// <summary>
        /// Returns a keyvalue pair containing a string and an image that will be used to create a button in the main form
        /// if no button is wanted for this control return null. Actions related to the main button should be registered within the plugin
        /// </summary>
        /// <returns>A button control that is going to be displayed in the main form</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual Button GetMainButton() { return null; }
        /// <summary>
        /// Called when a parameter is right clicked in order to add items to the existing menu
        /// Parameter ID represents a unique ID for the selected parameter
        /// </summary>
        /// <param name="parameter">Parameter that is beeing clicked</param>
        /// <returns>Array of menu items to be displayed</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual MenuItem[] GetParameterMenuItems(IParameter parameter) { return null; }
        /// <summary>
        /// Raised when one of the supported controls raises a Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual void onControlClicked(object sender, EventArgs e) { }
        /// <summary>
        /// Raised when one of the supported controls raises a KeyPress event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual void onControlKeyPressed(object sender, System.Windows.Forms.KeyPressEventArgs e) { }
        /// <summary>
        /// Raised when one of the supported controls raises a Leave event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual void onControlLeave(object sender, EventArgs e) { }
        /// <summary>
        /// Raised when one of the supported controls raises a Paint event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="content">Possibly the object that is currently represented within the sender control</param>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual void onControlPaint(object sender, System.Windows.Forms.PaintEventArgs e, object content) { }
        /// <summary>
        /// Called when a database is loaded by the user or automatically after an update. Method is 
        /// called after the database is loaded into GREET.
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual void onDatabaseLoaded(string filename) { }
        /// <summary>
        /// Called when a new file is available after an updated. Method is called right after the downloaded
        /// file is saved in some folder
        /// </summary>
        /// <param name="updateFile">The location of the new file downloaded by the automated process</param>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual void onDatabaseUpdate(string updateFile) { }
        /// <summary>
        /// Called when the user clicks File->Save, File->Save As or when he hits Ctrl+S in the main user interface.
        /// <para>This is called just before we save all the data to the XML file, so it's a good time to push your plugin
        /// XML node to the project or to save the data you need to save</para>
        /// <para>Important, this may also be called before an Autosave or if an other plugin asks the IGREETController to save data to a given file</para>
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual void onSaveActionRequired() { }
        /// <summary>
        /// Returns a list of the available command names for this plugin
        /// that can be called using the command line
        /// </summary>
        /// <returns></returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual string[] AvailableCommandsForThisPlugin() { return new string[0]; }
        /// <summary>
        /// Asks the plugin to run the specified command and return True if
        /// the execution was done properly
        /// </summary>
        /// <param name="command">The comannd that the plugin needs to run</param>
        /// <param name="args">The arguments for that command</param>
        /// <returns>True if the command runs wellm false otherwise</returns>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        public virtual bool RunCommand(string command, string[] args) { return false; }
    }
}
