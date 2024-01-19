using Savora.Utilities;
using System;
using System.Collections;
using System.Net;
using System.Windows.Forms;

namespace Savora.Model
{
    public partial class frmStaffAdd : SampleAdd
    {
        public frmStaffAdd()
        {
            InitializeComponent();
        }

        public int id = 0;

        public async override void btnSave_Click(object sender, EventArgs e)
        {
            string query = string.Empty;

            if (id == 0)
            {
                query = "INSERT INTO staff Values(@Name, @Phone, @Role)";
            }
            else
            {
                query = "UPDATE staff SET Name = @Name, Phone = @Phone, Role = @Role WHERE ID = @ID";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@ID", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@Phone", txtPhone.Text);
            ht.Add("@Role", cbRole.Text);

            int rowsAffected = await DatabaseHelper.CURD(query, ht);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Saved Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id = 0;
                txtName.Text = string.Empty;
                txtPhone.Text = string.Empty;
                cbRole.SelectedIndex = -1;
                txtName.Focus();
            }
        }
    }
}
