using Savora.Model;
using Savora.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savora
{
    //taskkill /F /IM savora.exe
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        public async static void test()
        {
            AllocConsole();
            DataTable dt = await DatabaseHelper.ExecuteDataTable("SELECT * FROM users WHERE username = 'fazzal' AND password = '123'");
            string user = dt.Rows[0]["username"].ToString();
            Console.WriteLine(user);

            //FreeConsole();
        }

        [STAThread]
        static  void Main()
        {

            //test();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
