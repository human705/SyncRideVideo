namespace BuildCorrectionsList
{
    partial class frmOldRide
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
            this.dgViewOldRide = new System.Windows.Forms.DataGridView();
            this.btnReWriteSecs = new System.Windows.Forms.Button();
            this.lblVideoSecs = new System.Windows.Forms.Label();
            this.flowPanelBotomRow = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewOldRide)).BeginInit();
            this.flowPanelBotomRow.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgViewOldRide
            // 
            this.dgViewOldRide.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewOldRide.Location = new System.Drawing.Point(12, 12);
            this.dgViewOldRide.Name = "dgViewOldRide";
            this.dgViewOldRide.RowHeadersWidth = 51;
            this.dgViewOldRide.RowTemplate.Height = 24;
            this.dgViewOldRide.Size = new System.Drawing.Size(694, 472);
            this.dgViewOldRide.TabIndex = 0;
            // 
            // btnReWriteSecs
            // 
            this.btnReWriteSecs.Location = new System.Drawing.Point(3, 3);
            this.btnReWriteSecs.Name = "btnReWriteSecs";
            this.btnReWriteSecs.Size = new System.Drawing.Size(127, 44);
            this.btnReWriteSecs.TabIndex = 1;
            this.btnReWriteSecs.Text = "ReWrite Secs";
            this.btnReWriteSecs.UseVisualStyleBackColor = true;
            this.btnReWriteSecs.Click += new System.EventHandler(this.btnReWriteSecs_Click);
            // 
            // lblVideoSecs
            // 
            this.lblVideoSecs.Location = new System.Drawing.Point(136, 3);
            this.lblVideoSecs.Margin = new System.Windows.Forms.Padding(3);
            this.lblVideoSecs.Name = "lblVideoSecs";
            this.lblVideoSecs.Size = new System.Drawing.Size(143, 39);
            this.lblVideoSecs.TabIndex = 2;
            this.lblVideoSecs.Text = "Video Secs: ";
            this.lblVideoSecs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowPanelBotomRow
            // 
            this.flowPanelBotomRow.Controls.Add(this.btnReWriteSecs);
            this.flowPanelBotomRow.Controls.Add(this.lblVideoSecs);
            this.flowPanelBotomRow.Location = new System.Drawing.Point(12, 496);
            this.flowPanelBotomRow.Name = "flowPanelBotomRow";
            this.flowPanelBotomRow.Size = new System.Drawing.Size(694, 53);
            this.flowPanelBotomRow.TabIndex = 3;
            // 
            // frmOldRide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 587);
            this.Controls.Add(this.flowPanelBotomRow);
            this.Controls.Add(this.dgViewOldRide);
            this.Name = "frmOldRide";
            this.Text = "frmOldRide";
            this.Load += new System.EventHandler(this.frmOldRide_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewOldRide)).EndInit();
            this.flowPanelBotomRow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnReWriteSecs;
        public System.Windows.Forms.DataGridView dgViewOldRide;
        private System.Windows.Forms.FlowLayoutPanel flowPanelBotomRow;
        public System.Windows.Forms.Label lblVideoSecs;
    }
}