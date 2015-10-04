using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Greet.DataStructureV4.Interfaces;
using Greet.Model.Interfaces;
using Greet.DataStructureV4.Entities;

namespace Greet.Plugins.Example4
{
    public partial class DataHelperForm : Form
    {
        IDataHelper _dataHelper = null;
        IGREETController _controller = null;
        
        public DataHelperForm()
        {
            InitializeComponent();
        }

        public DataHelperForm(IDataHelper dataHelper, IGREETController controller) : this()
        {
            _dataHelper = dataHelper;
            _controller = controller;
        }

        /// <summary>
        /// Creates a stationary process with a few inputs and outputs, then insert this process to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewStationaryProcess_Click(object sender, EventArgs e)
        {
            //Hardcoded crude oil resource ID, could be fetched from the IData class instead
            int CRUDE_RES_ID = 23;
            //Hardcoded mix for crude oil production, could be fectched from the IData class instead
            int CRUDE_FOR_US_MIX = 0;
            //Hardcoded pathway id for conventional crude recovery, could be fetched from the IData class instead
            int CRUDE_RECOVERY_PROCESS = 34;
            //Approximation of a BTU International in Joules
            double BTU = 1055.5;
            //Technology
            int TECH_BOIL = 230106;

            //Creates an instance of a process
            IProcess process = _dataHelper.CreateNewProcess(0, "Example4 Stationary Process");

            //Creates instances of inputs and outputs to be used in that stationary process
            IInput input = _dataHelper.CreateNewInput(CRUDE_RES_ID, BTU / 2, "joules", 3, CRUDE_FOR_US_MIX);
            bool success = _dataHelper.InputAddTechnology(input, TECH_BOIL, 1);
            IInput input2 = _dataHelper.CreateNewInput(CRUDE_RES_ID, BTU / 2, "joules", 2, CRUDE_RECOVERY_PROCESS);
            IIO mainOutput = _dataHelper.CreateNewMainOutput(CRUDE_RES_ID, 1055, "joules");
            IIO coProduct = _dataHelper.CreateNewCoProduct(CRUDE_RES_ID, 0, "joules");

            //Adds all inputs and outputs to the process
            success &= _dataHelper.ProcessAddInput(process, input, false);
            success &= _dataHelper.ProcessAddInput(process, input2, false);
            success &= _dataHelper.ProcessAddOrUpdateOutput(process, coProduct);
            success &= _dataHelper.ProcessAddOrUpdateOutput(process, mainOutput);

            //Inserts the complete process to the current dataset
            success &= _dataHelper.DataInsertOrUpdateProcess(process);

            if(success)
                MessageBox.Show("Process named : '" + process.Name + "' inserted in dataset");
            else
                MessageBox.Show("Failure");

        }

        /// <summary>
        /// Creates a transportation process with a few steps, then insert this process to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewTransportationProcess_Click(object sender, EventArgs e)
        {
            //Hardcoded crude oil resource ID, could be fetched from the IData class instead
            int CRUDE_RES_ID = 23;

            //Creates an instance of a process
            IProcess process = _dataHelper.CreateNewProcess(1, "Example4 Transportation Process");

            //Set the resource being transported by the process. It is necessary to do that before inserting steps
            _dataHelper.TransportationSetResource(process, CRUDE_RES_ID);

            //Creates instances of transportation steps
            ITransportationStep step1 = _dataHelper.CreateNewTStep(1, 1, 100);
            ITransportationStep step2 = _dataHelper.CreateNewTStep(1, 1, 100);

            //Adds steps to the transportation step
            bool success = _dataHelper.TransportationAddTStep(process, step1);
            success &= _dataHelper.TransportationAddTStep(process, step2);

            //Retrieves locations from the current dataset, it's not relevant to the calculation to know which locations are used, only for visualisation
            ILocation locationA = _controller.CurrentProject.Data.Locations.ValueForKey(3);
            ILocation locationB = _controller.CurrentProject.Data.Locations.ValueForKey(1);
            ILocation locationC = _controller.CurrentProject.Data.Locations.ValueForKey(2);

            //Connecting the steps and locoations together to form the transportation process
            success &= _dataHelper.TransportationAddConnector(locationA, step1); // A -> Step1
            success &= _dataHelper.TransportationAddConnector(step1, locationB); // Step1 -> B
            success &= _dataHelper.TransportationAddConnector(locationB, step2); // B -> Step2
            success &= _dataHelper.TransportationAddConnector(step2, locationC); // Step2 -> C

            //Insert the completed transportation process in the current dataset
            success &= _dataHelper.DataInsertOrUpdateProcess(process);

            if (success)
                MessageBox.Show("Process named : '" + process.Name + "' inserted in dataset");
            else
                MessageBox.Show("Failure");
        }

