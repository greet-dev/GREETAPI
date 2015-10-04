namespace Greet.Plugins.Example4
{
    partial class DataHelperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonNewStationaryProcess = new System.Windows.Forms.Button();
            this.buttonNewTransportationProcess = new System.Windows.Forms.Button();
            this.buttonNewPathway = new System.Windows.Forms.Button();
            this.buttonNewMix = new System.Windows.Forms.Button();
            this.buttonNewTechnology = new System.Windows.Forms.Button();
            this.buttonNewResource = new System.Windows.Forms.Button();
            this.buttonNewPollutant = new System.Windows.Forms.Button();
            this.buttonNewStep = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonNewStationaryProcess
            // 
            this.buttonNewStationaryProcess.Location = new System.Drawing.Point(9, 10);
            this.buttonNewStationaryProcess.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewStationaryProcess.Name = "buttonNewStationaryProcess";
            this.buttonNewStationaryProcess.Size = new System.Drawing.Size(131, 78);
            this.buttonNewStationaryProcess.TabIndex = 0;
            this.buttonNewStationaryProcess.Text = "Create New Stationary Process";
            this.buttonNewStationaryProcess.UseVisualStyleBackColor = true;
            this.buttonNewStationaryProcess.Click += new System.EventHandler(this.buttonNewStationaryProcess_Click);
            // 
            // buttonNewTransportationProcess
            // 
            this.buttonNewTransportationProcess.Location = new System.Drawing.Point(9, 93);
            this.buttonNewTransportationProcess.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewTransportationProcess.Name = "buttonNewTransportationProcess";
            this.buttonNewTransportationProcess.Size = new System.Drawing.Size(131, 78);
            this.buttonNewTransportationProcess.TabIndex = 1;
            this.buttonNewTransportationProcess.Text = "Create New Transportation Process";
            this.buttonNewTransportationProcess.UseVisualStyleBackColor = true;
            this.buttonNewTransportationProcess.Click += new System.EventHandler(this.buttonNewTransportationProcess_Click);
            // 
            // buttonNewPathway
            // 
            this.buttonNewPathway.Location = new System.Drawing.Point(9, 176);
            this.buttonNewPathway.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewPathway.Name = "buttonNewPathway";
            this.buttonNewPathway.Size = new System.Drawing.Size(131, 78);
            this.buttonNewPathway.TabIndex = 2;
            this.buttonNewPathway.Text = "Create New Pathway";
            this.buttonNewPathway.UseVisualStyleBackColor = true;
            this.buttonNewPathway.Click += new System.EventHandler(this.buttonNewPathway_Click);
            // 
            // buttonNewMix
            // 
            this.buttonNewMix.Location = new System.Drawing.Point(9, 258);
            this.buttonNewMix.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewMix.Name = "buttonNewMix";
            this.buttonNewMix.Size = new System.Drawing.Size(131, 78);
            this.buttonNewMix.TabIndex = 3;
            this.buttonNewMix.Text = "Create New Mix";
            this.buttonNewMix.UseVisualStyleBackColor = true;
            this.buttonNewMix.Click += new System.EventHandler(this.buttonNewMix_Click);
            // 
            // buttonNewTechnology
            // 
            this.buttonNewTechnology.Location = new System.Drawing.Point(145, 10);
            this.buttonNewTechnology.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewTechnology.Name = "buttonNewTechnology";
            this.buttonNewTechnology.Size = new System.Drawing.Size(131, 78);
            this.buttonNewTechnology.TabIndex = 5;
            this.buttonNewTechnology.Text = "Create New Technology";
            this.buttonNewTechnology.UseVisualStyleBackColor = true;
            this.buttonNewTechnology.Click += new System.EventHandler(this.buttonNewTechnology_Click);
            // 
            // buttonNewResource
            // 
            this.buttonNewResource.Location = new System.Drawing.Point(145, 93);
            this.buttonNewResource.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewResource.Name = "buttonNewResource";
            this.buttonNewResource.Size = new System.Drawing.Size(131, 78);
            this.buttonNewResource.TabIndex = 6;
            this.buttonNewResource.Text = "Create New Resource";
            this.buttonNewResource.UseVisualStyleBackColor = true;
            this.buttonNewResource.Click += new System.EventHandler(this.buttonNewResource_Click);
            // 
            // buttonNewPollutant
            // 
            this.buttonNewPollutant.Location = new System.Drawing.Point(145, 176);
            this.buttonNewPollutant.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewPollutant.Name = "buttonNewPollutant";
            this.buttonNewPollutant.Size = new System.Drawing.Size(131, 78);
            this.buttonNewPollutant.TabIndex = 7;
            this.buttonNewPollutant.Text = "Create New Pollutant";
            this.buttonNewPollutant.UseVisualStyleBackColor = true;
            this.buttonNewPollutant.Click += new System.EventHandler(this.buttonNewPollutant_Click);
            // 
            // buttonNewStep
            // 
            this.buttonNewStep.Location = new System.Drawing.Point(145, 258);
            this.buttonNewStep.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewStep.Name = "buttonNewStep";
            this.buttonNewStep.Size = new System.Drawing.Size(131, 78);
            this.buttonNewStep.TabIndex = 12;
            this.buttonNewStep.Text = "Create New Mode";
            this.buttonNewStep.UseVisualStyleBackColor = true;
            this.buttonNewStep.Click += new System.EventHandler(this.buttonNewMode_Click);
            // 
            // DataHelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 344);
            this.Controls.Add(this.buttonNewStep);
            this.Controls.Add(this.buttonNewPollutant);
            this.Controls.Add(this.buttonNewResource);
            this.Controls.Add(this.buttonNewTechnology);
            this.Controls.Add(this.buttonNewMix);
            this.Controls.Add(this.buttonNewPathway);
            this.Controls.Add(this.buttonNewTransportationProcess);
            this.Controls.Add(this.buttonNewStationaryProcess);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DataHelperForm";
            this.Text = "Example4_DataHeper";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNewStationaryProcess;
        private System.Windows.Forms.Button buttonNewTransportationProcess;
        private System.Windows.Forms.Button buttonNewPathway;
        private System.Windows.Forms.Button buttonNewMix;
        private System.Windows.Forms.Button buttonNewTechnology;
        private System.Windows.Forms.Button buttonNewResource;
        private System.Windows.Forms.Button buttonNewPollutant;
        private System.Windows.Forms.Button buttonNewStep;
    }
}