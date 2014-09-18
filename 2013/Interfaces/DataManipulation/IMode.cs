using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PlugInsInterfaces.DataManipulation
{
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IMode : IXmlObj
    {
        /// <summary>
        /// The name for this gas 
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Name { get; set; }
        /// <summary>
        /// Unique ID for the gas
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Id { get; set; }
    }
}
