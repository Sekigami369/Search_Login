using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Search_Login
{
    public partial class Login : Form
    {
        bool isLoggedIn = false;
        string connectionString = "Server=localhost;Database=MyDatabase;Trusted_Connection=true;";


        public Login()
        {
            InitializeComponent();

            textBox1.PasswordChar = '●';
            textBox2.PasswordChar = '●';
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void text_UserID(object sender, EventArgs e)
        {

        }

        private void text_UserPass(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string userId = textBox1.Text;
                string passWord = textBox2.Text;

                if (IsAlphaNumeric(userId, passWord))
                {
                    throw new Exception("半角英数字で入力してください。");
                }

                if (!IsValidCheck(userId, passWord))
                {
                    MessageBox.Show("ユーザーIDとパスワードを入力してください。");
                    return;
                }

                if (IdAndPassCheck(userId, passWord))
                {
                    isLoggedIn = true;
                    MainForm mainForm = new MainForm();  //後であたらしいフォーム画面を作る  
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("ユーザーIDまたはパスワードが正しくありません");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //記入された文字が半角英数字かをチェックするメソッド
        private bool IsAlphaNumeric(string userId, string passWord)
        {
            string pattern = @"[^\x00-\x7F]";//半角英筋以外の文字を含む場合マッチする

            if (Regex.IsMatch(userId, pattern) || Regex.IsMatch(passWord, pattern))
            {
                return true;
            }
            return false;
        }

        //入力がnullか空文字かをチェックするメソッド
        private bool IsValidCheck(string userId, string passWord)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(passWord))
            {
                return false;
            }
            return true;
        }

        //IDとPASSが正しいかチェックするメソッド
        private bool IdAndPassCheck(string userId, string passWord)
        {
            //テーブル名とその他は後でちゃんと書く
            string query = "SELECT COUNT(*) FROM idPass WHERE id = @id AND pass = @pass";
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", userId);
                    command.Parameters.AddWithValue("@pass", passWord);

                    connection.Open();

                    count = (int)command.ExecuteScalar();
                }
            }
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox1.PasswordChar == '\0' || textBox2.PasswordChar == '\0')
            {
                textBox1.PasswordChar = '●';
                textBox2.PasswordChar = '●';
            }
            else
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';
            }

        }
    }
}