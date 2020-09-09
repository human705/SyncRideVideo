using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CommonLibrary;

namespace BuildCorrectionsList
{
    public partial class frmOldRide : Form
    {
        public frmOldRide()
        {
            InitializeComponent();
        }

        private void frmOldRide_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            this.Text = "Old Ride Data";
            this.Size = new Size(820, 650);
            this.BackColor = Color.Blue;

            dgViewOldRide.Location = new Point(5, 5);
            dgViewOldRide.Size = new System.Drawing.Size(800, 600);
            dgViewOldRide.AutoSize = false;
            dgViewOldRide.AllowUserToAddRows = false;
            dgViewOldRide.AllowUserToDeleteRows = false;

            dgViewOldRide.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgViewOldRide.DataSource = Form1.dtOldRide;
            dgViewOldRide.Refresh();

            flowPanelBotomRow.Size = new Size(800, 50);
            flowPanelBotomRow.Location = new Point(5, 605);


            // Controls in the bottom row
                        btnReWriteSecs.Text = "ReWrite Secs";
            btnReWriteSecs.BackColor = Color.Gray;
            //btnReWriteSecs.Location = new Point(5, 605);
            btnReWriteSecs.Size = new Size(100, 30);

            lblVideoSecs.Size = new Size(100, 30);
            lblVideoSecs.Text += Form1.VideoLingthInSecs.ToString();
        }

        private void btnReWriteSecs_Click(object sender, EventArgs e)
        {
            int j = 0;
            foreach (DataRow dr in Form1.dtOldRide.Rows)
            {
                dr["secs"] = j;
                j++;
            }
            dgViewOldRide.Refresh();
            DataTableOperations dto = new DataTableOperations();
            dto.UpdateRideListSamplesFromTable(Form1.dtOldRide, Form1.newRide);
        }

    }
}
