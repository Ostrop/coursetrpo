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
                var user = new checkUser(table.Rows[0].ItemArray[1].ToString(), table.Rows[0].ItemArray[2].ToString(), table.Rows[0].ItemArray[3].ToString(), table.Rows[0].ItemArray[4].ToString(), table.Rows[0].ItemArray[5].ToString());
                switch (user)
                {
                    //case "":
                }
                //this.Hide();

                //this.Show();
            }
            else
                MessageBox.Show("Такого аккаунта не существует!", "Аккаунт не существует!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
