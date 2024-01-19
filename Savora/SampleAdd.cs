using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Savora
{
    public partial class SampleAdd : KryptonForm
    {
        public SampleAdd()
        {
            InitializeComponent();
        }

        private void SampleAdd_Load(object sender, EventArgs e)
        {

        }

        public virtual void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public virtual void btnSave_Click(object sender, EventArgs e)
        {

        }

       
    }
}
