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

            textBox1.PasswordChar = '��';
            textBox2.PasswordChar = '��';
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
                    throw new Exception("���p�p�����œ��͂��Ă��������B");
                }

                if (!IsValidCheck(userId, passWord))
                {
                    MessageBox.Show("���[�U�[ID�ƃp�X���[�h����͂��Ă��������B");
                    return;
                }

                if (IdAndPassCheck(userId, passWord))
                {
                    isLoggedIn = true;
                    MainForm mainForm = new MainForm();  //��ł����炵���t�H�[����ʂ����  
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("���[�U�[ID�܂��̓p�X���[�h������������܂���");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //�L�����ꂽ���������p�p���������`�F�b�N���郁�\�b�h
        private bool IsAlphaNumeric(string userId, string passWord)
        {
            string pattern = @"[^\x00-\x7F]";//���p�p�؈ȊO�̕������܂ޏꍇ�}�b�`����

            if (Regex.IsMatch(userId, pattern) || Regex.IsMatch(passWord, pattern))
            {
                return true;
            }
            return false;
        }

        //���͂�null���󕶎������`�F�b�N���郁�\�b�h
        private bool IsValidCheck(string userId, string passWord)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(passWord))
            {
                return false;
            }
            return true;
        }

        //ID��PASS�����������`�F�b�N���郁�\�b�h
        private bool IdAndPassCheck(string userId, string passWord)
        {
            //�e�[�u�����Ƃ��̑��͌�ł����Ə���
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
                textBox1.PasswordChar = '��';
                textBox2.PasswordChar = '��';
            }
            else
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';
            }

        }
    }
}