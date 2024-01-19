using System;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Savora
{
    public partial class frmLogin : KryptonForm
    {
        public static string LoggedInUsername { get; set; }
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
           frmRegister frmRegister= new frmRegister();
           frmRegister.Show();

           this.Hide();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    string login = $"SELECT * FROM users WHERE username = '{txtUsername.Text}' AND password = '{txtPassword.Text}'";
                    bool loginSuccess = await DatabaseHelper.ExecuteReader(login);

                    if (loginSuccess)
                    {
                    LoggedInUsername = txtUsername.Text;
                    await Dashboard.getUser(txtUsername.Text, txtPassword.Text);
                    new Dashboard().Show();
                    this.Hide();
                }
                    else
                    {
                        MessageBox.Show("Login Failed. Please check your credentials.","Login Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtUsername.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        txtUsername.Focus();
                    }

                }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUsername.Focus();
        }

        private void chkbxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
               
            }
            else
            {
                txtPassword.PasswordChar = '•';
                
            }
        }

    }
}