        /// <summary>
        /// Creates a new pathway with a few processes, then inserts that pathway in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewPathway_Click(object sender, EventArgs e)
        {
            //Hardcoded crude oil resource ID, could be fetched from the IData class instead
            int CRUDE_RES_ID = 23;
            //Hardcoded process model id
            int PROC_ID_REC = 200, PROC_ID_TRA = 1000, PROC_ID_STO = 201;

            //Creates an instance of a pathway
            IPathway path = _dataHelper.CreateNewPathway("Example4 Pathway");

            //Retrieves process models from the current dataset
            IProcess procr = _controller.CurrentProject.Data.Processes.ValueForKey(PROC_ID_REC);
            IProcess proct = _controller.CurrentProject.Data.Processes.ValueForKey(PROC_ID_TRA);
            IProcess procs = _controller.CurrentProject.Data.Processes.ValueForKey(PROC_ID_STO);

            //Add processes models to the pathway, vertices will be created for each process model added to the pathway
            IVertex vertexR = _dataHelper.PathwayAddModel(path, procr);
            IVertex vertexT = _dataHelper.PathwayAddModel(path, proct);
            IVertex vertexS = _dataHelper.PathwayAddModel(path, procs);

            //finds input and outputs ID from the model, then add a connector in the pathway
            IInput inpT = proct.FlattenInputList.First();
            IIO outR = procr.FlattenAllocatedOutputList.First();
            bool success = _dataHelper.PathwayAddConnector(path, vertexR.ID, outR.Id, vertexT.ID, inpT.Id);

            //finds input and outputs ID from the model, then add a connector in the pathway
            IInput inpS = procs.FlattenInputList.First();
            IIO outT = proct.FlattenAllocatedOutputList.First();
            success &= _dataHelper.PathwayAddConnector(path, vertexT.ID, outT.Id, vertexS.ID, inpS.Id);

            //creates a pathway output then connect the last process in the pathway to that output
            IIO pathOut = _dataHelper.PathwayCreateOutput(path, CRUDE_RES_ID);
            success &= _dataHelper.PathwaySetMainOutput(path, pathOut.Id);
            IIO outS = procs.FlattenAllocatedOutputList.First();
            success &= _dataHelper.PathwayAddConnector(path, vertexS.ID, outS.Id, pathOut.Id);

            //inserts the pathway to the current dataset
            success &= _dataHelper.DataInsertOrUpdatePathway(path);

            if (success)
                MessageBox.Show("Pathway named : '" + path.Name + "' inserted in dataset");
            else
                MessageBox.Show("Failure");
        }

        /// <summary>
        /// Creates a new mix with a few pathways and mixes references, then inserts that mix in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewMix_Click(object sender, EventArgs e)
        {
            //Hardcoded crude oil resource ID, could be fetched from the IData class instead
            int CRUDE_RES_ID = 23;
            //Hardcoded pathway IDs
            int CONV_CRD = 34, OILS_CRD = 36;
            //Hardcoded mix ID (for the fun of it)
            int MIX_CRD = 0;

            //Creating a new instance of a mix
            IMix mix = _dataHelper.CreateNewMix("Example4 Mix");

            //Set the resource ID produced by this mix
            _dataHelper.MixSetResource(mix, CRUDE_RES_ID);

            //Retriving pathways to be added to the mix
            IPathway path1 = _controller.CurrentProject.Data.Pathways.ValueForKey(CONV_CRD);
            IPathway path2 = _controller.CurrentProject.Data.Pathways.ValueForKey(OILS_CRD);
            IMix mix1 = _controller.CurrentProject.Data.Mixes.ValueForKey(MIX_CRD);

            //Adding the different production items to the mix and define their shares
            bool success = _dataHelper.MixAddFeed(mix, path1, 0.3);
            success &= _dataHelper.MixAddFeed(mix, path2, 0.6);
            success &= _dataHelper.MixAddFeed(mix, mix1, 0.1);

            //Inserting the newly created mix to the database
            success &= _dataHelper.DataInsertOrUpdateMix(mix);

            if (success)
                MessageBox.Show("Mix named : '" + mix.Name + "' inserted in dataset");
            else
                MessageBox.Show("Failure");

        }
        
