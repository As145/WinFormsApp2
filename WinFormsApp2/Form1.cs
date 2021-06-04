using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        //connection string
        private string connectionString = @"Data Source=DESKTOP-5PNC170\SQLEXPRESS;Initial Catalog=Golf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
        SqlConnection Con = new SqlConnection();
        DataTable GolfTable = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadDB_Click(object sender, EventArgs e)
        {
            loaddb();

        }
        private void loaddb()
        {
            //load datatable colums
            datatablecolumns();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string QueryString = @"SELECT * FROM Golf order by ID";
                //open your connection 
                connection.Open();

                SqlCommand Command = new SqlCommand(QueryString, connection);
                //start your DB reader

                SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read()) //looping through data
                {
                    //add in each row to the datatable
                    GolfTable.Rows.Add(
                        reader["ID"],
                        reader["Title"],
                        reader["Firstname"],
                        reader["Surname"],
                        reader["Gender"],
                        reader["DOB"],
                        reader["Street"],
                        reader["Suburb"],
                        reader["City"],
                        reader["Available week days"],
                        reader["Handicap"]);

                }
                reader.Close();//close reader
                connection.Close();//close connection
                // add the datatable into your data grid view
                dgvGolf.DataSource = GolfTable;

            }
        }

        private void datatablecolumns()
        {

            //clear the old data
            GolfTable.Clear();
            //add in the column titlees to the datable
            try
            {
                GolfTable.Columns.Add("ID");
                GolfTable.Columns.Add("Title");
                GolfTable.Columns.Add("Firstname");
                GolfTable.Columns.Add("Surname");
                GolfTable.Columns.Add("Gender");
                GolfTable.Columns.Add("DOB");
                GolfTable.Columns.Add("Street");
                GolfTable.Columns.Add("Suburb");
                GolfTable.Columns.Add("City");
                GolfTable.Columns.Add("Availavle week days");
                GolfTable.Columns.Add("Handicap");

            }
            catch (Exception s)
            {
                //MessageBox.Show("Data Table not loading"+ s.Message);
            }
        }


    }
}

