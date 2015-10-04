using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WaterReplace
{
    /// <summary>
    /// This simple program allows one to replace the input resource reference easily over the .greet file
    /// 
    /// Example
    /// InputReplacer.exe "Default 2015.greet"
    /// 
    /// Will open the file, and ask for the resource id to replace in the stationary processes inputs
    /// It will then find all inputs that are using (refering) to that resource ID and ask the user to replace or not
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify a file after the command like water.exe 'default 2015.greet'");
                return;
            }

            if (new FileInfo(args[0]).Exists == false)
            {
                Console.WriteLine("The specified file does not exists");
                return;
            }

            int search = -1;
            int replaceWith = -1;

            while (search == -1)
            {
                Console.WriteLine("Specify resource ID to look for:");
                string line = Console.ReadLine();
                if (int.TryParse(line, out search))
                    break;
            }

            while (replaceWith == -1)
            {
                Console.WriteLine("Specify resource ID to replace with:");
                string line = Console.ReadLine();
                if (int.TryParse(line, out replaceWith))
                    break;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(args[0]);
            bool doItAll = false;
            foreach (XmlNode node in xmlDoc.SelectNodes("greet/data/processes/stationary"))
            {
                string processName = node.Attributes["name"].Value;

                List<XmlNode> inputNodes = new List<XmlNode>();
                foreach (XmlNode input in node.SelectNodes("input"))
                    inputNodes.Add(input);
                foreach (XmlNode input in node.SelectNodes("group/shares/input"))
                    inputNodes.Add(input);

                bool isContainingSearch = false;
                foreach (XmlNode n in inputNodes)
                    isContainingSearch |= n.Attributes["ref"].Value.Equals(search.ToString());

                if (isContainingSearch)
                {
                    Console.Write("Process named: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(processName);
                    char answer = 'n';
                    if (!doItAll)
                    {
                        Console.ResetColor();
                        Console.WriteLine(", has input(s) with reference " + search.ToString() + " replace? (y/n/a)");
                        ConsoleKeyInfo key = Console.ReadKey();
                        answer = key.KeyChar;

                        if (answer == 'a')
                        {
                            doItAll = true;
                            answer = 'y';
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.ResetColor();
                        answer = 'y';
                    }

                    if (answer == 'y')
                    {
                        foreach (XmlNode n in inputNodes)
                            if (n.Attributes["ref"].Value == search.ToString())
                                n.Attributes["ref"].Value = replaceWith.ToString();
                        xmlDoc.Save(args[0]);
                    }
                }
            }

            Console.ResetColor();
        }
    }
}
