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
            this.btnLoadVideo = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correctionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoadRide = new System.Windows.Forms.Button();
            this.openRideFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DataGridOldRide = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCreateLatLons = new System.Windows.Forms.Button();
            this.flowLayoutPanelLabels = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRideName = new System.Windows.Forms.Label();
            this.lblLoadVideo = new System.Windows.Forms.Label();
            this.txtbFromClipboard = new System.Windows.Forms.TextBox();
            this.openFileMovieDialog = new System.Windows.Forms.OpenFileDialog();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.btnSetVideoPositions = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridCorrectionsList)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridOldRide)).BeginInit();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.flowLayoutPanelLabels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
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
            this.gridCorrectionsList.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.UserDeleteRow);
            // 
            // btnGetClipboardData
            // 
            this.btnGetClipboardData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetClipboardData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetClipboardData.Location = new System.Drawing.Point(3, 80);
            this.btnGetClipboardData.Name = "btnGetClipboardData";
            this.btnGetClipboardData.Size = new System.Drawing.Size(212, 33);
            this.btnGetClipboardData.TabIndex = 1;
            this.btnGetClipboardData.Text = "Get Clipboard Data";
            this.btnGetClipboardData.UseVisualStyleBackColor = true;
            this.btnGetClipboardData.Click += new System.EventHandler(this.btnGetClipboardData_Click);
            // 
            // btnLoadVideo
            // 
            this.btnLoadVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadVideo.Location = new System.Drawing.Point(3, 39);
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
            this.fileToolStripMenuItem,
            this.correctionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1055, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.openToolStripMenuItem.Text = "&Open Config";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.saveToolStripMenuItem.Text = "&Save Config";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // correctionsToolStripMenuItem
            // 
            this.correctionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.clearGridToolStripMenuItem});
            this.correctionsToolStripMenuItem.Name = "correctionsToolStripMenuItem";
            this.correctionsToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.correctionsToolStripMenuItem.Text = "Corrections";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.openFileToolStripMenuItem.Text = "Open File";
            // 
            // clearGridToolStripMenuItem
            // 
            this.clearGridToolStripMenuItem.Name = "clearGridToolStripMenuItem";
            this.clearGridToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.clearGridToolStripMenuItem.Text = "Clear grid";
            this.clearGridToolStripMenuItem.Click += new System.EventHandler(this.clearGridToolStripMenuItem_Click);
            // 
            // btnLoadRide
            // 
            this.btnLoadRide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadRide.Location = new System.Drawing.Point(3, 3);
            this.btnLoadRide.Name = "btnLoadRide";
            this.btnLoadRide.Size = new System.Drawing.Size(212, 30);
            this.btnLoadRide.TabIndex = 5;
            this.btnLoadRide.Text = "Load Ride";
            this.btnLoadRide.UseVisualStyleBackColor = true;
            this.btnLoadRide.Click += new System.EventHandler(this.BtnLoadRide_Click);
            // 
            // openRideFileDialog
            // 
            this.openRideFileDialog.Filter = "json files|*.json";
            this.openRideFileDialog.InitialDirectory = "C:\\coding\\json\\";
            this.openRideFileDialog.RestoreDirectory = true;
            this.openRideFileDialog.Title = "Browse json files";
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
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanelButtons.Controls.Add(this.btnLoadRide);
            this.flowLayoutPanelButtons.Controls.Add(this.btnLoadVideo);
            this.flowLayoutPanelButtons.Controls.Add(this.btnGetClipboardData);
            this.flowLayoutPanelButtons.Controls.Add(this.btnCreateLatLons);
            this.flowLayoutPanelButtons.Controls.Add(this.btnSetVideoPositions);
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(807, 311);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(221, 235);
            this.flowLayoutPanelButtons.TabIndex = 8;
            // 
            // btnCreateLatLons
            // 
            this.btnCreateLatLons.Location = new System.Drawing.Point(3, 119);
            this.btnCreateLatLons.Name = "btnCreateLatLons";
            this.btnCreateLatLons.Size = new System.Drawing.Size(212, 31);
            this.btnCreateLatLons.TabIndex = 6;
            this.btnCreateLatLons.Text = "Create LatLons";
            this.btnCreateLatLons.UseVisualStyleBackColor = true;
            this.btnCreateLatLons.Click += new System.EventHandler(this.btnCreateLatLons_Click);
            // 
            // flowLayoutPanelLabels
            // 
            this.flowLayoutPanelLabels.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanelLabels.Controls.Add(this.lblRideName);
            this.flowLayoutPanelLabels.Controls.Add(this.lblLoadVideo);
            this.flowLayoutPanelLabels.Controls.Add(this.txtbFromClipboard);
            this.flowLayoutPanelLabels.Location = new System.Drawing.Point(474, 314);
            this.flowLayoutPanelLabels.Name = "flowLayoutPanelLabels";
            this.flowLayoutPanelLabels.Size = new System.Drawing.Size(327, 232);
            this.flowLayoutPanelLabels.TabIndex = 9;
            // 
            // lblRideName
            // 
            this.lblRideName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblRideName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRideName.Location = new System.Drawing.Point(3, 0);
            this.lblRideName.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.lblRideName.Name = "lblRideName";
            this.lblRideName.Size = new System.Drawing.Size(326, 30);
            this.lblRideName.TabIndex = 0;
            this.lblRideName.Text = "Ride Name";
            this.lblRideName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLoadVideo
            // 
            this.lblLoadVideo.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblLoadVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLoadVideo.Location = new System.Drawing.Point(3, 32);
            this.lblLoadVideo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.lblLoadVideo.Name = "lblLoadVideo";
            this.lblLoadVideo.Size = new System.Drawing.Size(324, 39);
            this.lblLoadVideo.TabIndex = 1;
            this.lblLoadVideo.Text = "Video Name";
            this.lblLoadVideo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtbFromClipboard
            // 
            this.txtbFromClipboard.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtbFromClipboard.Location = new System.Drawing.Point(3, 83);
            this.txtbFromClipboard.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.txtbFromClipboard.Name = "txtbFromClipboard";
            this.txtbFromClipboard.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtbFromClipboard.Size = new System.Drawing.Size(324, 22);
            this.txtbFromClipboard.TabIndex = 2;
            this.txtbFromClipboard.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // openFileMovieDialog
            // 
            this.openFileMovieDialog.Filter = "All files|*.*";
            this.openFileMovieDialog.InitialDirectory = "C:\\BikeAthlets\\Peter Test\\media\\";
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(12, 312);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(456, 235);
            this.axWindowsMediaPlayer1.TabIndex = 2;
            this.axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.MediaPlayerStateChanged);
            // 
            // btnSetVideoPositions
            // 
            this.btnSetVideoPositions.Location = new System.Drawing.Point(3, 156);
            this.btnSetVideoPositions.Name = "btnSetVideoPositions";
            this.btnSetVideoPositions.Size = new System.Drawing.Size(212, 34);
            this.btnSetVideoPositions.TabIndex = 7;
            this.btnSetVideoPositions.Text = "Set Video Position";
            this.btnSetVideoPositions.UseVisualStyleBackColor = true;
            this.btnSetVideoPositions.Click += new System.EventHandler(this.btnSetVideoPositions_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1055, 808);
            this.Controls.Add(this.flowLayoutPanelLabels);
            this.Controls.Add(this.flowLayoutPanelButtons);
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridOldRide)).EndInit();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.flowLayoutPanelLabels.ResumeLayout(false);
            this.flowLayoutPanelLabels.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
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
        private System.Windows.Forms.DataGridView DataGridOldRide;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLabels;
        private System.Windows.Forms.Label lblRideName;
        private System.Windows.Forms.Label lblLoadVideo;
        private System.Windows.Forms.TextBox txtbFromClipboard;
        private System.Windows.Forms.OpenFileDialog openFileMovieDialog;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem correctionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearGridToolStripMenuItem;
        private System.Windows.Forms.Button btnCreateLatLons;
        private System.Windows.Forms.Button btnSetVideoPositions;
    }
}

