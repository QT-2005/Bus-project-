using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusTicketSystem.GUI
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void bookingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is the parent menu item - no action needed
        }

        private void myBookingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the BookingManagementForm
            try
            {
                BookingManagementForm bookingForm = new BookingManagementForm();
                bookingForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Booking Management: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void systemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is the parent menu item - no action needed
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var changePasswordForm = new ChangePasswordForm();
            changePasswordForm.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // This event fires for any menu item click
            // We're handling specific menu items in their own event handlers
        }


    }
}