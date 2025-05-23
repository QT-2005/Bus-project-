using BusTicketSystem.BLL;
using BusTicketSystem.Config;
using System;
using System.Windows.Forms;

namespace BusTicketSystem.GUI
{
    public partial class ChangePasswordForm : Form
    {
        private readonly AccountBLL _accountBLL;
        private bool _isCurrentPasswordValid = false;
        private bool _isNewPasswordValid = false;
        private bool _isConfirmPasswordValid = false;

        public ChangePasswordForm()
        {
            InitializeComponent();
            _accountBLL = new AccountBLL();
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            // Set form title with current username
            this.Text = $"Change Password - {Session.CurrentUser.Username}";
            txtCurrentPassword.Focus();
            UpdateSaveButtonState();
        }

        private void UpdateSaveButtonState()
        {
            btnSave.Enabled = _isCurrentPasswordValid && _isNewPasswordValid && _isConfirmPasswordValid;
        }

        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            _isCurrentPasswordValid = !string.IsNullOrWhiteSpace(txtCurrentPassword.Text);
            UpdateSaveButtonState();
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            _isNewPasswordValid = !string.IsNullOrWhiteSpace(newPassword) && newPassword.Length >= 6;

            // Recheck confirm password validity
            if (!string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                _isConfirmPasswordValid = txtConfirmPassword.Text == newPassword;
            }

            UpdateSaveButtonState();
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            _isConfirmPasswordValid = !string.IsNullOrWhiteSpace(txtConfirmPassword.Text) &&
                                      txtConfirmPassword.Text == txtNewPassword.Text;
            UpdateSaveButtonState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Extra validation
            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirmation do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("New password must be at least 6 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                bool success = _accountBLL.ChangePassword(Session.CurrentUser.Id, currentPassword, newPassword);

                if (success)
                {
                    MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Current password is incorrect.", "Change Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCurrentPassword.Focus();
                    txtCurrentPassword.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
