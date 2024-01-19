using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savora.Model
{
    public partial class frmProductAdd : SampleAdd
    {
        public frmProductAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        public int cID = 0;

        private async void frmProductAdd_Load(object sender, EventArgs e)
        {
            string query = "SELECT ID 'id', Name 'name' FROM category";
            await DatabaseHelper.getCategories(query, cbCategory);

            if(cID > 0) //Update
            {
                cbCategory.SelectedValue = cID;

            }
        }

        //string FilePath;
        Byte[] ImageArray; 


        private void btnUpload_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jgp;*.jpeg;*.png;*.bmp";
                ofd.Title = "Select an Image File";

                if(ofd.ShowDialog() == DialogResult.OK) 
                {
                    string FilePath = ofd.FileName;
                    lblUploadPath.Text = Path.GetFileName(FilePath);
                    pbImage.Image = new Bitmap(FilePath);
                }
            }
        }

        public async override void btnSave_Click(object sender, EventArgs e)
        {
            string query = string.Empty;

            if (id == 0)
            {
                query = "INSERT INTO products Values(@Name, @Price, @CatID, @Image)";
            }
            else
            {
                query = "UPDATE products SET Name = @Name, Price = @Price, CategoryID = @CatID, Image = @Image WHERE ID = @ID";
            }

            Image img = new Bitmap(pbImage.Image);
            MemoryStream ms = new MemoryStream();
            img.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
            ImageArray = ms.ToArray();


            Hashtable ht = new Hashtable();
            ht.Add("@ID", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@Price", txtPrice.Text);
            ht.Add("@CatID", Convert.ToInt32(cbCategory.SelectedValue));
            ht.Add("@Image", ImageArray);
            int rowsAffected = await DatabaseHelper.CURD(query, ht);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Saved Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id = 0;
                txtName.Text = string.Empty;
                txtPrice.Text = string.Empty;
                cbCategory.SelectedIndex = -1;
                lblUploadPath.Text = string.Empty;
                pbImage.Image = null;
                txtName.Focus();
            }
        }
    }
}
