using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PlugInsInterfaces.DataManipulation
{
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


        //[Obfuscation(Feature = "renaming", Exclude = true)]
        
        //[Obfuscation(Feature = "renaming", Exclude = true)]
        
        //[Obfuscation(Feature = "renaming", Exclude = true)]


    }
}
