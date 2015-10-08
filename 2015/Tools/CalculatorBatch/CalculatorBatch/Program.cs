using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;
using System.Reflection;
using Greet.DataStructureV4.ResultsStorage;
using Greet.DataStructureV4.Entities;
using Greet.DataStructureV4;
using Greet.DataStructureV4.Interfaces;
using Greet.Model;
using Greet.UnitLib3;
using Greet.ConvenienceLib;

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
        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            #region preproceiing parsing the user inputs
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
            #endregion 

            #region loading units file and data file
            Console.WriteLine("Building units context...");
            //Build units context before loading the database
            Units.BuildContext();

            Console.WriteLine("Loading datafile...");
            //Loading the database
            GProject project = new GProject();
            project.Load(fileName);
            #endregion

            #region preprocessing the pathways/mixes we want to record by finding their main output resource
            //Assign main output resource IDs to all the inputsResourceReferences
            foreach (InputResourceReference iref in inRef)
            {
                if (iref.SourceType == Greet.DataStructureV4.Interfaces.Enumerators.SourceType.Pathway)
                {
                    if (project.Dataset.PathwaysData.ContainsKey(iref.SourceMixOrPathwayID))
                        iref.ResourceId = project.Dataset.PathwaysData[iref.SourceMixOrPathwayID].MainOutputResourceID;
                }
                else if (iref.SourceType == Greet.DataStructureV4.Interfaces.Enumerators.SourceType.Mix)
                { 
                    if (project.Dataset.MixesData.ContainsKey(iref.SourceMixOrPathwayID))
                        iref.ResourceId = project.Dataset.MixesData[iref.SourceMixOrPathwayID].MainOutputResourceID;
                }
            }
            #endregion

            #region running the calculations for each year and storing the results
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
                            results = Convenience.Clone(project.Dataset.PathwaysData[pathMixToSave.SourceMixOrPathwayID].getMainOutputResults().Results);
                        else
                            results = Convenience.Clone(project.Dataset.MixesData[pathMixToSave.SourceMixOrPathwayID].getMainOutputResults().Results);
                        savedResults[pathMixToSave].Add(simulationYear, results);
                    }
                }
            }
            #endregion

            #region exporting all the results in a text file per pathway and per mix
            //Export all the desired results to an Excel spreadsheet
            Console.WriteLine("Export all selected results...");
            string preferedMass = "g";
            string preferedEnergy = "Btu";
            string preferedVolume = "gal";

            foreach (KeyValuePair<InputResourceReference, Dictionary<int, Results>> pair in savedResults)
            {
                DataTable dt = new DataTable();
                List<string> resGroups = new List<string>() { "Total Energy", "Fossil Fuel", "Coal Fuel", "Natural Gas Fuel", "Petroleum Fuel", "Water" };
                List<string> pollutants = new List<string>() { "VOC", "CO", "NOx", "PM10", "PM2.5", "SOx", "BC", "POC", "CH4", "N2O", "CO2", "CO2_Biogenic" };
                List<string> polGroups = new List<string>() { "GHG-100" };
                List<string> urbanPoll = new List<string>() { "VOC", "CO", "NOx", "PM10", "PM2.5", "SOx", "BC", "POC", "CH4", "N2O" };

                Results resultsFU = pair.Value.Values.FirstOrDefault();
                string functionalUnit = "Per ";
                if (resultsFU != null)
                    functionalUnit += GetPreferedVisualizationFunctionalUnitString(project.Dataset, resultsFU, pair.Key.ResourceId);

                dt.Columns.Add("Items " + functionalUnit);
                foreach (int simulationYear in pair.Value.Keys)
                    dt.Columns.Add(simulationYear.ToString());
                List<string> rowString = new List<string>();

                #region total energy and energy groups
                foreach (string resGrp in resGroups)
                {
                    rowString = new List<string>();
                    if (resGrp=="Water")
                        rowString.Add(resGrp+" ("+preferedVolume+")");
                    else
                        rowString.Add(resGrp + " (" + preferedEnergy + ")");

                    foreach (int simulationYear in pair.Value.Keys)
                    {
                        Results results = pair.Value[simulationYear];
                        double amountRatio = GetFunctionalRatio(project.Dataset, results, pair.Key.ResourceId);

                        if (resGrp == "Total Energy")
                        {
                            LightValue totalE = results.wellToProductEnem.materialsAmounts.TotalEnergy();
                            rowString.Add(NiceValueWithAttribute(totalE * amountRatio, preferedEnergy));
                        }
                        else
                        {
                            Dictionary<int, IValue> resGroupes = results.WellToProductResourcesGroups(project.Dataset);
                            Group resGrpSelected = project.Dataset.ResourcesData.Groups.Values.SingleOrDefault(item => item.Name == resGrp);
                            if (resGrpSelected != null)
                            {
                                int resGrpId = resGrpSelected.Id;
                                LightValue groupValue = new LightValue(resGroupes[resGrpId].Value, resGroupes[resGrpId].UnitExpression);
                                if (groupValue.Dim == DimensionUtils.ENERGY)
                                    rowString.Add(NiceValueWithAttribute(groupValue * amountRatio, preferedEnergy));
                                else
                                    rowString.Add(NiceValueWithAttribute(groupValue * amountRatio, preferedVolume));
                            }
                            else
                                rowString.Add("0");
                        }
                    }
                    dt.Rows.Add(rowString.ToArray());
                }
                #endregion

                #region wtp emissions
                foreach (string poll in pollutants)
                {
                    rowString = new List<string>();
                    rowString.Add(poll+" ("+preferedMass+")");
                    foreach (int simulationYear in pair.Value.Keys)
                    {
                        Results results = pair.Value[simulationYear];
                        double amountRatio = GetFunctionalRatio(project.Dataset, results, pair.Key.ResourceId);

                        int polId = project.Dataset.GasesData.Values.Single(item => item.Name == poll).Id;
                        rowString.Add(NiceValueWithAttribute(
                            new LightValue(results.wellToProductEnem.emissions[polId], DimensionUtils.MASS) * amountRatio
                            , preferedMass));
                    }
                    dt.Rows.Add(rowString.ToArray());
                }
                #endregion

                #region wtp Groups (here only GHG 100)
                foreach (string resGrp in polGroups)
                {
                    rowString = new List<string>();
                    rowString.Add(resGrp+ " ("+ preferedMass+")");
                    foreach (int simulationYear in pair.Value.Keys)
                    {
                        Results results = pair.Value[simulationYear];
                        double amountRatio = GetFunctionalRatio(project.Dataset, results, pair.Key.ResourceId);

                        Dictionary<int, IValue> emGroupes = pair.Value[simulationYear].WellToProductEmissionsGroups(project.Dataset);
                        Group resGrpSelected = project.Dataset.GasesData.Groups.Values.SingleOrDefault(item => item.Name == resGrp);
                        if (resGrpSelected != null)
                        {
                            int grpId = resGrpSelected.Id;
                            rowString.Add(NiceValueWithAttribute(new LightValue(emGroupes[grpId].Value, emGroupes[grpId].UnitExpression) * amountRatio, preferedMass));
                        }
                        else
                            rowString.Add("0");
                    }
                    dt.Rows.Add(rowString.ToArray());
                }
                #endregion

                #region urban emissions
                foreach (string poll in pollutants)
                {
                    rowString = new List<string>();
                    rowString.Add("Urban " +poll+" ("+preferedMass+")");
                    foreach (int simulationYear in pair.Value.Keys)
                    {
                        Results results = pair.Value[simulationYear];
                        double amountRatio = GetFunctionalRatio(project.Dataset, results, pair.Key.ResourceId);
                        
                        int polId = project.Dataset.GasesData.Values.Single(item => item.Name == poll).Id;
                        rowString.Add(NiceValueWithAttribute(new LightValue(results.wellToProductUrbanEmission[polId], DimensionUtils.MASS) * amountRatio, preferedMass));
                    }
                    dt.Rows.Add(rowString.ToArray());
                }
                #endregion


                string value = ConvertDataTableToString(dt);
                System.IO.File.WriteAllText("Results-" + pair.Key.SourceType.ToString() + "-" + pair.Key.SourceMixOrPathwayID.ToString() + ".txt", value);
            }
            #endregion
        }

        /// <summary>
        /// Creates a human readable string representing a data table
        /// </summary>
        /// <param name="dataTable">Data table to be converted as a string</param>
        /// <returns>String representing the data table</returns>
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

        /// <summary>
        /// Returns the coefficient to be used in order to display the results according to the preferences of the IResult
        /// object used to create the instance of this object. Returns the prefered functional unit divided by the functional unit.
        /// 
        /// In case of the FunctionalUnit is null or the PreferedUnit is null, this method returns a ratio of 1. This is usefull to
        /// display the Inputs results and the TransportationSteps results which are accounted for all outputs and not a specific one.
        /// In these cases instead of defining a PreferedUnit for each output we prefer to define none to make things simpler (see InputResult.GetResults)
        /// 
        /// Before display all results must be multiplied by this coefficient
        /// 
        /// May return a NullReferenceExeption if the IResult object does not define FunctinoalUnit, PreferedDisplayedUnit or PreferedDisplayedAmount
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        internal static double GetFunctionalRatio(GData data, Results results, int producedResourceId)
        {
            if (results != null && results.CustomFunctionalUnitPreference != null && results.CustomFunctionalUnitPreference.PreferredUnitExpression != null)
            {
                LightValue functionalUnit = new LightValue(1.0, results.BottomDim);
                LightValue preferedFunctionalUnit = GetPreferedVisualizationFunctionalUnit(data, results, producedResourceId);

                switch (preferedFunctionalUnit.Dim)
                {
                    case DimensionUtils.MASS: // HARDCODED
                        {
                            functionalUnit = data.ResourcesData[producedResourceId].ConvertToMass(functionalUnit);
                        } break;
                    case DimensionUtils.ENERGY: // HARDCODED
                        {
                            functionalUnit = data.ResourcesData[producedResourceId].ConvertToEnergy(functionalUnit);
                        } break;
                    case DimensionUtils.VOLUME: // HARDCODED
                        {
                            functionalUnit = data.ResourcesData[producedResourceId].ConvertToVolume(functionalUnit);
                        } break;
                }
                return preferedFunctionalUnit.Value / functionalUnit.Value;
            }
            else
                return 1;
        }

        /// <summary>
        /// Returns the functional unit to be used on display: User prefered, default million btu or functional unit of the
        /// process in case of the database does not contains enough information to convert to one million btu
        /// </summary>
        /// <param name="data"></param>
        /// <param name="results"></param>
        /// <param name="producedResourceId"></param>
        /// <returns></returns>
        internal static LightValue GetPreferedVisualizationFunctionalUnit(GData data, Results results, int producedResourceId)
        {
            LightValue preferedFunctionalUnit;
            if (results.CustomFunctionalUnitPreference.enabled)
                preferedFunctionalUnit = new LightValue(results.CustomFunctionalUnitPreference.Amount, results.CustomFunctionalUnitPreference.PreferredUnitExpression);
            else if (data.ResourcesData[producedResourceId].CanConvertTo(DimensionUtils.ENERGY, new LightValue(1.0, results.BottomDim)))
                preferedFunctionalUnit = new LightValue(1, "MJ");
            else
                preferedFunctionalUnit = new LightValue(1.0, results.BottomDim);
            return preferedFunctionalUnit;
        }

        /// <summary>
        /// Returns the functional unit to be used on display: User prefered, default million btu or functional unit of the
        /// process in case of the database does not contains enough information to convert to one million btu
        /// </summary>
        /// <param name="data"></param>
        /// <param name="results"></param>
        /// <param name="producedResourceId"></param>
        /// <returns></returns>
        internal static string GetPreferedVisualizationFunctionalUnitString(GData data, Results results, int producedResourceId)
        {
            string preferedFunctionalUnit = "";
            try
            {
                if (results.CustomFunctionalUnitPreference.enabled)
                    preferedFunctionalUnit = results.CustomFunctionalUnitPreference.Amount.ToString() + " " + results.CustomFunctionalUnitPreference.PreferredUnitExpression;
                else if (data.ResourcesData[producedResourceId].CanConvertTo(DimensionUtils.ENERGY, new LightValue(1.0, results.BottomDim)))
                    preferedFunctionalUnit = "1 MJ";
                else
                {
                    AQuantity qty = Units.QuantityList.ByDim(results.BottomDim);
                    preferedFunctionalUnit = "1 " + qty.SiUnit.Expression;
                }
            }
            catch { }

            return preferedFunctionalUnit;
        }

        internal static string NiceValueWithAttribute(LightValue value, string preferedExpression = "")
        {
            double automaticScalingSlope = 1;
            string overrideUnitAttribute = "";
            return GuiUtils.FormatSIValue(value.Value
                , 2 //DEFINES THE FORMATTING FOR VALUES
                , out overrideUnitAttribute
                , out automaticScalingSlope
                , false
                , 16 //DEFINES HOW MANY DIGITS YOU WANT TO SEE
                , value.Dim
                , preferedExpression);// +" " + overrideUnitAttribute;
        }
    }
}
