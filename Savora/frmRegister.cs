using System;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Savora
{
    public partial class frmRegister : KryptonForm
    {
       
        public frmRegister()
        {
            InitializeComponent();

        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                string selectedRole = cbRole.SelectedItem?.ToString();
                if (string.IsNullOrWhiteSpace(selectedRole))
                {
                    ShowValidationError("Please select a role.");
                    return;
                }

                bool isUsernameAvailable = await DatabaseHelper.IsUsernameAvailable(txtUsername.Text);
                if (!isUsernameAvailable)
                {
                    ShowValidationError("The chosen username is not available. Please choose a different one.");
                    ClearFormFields();
                    txtUsername.Focus();
                    return;
                }

                string register = $"INSERT INTO users VALUES('{txtUsername.Text}','{txtPassword.Text}', '{selectedRole}')";
                bool success = await DatabaseHelper.ExecuteNonQuery(register);

                if (success)
                {
                    MessageBox.Show("Account Created Successfully!", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFormFields();
                    txtUsername.Focus();
                }
                else
                {
                    ShowError("There was a problem creating an account");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void chkbxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(chkbxShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
            }   
            else
            {
                txtPassword.PasswordChar = '•';
                txtConfirmPassword.PasswordChar = '•';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtUsername.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                ShowValidationError("All fields are required.");
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ShowValidationError("Password does not match.");
                return false;
            }

            return true;
        }

       

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {

            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();

            this.Hide();  

        }

        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ClearFormFields()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            cbRole.SelectedIndex = -1;
        }
    }
}