        /// <summary>
        /// Creates a new technology with a couple of emission factors for different years, then inserts that mix in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewTechnology_Click(object sender, EventArgs e)
        {
            //Hardcoded crude oil resource ID, could be fetched from the IData class instead
            int CRUDE_RES_ID = 23;

            //Creating new instance of a technology
            ITechnology technology = _dataHelper.CreateNewTechnology("Example4 Technology");

            //Sets the resource being combusted/used by that technology
            bool success = _dataHelper.TechnologySetResource(technology, CRUDE_RES_ID);

            //Adds a couple of yers for the emissions that will be created
            success &= _dataHelper.TechnologyAddYear(technology, 2010);
            success &= _dataHelper.TechnologyAddYear(technology, 2014);

            //Adds a couple of emission factors for different gases
            IGas gas = _controller.CurrentProject.Data.Gases.AllValues.First();
            IGas gas2 = _controller.CurrentProject.Data.Gases.AllValues.Last();
            success &= _dataHelper.TechnologyAddEmission(technology, gas);
            success &= _dataHelper.TechnologyAddEmission(technology, gas2);

            //Retrieves an emission factor to observe it's value
            IParameter ef = _dataHelper.TechnologyGetEF(technology, 2014, gas.Id);

            //Sets the emission factor for a specific year and pollutant
            success &= _dataHelper.TechnologySetEF(technology, 2014, gas.Id, ef.UnitGroupName, 1);

            //Adds the technology to the database
            success &= _dataHelper.DataInsertOrUpdateTechnology(technology);

            if (success)
                MessageBox.Show("Technology named : '" + technology.Name + "' inserted in dataset");
            else
                MessageBox.Show("Failure");

        }
        
        /// <summary>
        /// Creates a new resource with some of the physical properties defined and a membership selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewResource_Click(object sender, EventArgs e)
        {
            //Creating a new instance of a resource
            IResource resource = _dataHelper.CreateNewResource("Example4 Resource");
            bool success = _dataHelper.ResourceSetNotes(resource, "Notes for this automatically created resource");
            success &= _dataHelper.ResourceSetState(resource, Resources.PhysicalState.liquid);
            success &= _dataHelper.ResourceSetIsPrimary(resource, true);
            success &= _dataHelper.ResourceSetCRatio(resource, 0.25);//25% by mass
            success &= _dataHelper.ResourceSetSRatio(resource, 0.26);//26% by mass
            success &= _dataHelper.ResourceSetMarketValue(resource, "us_dollars/joule", 10);
            success &= _dataHelper.ResourceSetLHV(resource, "grams/joule", 1);
            //success &= _dataHelper.ResourceUnsetLHV(resource);
            success &= _dataHelper.ResourceSetHHV(resource, "grams/joule", 1.2);
            //success &= _dataHelper.ResourceUnsetHVV(resource);
            success &= _dataHelper.ResourceSetDensity(resource, "grams/cu_meter", 1000000);
            //success &= _dataHelper.ResourceUnsetDensity(resource);
            success &= _dataHelper.DataInsertOrUpdateResource(resource);

            if (success)
                MessageBox.Show("Resource named : '" + resource.Name + "' inserted in dataset");
            else
                MessageBox.Show("Failure");

        }

        /// <summary>
        /// Creates a new pollutant with some characteristics defined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewPollutant_Click(object sender, EventArgs e)
        {
            IGas pollutant = _dataHelper.CreateNewPollutant("Example4 Gas", "Some notes");
            bool success = _dataHelper.PollutantSetCRatio(pollutant, 0.56); //56% by mass
            success &= _dataHelper.PollutantSetSRatio(pollutant, 0.003); //0.3% by mass
            success &= _dataHelper.PollutantSetGWP100(pollutant, 1.20); //120% relatively to CO2
            success &= _dataHelper.DataInsertOrUpdatePollutant(pollutant);

            if (success)
                MessageBox.Show("Pollutant named : '" + pollutant.Name + "' inserted in dataset");
            else
                MessageBox.Show("Failure");
        }

