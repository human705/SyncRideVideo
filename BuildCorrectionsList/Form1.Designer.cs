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
            this.btnLoadVideo = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateProjtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correctionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoadRide = new System.Windows.Forms.Button();
            this.openRideFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnCreateNewRide = new System.Windows.Forms.Button();
            this.btnShowOldRide = new System.Windows.Forms.Button();
            this.btnSetVideoPositions = new System.Windows.Forms.Button();
            this.lblVideoTimeInSecs = new System.Windows.Forms.Label();
            this.btnVideoAdvance = new System.Windows.Forms.Button();
            this.btnVideoReverse = new System.Windows.Forms.Button();
            this.txtbVideoTimeChange = new System.Windows.Forms.TextBox();
            this.openFileMovieDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStripCorrectionsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SelectFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.timerQMapLocs = new System.Windows.Forms.Timer(this.components);
            this.TopPanel = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.VideoNameLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.RideNameLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TotVideoTimeToolStripLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.BottomPanelFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.VideoPanel = new System.Windows.Forms.Panel();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.MiddleSplitter = new System.Windows.Forms.Splitter();
            this.mapPanel1 = new System.Windows.Forms.Panel();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridCorrectionsList)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripCorrectionsGrid.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.BottomPanelFlowLayoutPanel.SuspendLayout();
            this.VideoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.mapPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridCorrectionsList
            // 
            this.gridCorrectionsList.AllowUserToAddRows = false;
            this.gridCorrectionsList.AllowUserToDeleteRows = false;
            this.gridCorrectionsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCorrectionsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCorrectionsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCorrectionsList.Location = new System.Drawing.Point(0, 0);
            this.gridCorrectionsList.Name = "gridCorrectionsList";
            this.gridCorrectionsList.RowHeadersWidth = 51;
            this.gridCorrectionsList.RowTemplate.Height = 24;
            this.gridCorrectionsList.Size = new System.Drawing.Size(1242, 99);
            this.gridCorrectionsList.TabIndex = 0;
            this.gridCorrectionsList.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridCorrectionsList_CellMouseUp);
            this.gridCorrectionsList.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.UserDeleteRow);
            // 
            // btnLoadVideo
            // 
            this.btnLoadVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadVideo.Location = new System.Drawing.Point(222, 7);
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
            this.recentToolStripMenuItem,
            this.fileToolStripMenuItem,
            this.correctionsToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1242, 30);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // recentToolStripMenuItem
            // 
            this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
            this.recentToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
            this.recentToolStripMenuItem.Text = "Recent";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateProjtoolStripMenuItem,
            this.toolStripSeparator1,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // CreateProjtoolStripMenuItem
            // 
            this.CreateProjtoolStripMenuItem.Name = "CreateProjtoolStripMenuItem";
            this.CreateProjtoolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.CreateProjtoolStripMenuItem.Text = "Create Project";
            this.CreateProjtoolStripMenuItem.Click += new System.EventHandler(this.CreateProjtoolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.openToolStripMenuItem.Text = "&Open Project";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.saveToolStripMenuItem.Text = "&Save Project";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // correctionsToolStripMenuItem
            // 
            this.correctionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.clearGridToolStripMenuItem});
            this.correctionsToolStripMenuItem.Name = "correctionsToolStripMenuItem";
            this.correctionsToolStripMenuItem.Size = new System.Drawing.Size(98, 26);
            this.correctionsToolStripMenuItem.Text = "Corrections";
            this.correctionsToolStripMenuItem.Click += new System.EventHandler(this.correctionsToolStripMenuItem_Click);
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
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMapToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 26);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showMapToolStripMenuItem
            // 
            this.showMapToolStripMenuItem.Name = "showMapToolStripMenuItem";
            this.showMapToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.showMapToolStripMenuItem.Text = "Show Map";
            this.showMapToolStripMenuItem.Click += new System.EventHandler(this.showMapToolStripMenuItem_Click);
            // 
            // btnLoadRide
            // 
            this.btnLoadRide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadRide.Location = new System.Drawing.Point(325, 7);
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
            // btnCreateNewRide
            // 
            this.btnCreateNewRide.BackColor = System.Drawing.Color.Turquoise;
            this.btnCreateNewRide.Location = new System.Drawing.Point(1053, 8);
            this.btnCreateNewRide.Name = "btnCreateNewRide";
            this.btnCreateNewRide.Size = new System.Drawing.Size(144, 35);
            this.btnCreateNewRide.TabIndex = 7;
            this.btnCreateNewRide.Text = "Create New Ride";
            this.btnCreateNewRide.UseVisualStyleBackColor = false;
            this.btnCreateNewRide.Click += new System.EventHandler(this.btnCreateNewRide_Click);
            // 
            // btnShowOldRide
            // 
            this.btnShowOldRide.Location = new System.Drawing.Point(759, 8);
            this.btnShowOldRide.Name = "btnShowOldRide";
            this.btnShowOldRide.Size = new System.Drawing.Size(132, 35);
            this.btnShowOldRide.TabIndex = 8;
            this.btnShowOldRide.Text = "Show Old Ride";
            this.btnShowOldRide.UseVisualStyleBackColor = true;
            this.btnShowOldRide.Click += new System.EventHandler(this.btnShowOldRide_Click);
            // 
            // btnSetVideoPositions
            // 
            this.btnSetVideoPositions.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetVideoPositions.Location = new System.Drawing.Point(897, 8);
            this.btnSetVideoPositions.Name = "btnSetVideoPositions";
            this.btnSetVideoPositions.Size = new System.Drawing.Size(150, 35);
            this.btnSetVideoPositions.TabIndex = 7;
            this.btnSetVideoPositions.Text = "Set Video Position";
            this.btnSetVideoPositions.UseVisualStyleBackColor = true;
            this.btnSetVideoPositions.Click += new System.EventHandler(this.btnSetVideoPositions_Click);
            // 
            // lblVideoTimeInSecs
            // 
            this.lblVideoTimeInSecs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVideoTimeInSecs.Location = new System.Drawing.Point(7, 7);
            this.lblVideoTimeInSecs.Margin = new System.Windows.Forms.Padding(2);
            this.lblVideoTimeInSecs.Name = "lblVideoTimeInSecs";
            this.lblVideoTimeInSecs.Size = new System.Drawing.Size(211, 35);
            this.lblVideoTimeInSecs.TabIndex = 11;
            this.lblVideoTimeInSecs.Text = "Video position: ";
            this.lblVideoTimeInSecs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnVideoAdvance
            // 
            this.btnVideoAdvance.Location = new System.Drawing.Point(628, 8);
            this.btnVideoAdvance.Name = "btnVideoAdvance";
            this.btnVideoAdvance.Size = new System.Drawing.Size(125, 35);
            this.btnVideoAdvance.TabIndex = 9;
            this.btnVideoAdvance.Text = "Advance Video";
            this.btnVideoAdvance.UseVisualStyleBackColor = true;
            this.btnVideoAdvance.Click += new System.EventHandler(this.btnVideoAdvance_Click);
            // 
            // btnVideoReverse
            // 
            this.btnVideoReverse.Location = new System.Drawing.Point(429, 8);
            this.btnVideoReverse.Name = "btnVideoReverse";
            this.btnVideoReverse.Size = new System.Drawing.Size(119, 35);
            this.btnVideoReverse.TabIndex = 8;
            this.btnVideoReverse.Text = "Reverse Video";
            this.btnVideoReverse.UseVisualStyleBackColor = true;
            this.btnVideoReverse.Click += new System.EventHandler(this.btnVideoReverse_Click);
            // 
            // txtbVideoTimeChange
            // 
            this.txtbVideoTimeChange.BackColor = System.Drawing.SystemColors.Info;
            this.txtbVideoTimeChange.Location = new System.Drawing.Point(554, 8);
            this.txtbVideoTimeChange.Name = "txtbVideoTimeChange";
            this.txtbVideoTimeChange.Size = new System.Drawing.Size(68, 22);
            this.txtbVideoTimeChange.TabIndex = 10;
            // 
            // openFileMovieDialog
            // 
            this.openFileMovieDialog.Filter = "All files|*.*";
            this.openFileMovieDialog.InitialDirectory = "C:\\BikeAthlets\\Peter Test\\media\\";
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
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.gridCorrectionsList);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 30);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1242, 99);
            this.TopPanel.TabIndex = 10;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.RoyalBlue;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 129);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1242, 13);
            this.splitter1.TabIndex = 11;
            this.splitter1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VideoNameLabel1,
            this.RideNameLabel1,
            this.TotVideoTimeToolStripLabel1});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 651);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1242, 30);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // VideoNameLabel1
            // 
            this.VideoNameLabel1.BackColor = System.Drawing.Color.Red;
            this.VideoNameLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.VideoNameLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.VideoNameLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.VideoNameLabel1.Name = "VideoNameLabel1";
            this.VideoNameLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VideoNameLabel1.Size = new System.Drawing.Size(130, 24);
            this.VideoNameLabel1.Spring = true;
            this.VideoNameLabel1.Text = "No Video Loaded";
            this.VideoNameLabel1.ToolTipText = "The name of the video loaded";
            // 
            // RideNameLabel1
            // 
            this.RideNameLabel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.RideNameLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.RideNameLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.RideNameLabel1.Name = "RideNameLabel1";
            this.RideNameLabel1.Size = new System.Drawing.Size(121, 24);
            this.RideNameLabel1.Spring = true;
            this.RideNameLabel1.Text = "No Ride Loaded";
            this.RideNameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RideNameLabel1.ToolTipText = "The name of the lodaed ride";
            // 
            // TotVideoTimeToolStripLabel1
            // 
            this.TotVideoTimeToolStripLabel1.BackColor = System.Drawing.Color.SpringGreen;
            this.TotVideoTimeToolStripLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.TotVideoTimeToolStripLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.TotVideoTimeToolStripLabel1.Name = "TotVideoTimeToolStripLabel1";
            this.TotVideoTimeToolStripLabel1.Size = new System.Drawing.Size(126, 24);
            this.TotVideoTimeToolStripLabel1.Spring = true;
            this.TotVideoTimeToolStripLabel1.Text = "Total Video Time";
            this.TotVideoTimeToolStripLabel1.ToolTipText = "Total Video Time";
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.BottomPanelFlowLayoutPanel);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 601);
            this.BottomPanel.MaximumSize = new System.Drawing.Size(0, 50);
            this.BottomPanel.MinimumSize = new System.Drawing.Size(0, 50);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1242, 50);
            this.BottomPanel.TabIndex = 15;
            // 
            // BottomPanelFlowLayoutPanel
            // 
            this.BottomPanelFlowLayoutPanel.Controls.Add(this.lblVideoTimeInSecs);
            this.BottomPanelFlowLayoutPanel.Controls.Add(this.btnLoadVideo);
            this.BottomPanelFlowLayoutPanel.Controls.Add(this.btnLoadRide);
            this.BottomPanelFlowLayoutPanel.Controls.Add(this.btnVideoReverse);
            this.BottomPanelFlowLayoutPanel.Controls.Add(this.txtbVideoTimeChange);
            this.BottomPanelFlowLayoutPanel.Controls.Add(this.btnVideoAdvance);
            this.BottomPanelFlowLayoutPanel.Controls.Add(this.btnShowOldRide);
            this.BottomPanelFlowLayoutPanel.Controls.Add(this.btnSetVideoPositions);
            this.BottomPanelFlowLayoutPanel.Controls.Add(this.btnCreateNewRide);
            this.BottomPanelFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomPanelFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomPanelFlowLayoutPanel.Name = "BottomPanelFlowLayoutPanel";
            this.BottomPanelFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(5);
            this.BottomPanelFlowLayoutPanel.Size = new System.Drawing.Size(1242, 50);
            this.BottomPanelFlowLayoutPanel.TabIndex = 0;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.RoyalBlue;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 589);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1242, 12);
            this.splitter2.TabIndex = 16;
            this.splitter2.TabStop = false;
            // 
            // VideoPanel
            // 
            this.VideoPanel.BackColor = System.Drawing.Color.Red;
            this.VideoPanel.Controls.Add(this.axWindowsMediaPlayer1);
            this.VideoPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.VideoPanel.Location = new System.Drawing.Point(0, 142);
            this.VideoPanel.Name = "VideoPanel";
            this.VideoPanel.Size = new System.Drawing.Size(426, 447);
            this.VideoPanel.TabIndex = 0;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(426, 447);
            this.axWindowsMediaPlayer1.TabIndex = 2;
            this.axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.MediaPlayerStateChanged);
            // 
            // MiddleSplitter
            // 
            this.MiddleSplitter.BackColor = System.Drawing.Color.RoyalBlue;
            this.MiddleSplitter.Location = new System.Drawing.Point(426, 142);
            this.MiddleSplitter.Name = "MiddleSplitter";
            this.MiddleSplitter.Size = new System.Drawing.Size(16, 447);
            this.MiddleSplitter.TabIndex = 17;
            this.MiddleSplitter.TabStop = false;
            // 
            // mapPanel1
            // 
            this.mapPanel1.BackColor = System.Drawing.Color.Magenta;
            this.mapPanel1.Controls.Add(this.gMapControl1);
            this.mapPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapPanel1.Location = new System.Drawing.Point(442, 142);
            this.mapPanel1.Name = "mapPanel1";
            this.mapPanel1.Size = new System.Drawing.Size(800, 447);
            this.mapPanel1.TabIndex = 18;
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(0, 0);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 1;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(800, 447);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.OnMapClick += new GMap.NET.WindowsForms.MapClick(this.gMapControl1_OnMapClick);
            this.gMapControl1.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gMapControl1_OnMarkerClick);
            this.gMapControl1.OnMarkerDoubleClick += new GMap.NET.WindowsForms.MarkerDoubleClick(this.gMapControl1_OnMarkerDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1242, 681);
            this.Controls.Add(this.mapPanel1);
            this.Controls.Add(this.MiddleSplitter);
            this.Controls.Add(this.VideoPanel);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.TopPanel);
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
            this.contextMenuStripCorrectionsGrid.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanelFlowLayoutPanel.ResumeLayout(false);
            this.BottomPanelFlowLayoutPanel.PerformLayout();
            this.VideoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.mapPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridCorrectionsList;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Button btnLoadVideo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button btnLoadRide;
        private System.Windows.Forms.OpenFileDialog openRideFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileMovieDialog;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem correctionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearGridToolStripMenuItem;
        private System.Windows.Forms.Button btnSetVideoPositions;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCorrectionsGrid;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.Button btnVideoReverse;
        private System.Windows.Forms.TextBox txtbVideoTimeChange;
        private System.Windows.Forms.Button btnVideoAdvance;
        private System.Windows.Forms.Label lblVideoTimeInSecs;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnCreateNewRide;
        private System.Windows.Forms.Button btnShowOldRide;
        private System.Windows.Forms.ToolStripMenuItem CreateProjtoolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.FolderBrowserDialog SelectFolderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem recentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMapToolStripMenuItem;
        public System.Windows.Forms.Timer timerQMapLocs;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel VideoPanel;
        private System.Windows.Forms.Splitter MiddleSplitter;
        private System.Windows.Forms.ToolStripStatusLabel VideoNameLabel1;
        private System.Windows.Forms.ToolStripStatusLabel RideNameLabel1;
        private System.Windows.Forms.FlowLayoutPanel BottomPanelFlowLayoutPanel;
        private System.Windows.Forms.ToolStripStatusLabel TotVideoTimeToolStripLabel1;
        private System.Windows.Forms.Panel mapPanel1;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
    }
}

