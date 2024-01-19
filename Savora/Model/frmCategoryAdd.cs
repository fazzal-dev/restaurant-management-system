using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savora.Model
{
    public partial class frmCategoryAdd : SampleAdd
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }

        public int id = 0;

        public async override void btnSave_Click(object sender, EventArgs e)
        {
            string query = string.Empty;
            
            if(id == 0)
            {
                query = "INSERT INTO category Values(@Name)";
            }
            else
            {
                query = "UPDATE category SET Name = @Name WHERE ID = @ID";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@ID", id);
            ht.Add("@Name", txtAddCategory.Text);

            int rowsAffected = await DatabaseHelper.CURD(query, ht);
            if(rowsAffected > 0)
            {
                MessageBox.Show("Saved Successfully!","Save",MessageBoxButtons.OK,MessageBoxIcon.Information);
                id = 0;
                txtAddCategory.Text = string.Empty;
                txtAddCategory.Focus();
            }
        }

    }
}
