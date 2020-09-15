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
            this.components = new System.ComponentModel.Container();
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
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowOldRide = new System.Windows.Forms.Button();
            this.btnReSequence = new System.Windows.Forms.Button();
            this.btnCreateLatLons = new System.Windows.Forms.Button();
            this.btnCreateNewRide = new System.Windows.Forms.Button();
            this.btnSetVideoPositions = new System.Windows.Forms.Button();
            this.flowLayoutPanelLabels = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRideName = new System.Windows.Forms.Label();
            this.lblLoadVideo = new System.Windows.Forms.Label();
            this.lblFromClipboard = new System.Windows.Forms.Label();
            this.grpboxVideoControls = new System.Windows.Forms.GroupBox();
            this.lblVideoTimeTotal = new System.Windows.Forms.Label();
            this.lblVideoTimeInSecs = new System.Windows.Forms.Label();
            this.btnVideoAdvance = new System.Windows.Forms.Button();
            this.btnVideoReverse = new System.Windows.Forms.Button();
            this.txtbVideoTimeChange = new System.Windows.Forms.TextBox();
            this.openFileMovieDialog = new System.Windows.Forms.OpenFileDialog();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.contextMenuStripCorrectionsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridCorrectionsList)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanelLabels.SuspendLayout();
            this.grpboxVideoControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.contextMenuStripCorrectionsGrid.SuspendLayout();
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
            this.gridCorrectionsList.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridCorrectionsList_CellMouseUp);
            this.gridCorrectionsList.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.UserDeleteRow);
            // 
            // btnGetClipboardData
            // 
            this.btnGetClipboardData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetClipboardData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetClipboardData.Location = new System.Drawing.Point(4, 82);
            this.btnGetClipboardData.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetClipboardData.Name = "btnGetClipboardData";
            this.btnGetClipboardData.Size = new System.Drawing.Size(143, 33);
            this.btnGetClipboardData.TabIndex = 1;
            this.btnGetClipboardData.Text = "Get Clipboard Data";
            this.btnGetClipboardData.UseVisualStyleBackColor = true;
            this.btnGetClipboardData.Click += new System.EventHandler(this.btnGetClipboardData_Click);
            // 
            // btnLoadVideo
            // 
            this.btnLoadVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadVideo.Location = new System.Drawing.Point(4, 43);
            this.btnLoadVideo.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadVideo.Name = "btnLoadVideo";
            this.btnLoadVideo.Size = new System.Drawing.Size(99, 35);
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
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem.Text = "&Open Config";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
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
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
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
            this.btnLoadRide.Location = new System.Drawing.Point(4, 4);
            this.btnLoadRide.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadRide.Name = "btnLoadRide";
            this.btnLoadRide.Size = new System.Drawing.Size(99, 35);
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
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanelButtons.Controls.Add(this.groupBox1);
            this.flowLayoutPanelButtons.Controls.Add(this.btnCreateLatLons);
            this.flowLayoutPanelButtons.Controls.Add(this.btnCreateNewRide);
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(879, 311);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(149, 235);
            this.flowLayoutPanelButtons.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShowOldRide);
            this.groupBox1.Controls.Add(this.btnReSequence);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 104);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Old Ride";
            // 
            // btnShowOldRide
            // 
            this.btnShowOldRide.Location = new System.Drawing.Point(6, 21);
            this.btnShowOldRide.Name = "btnShowOldRide";
            this.btnShowOldRide.Size = new System.Drawing.Size(132, 37);
            this.btnShowOldRide.TabIndex = 8;
            this.btnShowOldRide.Text = "Show Old Ride";
            this.btnShowOldRide.UseVisualStyleBackColor = true;
            this.btnShowOldRide.Click += new System.EventHandler(this.btnShowOldRide_Click);
            // 
            // btnReSequence
            // 
            this.btnReSequence.Location = new System.Drawing.Point(6, 63);
            this.btnReSequence.Name = "btnReSequence";
            this.btnReSequence.Size = new System.Drawing.Size(132, 35);
            this.btnReSequence.TabIndex = 9;
            this.btnReSequence.Text = "Re-Sequence";
            this.btnReSequence.UseVisualStyleBackColor = true;
            this.btnReSequence.Click += new System.EventHandler(this.btnReSequence_Click);
            // 
            // btnCreateLatLons
            // 
            this.btnCreateLatLons.Location = new System.Drawing.Point(3, 113);
            this.btnCreateLatLons.Name = "btnCreateLatLons";
            this.btnCreateLatLons.Size = new System.Drawing.Size(144, 39);
            this.btnCreateLatLons.TabIndex = 6;
            this.btnCreateLatLons.Text = "Create LatLons";
            this.btnCreateLatLons.UseVisualStyleBackColor = true;
            this.btnCreateLatLons.Click += new System.EventHandler(this.btnCreateLatLons_Click);
            // 
            // btnCreateNewRide
            // 
            this.btnCreateNewRide.Location = new System.Drawing.Point(3, 158);
            this.btnCreateNewRide.Name = "btnCreateNewRide";
            this.btnCreateNewRide.Size = new System.Drawing.Size(144, 41);
            this.btnCreateNewRide.TabIndex = 7;
            this.btnCreateNewRide.Text = "Create New Ride";
            this.btnCreateNewRide.UseVisualStyleBackColor = true;
            this.btnCreateNewRide.Click += new System.EventHandler(this.btnCreateNewRide_Click);
            // 
            // btnSetVideoPositions
            // 
            this.btnSetVideoPositions.Location = new System.Drawing.Point(252, 50);
            this.btnSetVideoPositions.Name = "btnSetVideoPositions";
            this.btnSetVideoPositions.Size = new System.Drawing.Size(125, 47);
            this.btnSetVideoPositions.TabIndex = 7;
            this.btnSetVideoPositions.Text = "Set Video Position from grid";
            this.btnSetVideoPositions.UseVisualStyleBackColor = true;
            this.btnSetVideoPositions.Click += new System.EventHandler(this.btnSetVideoPositions_Click);
            // 
            // flowLayoutPanelLabels
            // 
            this.flowLayoutPanelLabels.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanelLabels.Controls.Add(this.btnLoadRide);
            this.flowLayoutPanelLabels.Controls.Add(this.lblRideName);
            this.flowLayoutPanelLabels.Controls.Add(this.btnLoadVideo);
            this.flowLayoutPanelLabels.Controls.Add(this.lblLoadVideo);
            this.flowLayoutPanelLabels.Controls.Add(this.btnGetClipboardData);
            this.flowLayoutPanelLabels.Controls.Add(this.lblFromClipboard);
            this.flowLayoutPanelLabels.Controls.Add(this.grpboxVideoControls);
            this.flowLayoutPanelLabels.Location = new System.Drawing.Point(474, 311);
            this.flowLayoutPanelLabels.Name = "flowLayoutPanelLabels";
            this.flowLayoutPanelLabels.Padding = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelLabels.Size = new System.Drawing.Size(399, 236);
            this.flowLayoutPanelLabels.TabIndex = 9;
            // 
            // lblRideName
            // 
            this.lblRideName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblRideName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRideName.Location = new System.Drawing.Point(107, 4);
            this.lblRideName.Margin = new System.Windows.Forms.Padding(2);
            this.lblRideName.Name = "lblRideName";
            this.lblRideName.Size = new System.Drawing.Size(281, 35);
            this.lblRideName.TabIndex = 0;
            this.lblRideName.Text = "Ride Name";
            this.lblRideName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLoadVideo
            // 
            this.lblLoadVideo.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblLoadVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLoadVideo.Location = new System.Drawing.Point(107, 43);
            this.lblLoadVideo.Margin = new System.Windows.Forms.Padding(2);
            this.lblLoadVideo.Name = "lblLoadVideo";
            this.lblLoadVideo.Size = new System.Drawing.Size(281, 35);
            this.lblLoadVideo.TabIndex = 1;
            this.lblLoadVideo.Text = "Video Name";
            this.lblLoadVideo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFromClipboard
            // 
            this.lblFromClipboard.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblFromClipboard.Location = new System.Drawing.Point(151, 82);
            this.lblFromClipboard.Margin = new System.Windows.Forms.Padding(2);
            this.lblFromClipboard.Name = "lblFromClipboard";
            this.lblFromClipboard.Size = new System.Drawing.Size(237, 33);
            this.lblFromClipboard.TabIndex = 11;
            this.lblFromClipboard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpboxVideoControls
            // 
            this.grpboxVideoControls.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grpboxVideoControls.Controls.Add(this.lblVideoTimeTotal);
            this.grpboxVideoControls.Controls.Add(this.lblVideoTimeInSecs);
            this.grpboxVideoControls.Controls.Add(this.btnSetVideoPositions);
            this.grpboxVideoControls.Controls.Add(this.btnVideoAdvance);
            this.grpboxVideoControls.Controls.Add(this.btnVideoReverse);
            this.grpboxVideoControls.Controls.Add(this.txtbVideoTimeChange);
            this.grpboxVideoControls.Location = new System.Drawing.Point(5, 120);
            this.grpboxVideoControls.Name = "grpboxVideoControls";
            this.grpboxVideoControls.Size = new System.Drawing.Size(383, 103);
            this.grpboxVideoControls.TabIndex = 12;
            this.grpboxVideoControls.TabStop = false;
            this.grpboxVideoControls.Text = "Video Controls";
            // 
            // lblVideoTimeTotal
            // 
            this.lblVideoTimeTotal.Location = new System.Drawing.Point(4, 84);
            this.lblVideoTimeTotal.Name = "lblVideoTimeTotal";
            this.lblVideoTimeTotal.Size = new System.Drawing.Size(217, 19);
            this.lblVideoTimeTotal.TabIndex = 12;
            this.lblVideoTimeTotal.Text = "Total Secs: ";
            // 
            // lblVideoTimeInSecs
            // 
            this.lblVideoTimeInSecs.Location = new System.Drawing.Point(4, 49);
            this.lblVideoTimeInSecs.Margin = new System.Windows.Forms.Padding(3);
            this.lblVideoTimeInSecs.Name = "lblVideoTimeInSecs";
            this.lblVideoTimeInSecs.Size = new System.Drawing.Size(226, 30);
            this.lblVideoTimeInSecs.TabIndex = 11;
            this.lblVideoTimeInSecs.Text = "Video Secs: ";
            this.lblVideoTimeInSecs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnVideoAdvance
            // 
            this.btnVideoAdvance.Location = new System.Drawing.Point(252, 21);
            this.btnVideoAdvance.Name = "btnVideoAdvance";
            this.btnVideoAdvance.Size = new System.Drawing.Size(125, 23);
            this.btnVideoAdvance.TabIndex = 9;
            this.btnVideoAdvance.Text = "Advance Video";
            this.btnVideoAdvance.UseVisualStyleBackColor = true;
            this.btnVideoAdvance.Click += new System.EventHandler(this.btnVideoAdvance_Click);
            // 
            // btnVideoReverse
            // 
            this.btnVideoReverse.Location = new System.Drawing.Point(7, 21);
            this.btnVideoReverse.Name = "btnVideoReverse";
            this.btnVideoReverse.Size = new System.Drawing.Size(119, 23);
            this.btnVideoReverse.TabIndex = 8;
            this.btnVideoReverse.Text = "Reverse Video";
            this.btnVideoReverse.UseVisualStyleBackColor = true;
            this.btnVideoReverse.Click += new System.EventHandler(this.btnVideoReverse_Click);
            // 
            // txtbVideoTimeChange
            // 
            this.txtbVideoTimeChange.BackColor = System.Drawing.SystemColors.Info;
            this.txtbVideoTimeChange.Location = new System.Drawing.Point(132, 21);
            this.txtbVideoTimeChange.Name = "txtbVideoTimeChange";
            this.txtbVideoTimeChange.Size = new System.Drawing.Size(114, 22);
            this.txtbVideoTimeChange.TabIndex = 10;
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
            // contextMenuStripCorrectionsGrid
            // 
            this.contextMenuStripCorrectionsGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripCorrectionsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowToolStripMenuItem});
            this.contextMenuStripCorrectionsGrid.Name = "contextMenuStripCorrectionsGrid";
            this.contextMenuStripCorrectionsGrid.Size = new System.Drawing.Size(156, 28);
            this.contextMenuStripCorrectionsGrid.Click += new System.EventHandler(this.contextMenuStripCorrectionsGrid_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(155, 24);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1055, 808);
            this.Controls.Add(this.flowLayoutPanelLabels);
            this.Controls.Add(this.flowLayoutPanelButtons);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.gridCorrectionsList);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Sync Ride Video";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.FormLoaded);
            this.ResizeEnd += new System.EventHandler(this.FormResizeEnded);
            this.Resize += new System.EventHandler(this.FormResizing);
            ((System.ComponentModel.ISupportInitialize)(this.gridCorrectionsList)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanelLabels.ResumeLayout(false);
            this.grpboxVideoControls.ResumeLayout(false);
            this.grpboxVideoControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.contextMenuStripCorrectionsGrid.ResumeLayout(false);
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLabels;
        private System.Windows.Forms.Label lblRideName;
        private System.Windows.Forms.Label lblLoadVideo;
        private System.Windows.Forms.OpenFileDialog openFileMovieDialog;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem correctionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearGridToolStripMenuItem;
        private System.Windows.Forms.Button btnCreateLatLons;
        private System.Windows.Forms.Button btnSetVideoPositions;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCorrectionsGrid;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.Label lblFromClipboard;
        private System.Windows.Forms.Button btnVideoReverse;
        private System.Windows.Forms.TextBox txtbVideoTimeChange;
        private System.Windows.Forms.Button btnVideoAdvance;
        private System.Windows.Forms.GroupBox grpboxVideoControls;
        private System.Windows.Forms.Label lblVideoTimeInSecs;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnCreateNewRide;
        private System.Windows.Forms.Button btnShowOldRide;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReSequence;
        private System.Windows.Forms.Label lblVideoTimeTotal;
    }
}

