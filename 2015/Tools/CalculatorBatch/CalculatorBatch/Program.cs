using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greet.UnitLib3;
using Greet.DataStructureV4;
using Greet.Model;
using Greet.DataStructureV4.Entities;
using Greet.DataStructureV4.Interfaces;
using Greet.DataStructureV4.ResultsStorage;
using System.Data;
using System.Threading;
using System.Reflection;

namespace CalculatorBatch
{
    /// <summary>
    /// This program loads a data file, runs the calculations and exports the results in a text file
    /// 
    /// Example
    /// CalculatorBatch.exe "Default 2015.greet" 1995,2000,2005,2010,2015,2020,2025,2030,2035,2040 p5555,m0,p1
    /// 
    /// Will run the simulations for all the years 1995 to 2040 with a 5 year interval and save the results for the pathway which has the id 5555, the mix with the id=0 and the mix with the id=1
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Parsing given parameters...");
            //Using the filename provided as the first argument
            string fileName = args[0];
            
            //Using the years defined in the second argument
            string years = args[1];
            string[] yearsSplit = years.Split(',');
            List<int> yearsList = new List<int>();
            int temp;
            foreach (string y in yearsSplit)
                if (int.TryParse(y, out temp))
                    yearsList.Add(temp);

            //Using the pathways and mix id provided in the third argument
            string pm = args[2];
            string[] pms = pm.Split(',');
            List<InputResourceReference> inRef = new List<InputResourceReference>();
            foreach (string s in pms)
            {
                if (s[0] == 'p')
                {//this is a pathway reference 
                    if (int.TryParse(s.Substring(1, s.Length - 1), out temp))
                    {
                        InputResourceReference pRef = new InputResourceReference(-1, temp, Enumerators.SourceType.Pathway);
                        inRef.Add(pRef);
                    }
                }
                else if (s[0] == 'm')
                {//this is a mix reference 
                    if (int.TryParse(s.Substring(1, s.Length - 1), out temp))
                    {
                        InputResourceReference mRef = new InputResourceReference(-1, temp, Enumerators.SourceType.Mix);
                        inRef.Add(mRef);
                    }
                }
            }

            Console.WriteLine("Building units context...");
            //Build units context before loading the database
            Units.BuildContext();

            Console.WriteLine("Loading datafile...");
            //Loading the database
            GProject project = new GProject();
            project.Load(fileName);

            //Creating a new instance of a dictionary used to store results of the simulations
            Dictionary<InputResourceReference, Dictionary<int, Results>> savedResults = new Dictionary<InputResourceReference, Dictionary<int, Results>>();   

            //Running simulations for every provided years
            foreach (int simulationYear in yearsList)
            {
                Console.WriteLine("Running calculations for year " + simulationYear);
                //Set the current year for simulations
                BasicParameters.SelectedYear = project.Dataset.ParametersData.CreateUnregisteredParameter(project.Dataset, "", simulationYear);
                Calculator calc = new Calculator();
               
                //Run the simulations for the loaded project and defined year, we need to wait completion as the RunCalculationMethod is Async
                var manualEvent = new ManualResetEvent(false);
                calc.CalculationDoneEvent += () => manualEvent.Set();
                calc.RunCalculations(project);
                manualEvent.WaitOne();
             
                //Loop over all the pathways and mixes ID that we wish to save
                foreach (InputResourceReference pathMixToSave in inRef)
                {
                    if (!savedResults.ContainsKey(pathMixToSave))
                        savedResults.Add(pathMixToSave, new Dictionary<int, Results>());
                    if(!savedResults[pathMixToSave].ContainsKey(simulationYear))
                    {
                        //Pull the results and add them to the dictionary used to store results
                        Results results;
                        if(pathMixToSave.SourceType == Enumerators.SourceType.Pathway)
                            results = project.Dataset.PathwaysData[pathMixToSave.SourceMixOrPathwayID].getMainOutputResults().Results;
                        else
                            results = project.Dataset.MixesData[pathMixToSave.SourceMixOrPathwayID].getMainOutputResults().Results;
                        savedResults[pathMixToSave].Add(simulationYear, results);
                    }
                }
            }

