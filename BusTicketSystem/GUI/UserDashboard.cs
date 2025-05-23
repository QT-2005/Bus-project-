using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusTicketSystem.Config;

namespace BusTicketSystem.GUI
{
    public partial class UserDashboard : Form
    {
        public UserDashboard()
        {
            InitializeComponent();
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            // Update status bar with logged in user info
            if (statusStrip != null && toolStripStatusLabel != null)
            {
                toolStripStatusLabel.Text = $"Logged in as: {Session.CurrentUser.FullName} (User)";
            }

            // Load default welcome screen
            LoadWelcomeScreen();
        }

        private void LoadWelcomeScreen()
        {
            // Clear panel
            panelContent.Controls.Clear();

            // Create welcome label
            Label lblWelcome = new Label();
            lblWelcome.Text = $"Welcome to Bus Ticket System\nLogged in as: {Session.CurrentUser.FullName}";
            lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new System.Drawing.Point(50, 50);

            // Create quick booking panel
            Panel quickBookingPanel = new Panel();
            quickBookingPanel.BorderStyle = BorderStyle.FixedSingle;
            quickBookingPanel.Width = 400;
            quickBookingPanel.Height = 200;
            quickBookingPanel.Location = new System.Drawing.Point(50, 120);

            Label lblQuickBooking = new Label();
            lblQuickBooking.Text = "Quick Actions";
            lblQuickBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblQuickBooking.AutoSize = true;
            lblQuickBooking.Location = new System.Drawing.Point(10, 10);

            Button btnNewBooking = new Button();
            btnNewBooking.Text = "New Booking";
            btnNewBooking.Size = new Size(150, 40);
            btnNewBooking.Location = new Point(30, 50);
            btnNewBooking.Click += (s, args) => newBookingToolStripMenuItem_Click(s, args);

            Button btnMyBookings = new Button();
            btnMyBookings.Text = "My Bookings";
            btnMyBookings.Size = new Size(150, 40);
            btnMyBookings.Location = new Point(210, 50);
            btnMyBookings.Click += (s, args) => myBookingsToolStripMenuItem_Click(s, args);

            Button btnChangePassword = new Button();
            btnChangePassword.Text = "Change Password";
            btnChangePassword.Size = new Size(150, 40);
            btnChangePassword.Location = new Point(30, 110);
            btnChangePassword.Click += (s, args) => changePasswordToolStripMenuItem_Click(s, args);

            Button btnLogout = new Button();
            btnLogout.Text = "Logout";
            btnLogout.Size = new Size(150, 40);
            btnLogout.Location = new Point(210, 110);
            btnLogout.Click += (s, args) => logoutToolStripMenuItem_Click(s, args);

            quickBookingPanel.Controls.Add(lblQuickBooking);
            quickBookingPanel.Controls.Add(btnNewBooking);
            quickBookingPanel.Controls.Add(btnMyBookings);
            quickBookingPanel.Controls.Add(btnChangePassword);
            quickBookingPanel.Controls.Add(btnLogout);

            panelContent.Controls.Add(lblWelcome);
            panelContent.Controls.Add(quickBookingPanel);
        }

        private void bookingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is likely a menu, not a menu item
            // No action needed as clicking on a menu just opens the dropdown
        }

        private void newBookingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Load new booking form in panel
            panelContent.Controls.Clear();
            var bookingForm = new NewBookingForm();
            bookingForm.TopLevel = false;
            bookingForm.FormBorderStyle = FormBorderStyle.None;
            bookingForm.Dock = DockStyle.Fill;
            panelContent.Controls.Add(bookingForm);
            bookingForm.Show();
        }

        private void myBookingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Load my bookings form in panel
            panelContent.Controls.Clear();
            var myBookingsForm = new MyBookingForm();
            myBookingsForm.TopLevel = false;
            myBookingsForm.FormBorderStyle = FormBorderStyle.None;
            myBookingsForm.Dock = DockStyle.Fill;
            panelContent.Controls.Add(myBookingsForm);
            myBookingsForm.Show();
        }

        private void systemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is likely a menu, not a menu item
            // No action needed as clicking on a menu just opens the dropdown
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show change password dialog
            var changePasswordForm = new ChangePasswordForm();
            changePasswordForm.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Confirm logout
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Session.Logout();
                this.Close();

                // Show login form
                var loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {
            // This event is not needed for functionality
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // This event is not needed for functionality
        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // This event is not needed for functionality
        }

        private void UserDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If application is exiting, don't show login form
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                return;
            }

            // If user is still logged in and form is closing, confirm exit
            if (Session.IsLoggedIn && e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}