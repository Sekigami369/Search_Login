using System;
using System.Data;
using System.Data.SqlClient;


namespace Search_Login
{
    public partial class MainForm : Form
    {

        string connectionString = "Server=localhost;Database=MyDatabase;Trusted_Connection=true;";
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchQuery = textBox1.Text;
            string[] keywords = searchQuery.Split(' ');

            string baseQuery = "SELECT * FROM TestTable WHERE ";
            string dynamicCondition = "" ;

            List<SqlParameter> parameters = new List<SqlParameter>();

            string[] seachColumns = { "Column1", "Column2", "Column3", "Column4", "Column5", "Column6" };

            
            for(int i = 0; i < keywords.Length; i++)
            {
                string paramName = "@keyword" + i;//「i」はインデックス番号として追加。無くても検索に影響なし

                parameters.Add(new SqlParameter(paramName, " '%" + keywords[i] + "%' "));

                foreach (string searchColumn in seachColumns)
                {
                    dynamicCondition += searchColumn + " LIKE " + paramName;//変数に入れて　ログに出力してみてもう一度見てみる    

                    if (i < keywords.Length)
                    {
                        dynamicCondition += " OR ";
                    }
                }
            }
            dynamicCondition = dynamicCondition.Remove(dynamicCondition.Length - 4);
            string finalQuery = baseQuery + dynamicCondition + ";" ;
           
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(finalQuery, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("検索結果がありません");
                    }
                    else
                    { 
                        dataGridView1.DataSource = dataTable;
                    }
                }   
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