            //Export all the desired results to an Excel spreadsheet
            Console.WriteLine("Export all selected results...");

            foreach (KeyValuePair<InputResourceReference, Dictionary<int, Results>> pair in savedResults)
            {
                DataTable dt = new DataTable();
                List<string> resGroups = new List<string>() { "Total Energy", "Fossil Fuel", "Coal Fuel", "Natural Gas Fuel", "Petroleum Fuel", "Water" };
                List<string> pollutants = new List<string>() { "VOC", "CO", "NOx", "PM10", "PM2.5", "SOx", "BC", "POC", "CH4", "N2O", "CO2" };
                List<string> polGroups = new List<string>() { "GHG-100" };
                List<string> urbanPoll = new List<string>() { "VOC", "CO", "NOx", "PM10", "PM2.5", "SOx", "BC", "POC", "CH4", "N2O" };

                dt.Columns.Add("Items");
                foreach (int simulationYear in pair.Value.Keys)
                    dt.Columns.Add(simulationYear.ToString());

                List<string> rowString = new List<string>();
                foreach (string resGrp in resGroups)
                {
                    rowString = new List<string>();
                    rowString.Add(resGrp);
                    foreach (int simulationYear in pair.Value.Keys)
                    {
                        if (resGrp == "Total Energy")
                        {
                            LightValue totalE = pair.Value[simulationYear].wellToProductEnem.materialsAmounts.TotalEnergy();
                            rowString.Add(totalE.ToString());
                        }
                        else
                        {
                            Dictionary<int, IValue> resGroupes = pair.Value[simulationYear].WellToProductResourcesGroups(project.Dataset);
                            int resGrpId = project.Dataset.ResourcesData.Groups.Values.Single(item => item.Name == resGrp).Id;
                            rowString.Add(resGroupes[resGrpId].ToString());
                        }
                    }
                    dt.Rows.Add(rowString.ToArray());
                }
                

                foreach (string poll in pollutants)
                {
                    rowString = new List<string>();
                    rowString.Add(poll);
                    foreach (int simulationYear in pair.Value.Keys)
                    {
                        int polId = project.Dataset.GasesData.Values.Single(item => item.Name == poll).Id;
                        rowString.Add(pair.Value[simulationYear].wellToProductEnem.emissions[polId].ToString());
                    }
                    dt.Rows.Add(rowString.ToArray());
                }
                
                
                foreach (string resGrp in polGroups)
                {
                    rowString = new List<string>();
                    rowString.Add(resGrp);
                    foreach (int simulationYear in pair.Value.Keys)
                    {
                        Dictionary<int, IValue> emGroupes = pair.Value[simulationYear].WellToProductEmissionsGroups(project.Dataset);
                        int grpId = project.Dataset.GasesData.Groups.Values.Single(item => item.Name == resGrp).Id;
                        rowString.Add(emGroupes[grpId].ToString());
                    }
                    dt.Rows.Add(rowString.ToArray());
                }
                

                foreach (string poll in pollutants)
                {
                    rowString = new List<string>();
                    rowString.Add(poll);
                    foreach (int simulationYear in pair.Value.Keys)
                    {
                        int polId = project.Dataset.GasesData.Values.Single(item => item.Name == poll).Id;
                        rowString.Add(pair.Value[simulationYear].wellToProductUrbanEmission[polId].ToString());
                    }
                    dt.Rows.Add(rowString.ToArray());
                }
                

                string value = ConvertDataTableToString(dt);
                System.IO.File.WriteAllText("Results-" + pair.Key.SourceType.ToString() + "-" + pair.Key.SourceMixOrPathwayID.ToString() + ".txt", value);
            }
        }

        public static string ConvertDataTableToString(DataTable dataTable)
        {
            var output = new StringBuilder();

            // Write Column titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var text = dataTable.Columns[i].ColumnName;
                output.Append(text + "\t");
            }
            output.Append("\n");

            // Write Rows
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var text = row[i].ToString();
                    output.Append(text + "\t");
                }
                output.Append("\n");
            }
            string fullString = output.ToString();
            return fullString.Replace("\t\n", Environment.NewLine);
        }
    }
}
