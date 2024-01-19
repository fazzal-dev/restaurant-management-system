using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Savora.View;

namespace Savora
{
    public partial class Dashboard : KryptonForm
    {

        public Dashboard()
        {
            InitializeComponent();
        }
        // Singleton Pattern: Dashboard class only has one global instance 
        static Dashboard _obj;

        public static Dashboard Instance
        {
            get{ if(_obj == null ) {_obj = new Dashboard();} return _obj; }
        }

        private static string user;
        private static string role;

        public static string User
        {
            get { return user; }
            private set { user = value; }
        }


        public static async Task getUser(string username,string password)
        {   
            string query = $"SELECT * FROM users WHERE username = '{username}' AND password = '{password}'";
            DataTable dt = await DatabaseHelper.ExecuteDataTable(query);

            User = dt.Rows.Count > 0 ? dt.Rows[0]["username"].ToString() : string.Empty;
            role = await DatabaseHelper.GetUserRole(username);
            
        }

        private void UpdateUserInfo()
        {
            lblUsername.Text = User;
            lblRole.Text = role;
        }

        private  void Dashboard_Load(object sender, EventArgs e)
        {
            UpdateUserInfo();
            _obj = this;
        }

        public async Task AddControls(Form form)
        {
            DashboardPanel.Controls.Clear();
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            DashboardPanel.Controls.Add(form);
            form.Show();
        }

        private async void btnHome_Click(object sender, EventArgs e)
        {
            await AddControls(new frmHome());
        }

        private async void btnCategories_Click(object sender, EventArgs e)
        {
             await AddControls(new frmCategoryView());
        }

        private async void btnTables_Click(object sender, EventArgs e)
        {
            await AddControls(new frmTableView());
        }

        private async void btnStaff_Click(object sender, EventArgs e)
        {
            await AddControls(new frmStaffView());
        }

        private async void btnProducts_Click(object sender, EventArgs e)
        {
            await AddControls(new frmProductView());
        }
    }
}
