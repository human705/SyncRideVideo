namespace BuildCorrectionsList
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gridCorrectionsList = new System.Windows.Forms.DataGridView();
            this.btnGetClipboardData = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.btnLoadVideo = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoadRide = new System.Windows.Forms.Button();
            this.openRideFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.TxtbRideFileName = new System.Windows.Forms.TextBox();
            this.DataGridOldRide = new System.Windows.Forms.DataGridView();
            this.FlowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.gridCorrectionsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridOldRide)).BeginInit();
            this.FlowLayoutPanelButtons.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridCorrectionsList
            // 
            this.gridCorrectionsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCorrectionsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCorrectionsList.Location = new System.Drawing.Point(12, 31);
            this.gridCorrectionsList.Name = "gridCorrectionsList";
            this.gridCorrectionsList.RowHeadersWidth = 51;
            this.gridCorrectionsList.RowTemplate.Height = 24;
            this.gridCorrectionsList.Size = new System.Drawing.Size(1016, 274);
            this.gridCorrectionsList.TabIndex = 0;
            // 
            // btnGetClipboardData
            // 
            this.btnGetClipboardData.Location = new System.Drawing.Point(3, 85);
            this.btnGetClipboardData.Name = "btnGetClipboardData";
            this.btnGetClipboardData.Size = new System.Drawing.Size(212, 41);
            this.btnGetClipboardData.TabIndex = 1;
            this.btnGetClipboardData.Text = "Get Clipboard Data";
            this.btnGetClipboardData.UseVisualStyleBackColor = true;
            this.btnGetClipboardData.Click += new System.EventHandler(this.btnGetClipboardData_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(12, 311);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(456, 235);
            this.axWindowsMediaPlayer1.TabIndex = 2;
            this.axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.MediaPlayerStateChanged);
            // 
            // btnLoadVideo
            // 
            this.btnLoadVideo.Location = new System.Drawing.Point(3, 44);
            this.btnLoadVideo.Name = "btnLoadVideo";
            this.btnLoadVideo.Size = new System.Drawing.Size(212, 35);
            this.btnLoadVideo.TabIndex = 3;
            this.btnLoadVideo.Text = "Load Video";
            this.btnLoadVideo.UseVisualStyleBackColor = true;
            this.btnLoadVideo.Click += new System.EventHandler(this.btnLoadVideo_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1319, 38);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 34);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // btnLoadRide
            // 
            this.btnLoadRide.Location = new System.Drawing.Point(3, 3);
            this.btnLoadRide.Name = "btnLoadRide";
            this.btnLoadRide.Size = new System.Drawing.Size(212, 35);
            this.btnLoadRide.TabIndex = 5;
            this.btnLoadRide.Text = "Load Ride";
            this.btnLoadRide.UseVisualStyleBackColor = true;
            this.btnLoadRide.Click += new System.EventHandler(this.BtnLoadRide_Click);
            // 
            // openRideFileDialog
            // 
            this.openRideFileDialog.FileName = "openRideFileDialog";
            this.openRideFileDialog.Filter = "json files|*.json";
            this.openRideFileDialog.InitialDirectory = "C:\\coding\\json\\";
            this.openRideFileDialog.RestoreDirectory = true;
            this.openRideFileDialog.Title = "Browse json files";
            // 
            // TxtbRideFileName
            // 
            this.TxtbRideFileName.Location = new System.Drawing.Point(3, 3);
            this.TxtbRideFileName.Name = "TxtbRideFileName";
            this.TxtbRideFileName.Size = new System.Drawing.Size(267, 22);
            this.TxtbRideFileName.TabIndex = 6;
            // 
            // DataGridOldRide
            // 
            this.DataGridOldRide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DataGridOldRide.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridOldRide.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridOldRide.Location = new System.Drawing.Point(12, 552);
            this.DataGridOldRide.Name = "DataGridOldRide";
            this.DataGridOldRide.RowHeadersWidth = 51;
            this.DataGridOldRide.RowTemplate.Height = 24;
            this.DataGridOldRide.Size = new System.Drawing.Size(1016, 244);
            this.DataGridOldRide.TabIndex = 7;
            // 
            // FlowLayoutPanelButtons
            // 
            this.FlowLayoutPanelButtons.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FlowLayoutPanelButtons.Controls.Add(this.btnLoadRide);
            this.FlowLayoutPanelButtons.Controls.Add(this.btnLoadVideo);
            this.FlowLayoutPanelButtons.Controls.Add(this.btnGetClipboardData);
            this.FlowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanelButtons.Location = new System.Drawing.Point(810, 311);
            this.FlowLayoutPanelButtons.Name = "FlowLayoutPanelButtons";
            this.FlowLayoutPanelButtons.Size = new System.Drawing.Size(218, 235);
            this.FlowLayoutPanelButtons.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanel1.Controls.Add(this.TxtbRideFileName);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(531, 314);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(273, 232);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1055, 808);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.FlowLayoutPanelButtons);
            this.Controls.Add(this.DataGridOldRide);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.gridCorrectionsList);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormLoaded);
            this.ResizeEnd += new System.EventHandler(this.FormResizeEnded);
            this.Resize += new System.EventHandler(this.FormResizing);
            ((System.ComponentModel.ISupportInitialize)(this.gridCorrectionsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridOldRide)).EndInit();
            this.FlowLayoutPanelButtons.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridCorrectionsList;
        private System.Windows.Forms.Button btnGetClipboardData;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Button btnLoadVideo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button btnLoadRide;
        private System.Windows.Forms.OpenFileDialog openRideFileDialog;
        private System.Windows.Forms.TextBox TxtbRideFileName;
        private System.Windows.Forms.DataGridView DataGridOldRide;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanelButtons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

