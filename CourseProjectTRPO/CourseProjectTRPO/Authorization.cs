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

namespace CourseProjectTRPO
{
    public partial class Authorization : Form
    {
        DataBase dataBase = new DataBase();
        Form form1;
        public Authorization()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string login = textBox1.Text;
            string password = md5.hashPassword(textBox2.Text);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string str = $"SELECT naming, surname, name, patronymic, Staff.id_post FROM Staff INNER JOIN Posts ON Staff.id_post = Posts.id_post WHERE login = '{login}' AND password = '{password}'";
            SqlCommand command = new SqlCommand(str, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                var user = new checkUser(table.Rows[0].ItemArray[1].ToString(), table.Rows[0].ItemArray[2].ToString(), table.Rows[0].ItemArray[3].ToString(), table.Rows[0].ItemArray[4].ToString());
                switch (user.Post)
                {
                    case "Системный администратор":
                        form1 = new AdministrationPanel(user);
                        this.Hide();
                        form1.ShowDialog();
                        this.Show();
                        break;
                    case "Регистратор":
                        form1 = new RegistrationPanel(user);
                        this.Hide();
                        form1.ShowDialog();
                        this.Show();
                        break;
                    case "Главный врач":
                        form1 = new MainDoctorPanel(user);
                        this.Hide();
                        form1.ShowDialog();
                        this.Show();
                        break;
                    default:
                        form1 = new DoctorPanel(user);
                        this.Hide();
                        form1.ShowDialog();
                        this.Show();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Проверьте введённые данные", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Text = null;
            }
        }
    }
}