        private void buttonNewMode_Click(object sender, EventArgs e)
        {
            IAMode pipeBased = _dataHelper.CreateNewMode(3, "Example 4 pipeline", "For API test purposes");
            bool success = _dataHelper.ModePipelineInsertOrUpdateEI(pipeBased, 4, "btu/mi/ton", 25);
            success &= _dataHelper.ModeAddEnergySource(pipeBased, 1, 23, 1, 341, 2, 230106, -1);
            IModeFuelShares fs = _dataHelper.ModeCreateNewFuelShare(pipeBased, "Second fuel share");
            success &= _dataHelper.ModeInsertFuelShare(pipeBased, fs);
            success &= _dataHelper.ModeAddEnergySource(pipeBased, fs.Id, 59, 1, 121, 2, 7, -1);
            success &= _dataHelper.DataInsertOrUpdateMode(pipeBased);

            IAMode tankerBased = _dataHelper.CreateNewMode(1, "Example 4 tanker", "For API test purposes");
            success &= _dataHelper.ModeTankerSetAvgSpd(tankerBased, "mi/hr", 20);
            success &= _dataHelper.ModeTankerSetLoadFactorTo(tankerBased, 0.75);
            success &= _dataHelper.ModeTankerSetLoadFactorFrom(tankerBased, 0.50);
            success &= _dataHelper.ModeTankerSetTypicalFuelConsumption(tankerBased, "kg/mmBtu", 43.961);
            success &= _dataHelper.ModeTankerSetTypicalHP(tankerBased, "W", 6670000.974);
            success &= _dataHelper.ModeTankerSetHPPayloadFactor(tankerBased, "W/kg", 0.000081886);
            success &= _dataHelper.ModeTankerInsertOrUpdatePayload(tankerBased, 23, "ton", 220000);
            success &= _dataHelper.ModeAddEnergySource(tankerBased, 1, 23, 1, 341, 2, 230106, -1);
            success &= _dataHelper.DataInsertOrUpdateMode(tankerBased);

            IAMode truckBased = _dataHelper.CreateNewMode(2, "Example 4 truck", "For API test purposes");
            success &= _dataHelper.ModeTruckSetFuelConsumptionTo(truckBased, "gal/mi", 6);
            success &= _dataHelper.ModeTruckSetFuelConsumptionFrom(truckBased, "gal/mi", 6);
            success &= _dataHelper.ModeTruckInsertOrUpdatePayload(truckBased, 23, "ton", 22);
            success &= _dataHelper.ModeAddEnergySource(truckBased, 1, 23, 1, 341, 2, 230106, -1);
            success &= _dataHelper.DataInsertOrUpdateMode(truckBased);

            IAMode railBased = _dataHelper.CreateNewMode(4, "Example 4 rail", "For API test purposes");
            success &= _dataHelper.ModeRailSetAvgSpd(railBased, "mi/hr", 20);
            success &= _dataHelper.ModeRailSetEI(railBased, 4, "btu/mi/ton", 25);
            success &= _dataHelper.DataInsertOrUpdateMode(railBased);

            IAMode magicBased = _dataHelper.CreateNewMode(5, "Example 4 magic", "For API test purposes");
            success &= _dataHelper.DataInsertOrUpdateMode(magicBased);

            if (success)
                MessageBox.Show("New modes of each types inserted in dataset");
            else
                MessageBox.Show("Failure");
        }

        private void buttonModifySTInput_Click(object sender, EventArgs e)
        {

        }

        private void buttonModifySTOutput_Click(object sender, EventArgs e)
        {

        }

        private void buttonModifySTOtherEmission_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddTechnologyToInput_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddProcessToPathway_Click(object sender, EventArgs e)
        {

        }

        private void buttonDeleteProcessFromPathway_Click(object sender, EventArgs e)
        {

        }

        private void buttonModifyMainOutputOfPathway_Click(object sender, EventArgs e)
        {

        }

        private void buttonModifyPathwayInMix_Click(object sender, EventArgs e)
        {

        }
    }
}
