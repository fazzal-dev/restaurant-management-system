using System;
using System.Collections;
using System.Windows.Forms;

namespace Savora.Model
{
    public partial class frmTableAdd : SampleAdd
    {
        public frmTableAdd()
        {
            InitializeComponent();
        }

        public int id = 0;

        public async override void btnSave_Click(object sender, EventArgs e)
        {
            string query = string.Empty;

            if (id == 0)
            {
                query = "INSERT INTO tables Values(@Name)";
            }
            else
            {
                query = "UPDATE tables SET Name = @Name WHERE ID = @ID";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@ID", id);
            ht.Add("@Name", txtAddTable.Text);

            int rowsAffected = await DatabaseHelper.CURD(query, ht);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Saved Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id = 0;
                txtAddTable.Text = string.Empty;
                txtAddTable.Focus();
            }
        }
    }
}
