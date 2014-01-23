using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PlugInsInterfaces.DataManipulation
{
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public interface IResource : IXmlObj
    {
        /// <summary>
        /// Name for this resource as it is going to show on the graphical interface
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        string Name { get; set; }
        /// <summary>
        /// Unique ID for this resource among the resources
        /// </summary>
        [Obfuscation(Feature = "renaming", Exclude = true)]
        int Id { get; set; }
    }
}