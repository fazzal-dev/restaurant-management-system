using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections;
using System;
using System.Windows.Forms;

namespace Savora
{
    public class DatabaseHelper
    {
        public static string conn_string = "Data Source=FAZZAL;Initial Catalog=savora;Integrated Security=True; TrustServerCertificate=True; ";

        public async static Task<bool> ExecuteNonQuery(string query)
        {
            using (SqlConnection connection =  new SqlConnection(conn_string))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query,connection))
                {
                    int rowsEffected = await cmd.ExecuteNonQueryAsync();
                    if(rowsEffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
        }

        public async static Task<bool> ExecuteReader(string query)
        {
            using (SqlConnection connection = new SqlConnection(conn_string))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        bool hasRows = reader.HasRows;
                        return hasRows;
                    }
                    
                }
            }

        }


        public async static Task<DataTable> ExecuteDataTable(string query)
        {
            DataTable dt = new DataTable();

            using (SqlConnection  connection = new SqlConnection(conn_string))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query,connection))
                {
                    using (SqlDataReader reader  = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

        public static async Task<object> ExecuteScalar(string query)
        {
            using (SqlConnection connection = new SqlConnection(conn_string))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return await command.ExecuteScalarAsync();
                }
            }
        }

        public async static Task<string> GetUserRole(string username)
        {
            string query = $"SELECT role FROM users WHERE username = '{username}'";
            object result = await ExecuteScalar(query);
            return result != null ? result.ToString() : null;
        }

        public async static Task<bool> IsUsernameAvailable(string username)
        {
            string query = $"SELECT COUNT(*) FROM users WHERE Username = '{username}'";
            Object count = await ExecuteScalar(query);

            // If count is 0, the username is available; otherwise, it's not
            return (int)count == 0;
        }


        public async static Task<int> CURD(string query,Hashtable ht)
        {
            int res = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(conn_string))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.CommandType = CommandType.Text;

                    foreach (DictionaryEntry item in ht)
                    {
                        cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                    } 

                    if (connection.State == ConnectionState.Closed) { await connection.OpenAsync(); }

                    res = await cmd.ExecuteNonQueryAsync();
                    
                }

                


            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return res;
        }


        public async static Task LoadData(string query, DataGridView gv, ListBox lb)
        {
            //Serial No
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);

            try
            {
                using(SqlConnection connection = new SqlConnection(conn_string))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.CommandType = CommandType.Text;
                    //Create a SqlDataAdapter to fill data from the database
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd); 

                    DataTable dt = new DataTable();

                    await Task.Run(()=> sqlDataAdapter.Fill(dt));

                    for(int i = 0; i< lb.Items.Count; i++)
                    {
                        //Get the column name from the ListBox
                        string col = ((DataGridViewColumn)lb.Items[i]).Name;
                        //Map DataGridView columns to DataTable columns
                        gv.Columns[col].DataPropertyName = dt.Columns[i].ToString();
                    }

                    gv.DataSource = dt;
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;

            int count = 0;
            foreach(DataGridViewRow row in gv.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        public async static Task getCategories(string query,ComboBox cb)
        {
            using(SqlConnection connection = new SqlConnection(conn_string))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                await Task.Run(()=> adapter.Fill(dt));

                cb.DisplayMember = "Name";
                cb.ValueMember = "id";
                cb.DataSource = dt;
                cb.SelectedIndex = -1;

            }
        }

    }
}
