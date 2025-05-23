using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusTicketSystem.BLL;
using BusTicketSystem.Config;

namespace BusTicketSystem.GUI
{
    public partial class LoginForm : Form
    {
        private readonly AccountBLL _accountBLL;

        public LoginForm()
        {
            InitializeComponent();
            _accountBLL = new AccountBLL();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // Optional: You can add validation here if needed
            // For example, disable the login button if the username is empty
            btnLogin.Enabled = !string.IsNullOrWhiteSpace(txtUsername.Text) &&
                              !string.IsNullOrWhiteSpace(txtPassword.Text);
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Optional: You can add validation here if needed
            // For example, disable the login button if the password is empty
            btnLogin.Enabled = !string.IsNullOrWhiteSpace(txtUsername.Text) &&
                              !string.IsNullOrWhiteSpace(txtPassword.Text);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Attempt to login
                var account = _accountBLL.Login(username, password);

                if (account != null)
                {
                    // Store the current user in session
                    Session.CurrentUser = account;

                    // Open the appropriate dashboard based on user role
                    if (Session.IsAdmin)
                    {
                        var adminDashboard = new AdminDashboard();
                        this.Hide();
                        adminDashboard.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        var userDashboard = new UserDashboard();
                        this.Hide();
                        userDashboard.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}