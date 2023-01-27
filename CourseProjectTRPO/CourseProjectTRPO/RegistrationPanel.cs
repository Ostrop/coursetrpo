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
    public partial class RegistrationPanel : Form
    {
        checkUser user;
        DataBase dataBase = new DataBase();
        SqlDataAdapter adapter;
        DataSet ds;
        string sqlstring = string.Empty;
        int selectedRow;

        public RegistrationPanel(checkUser user1)
        {
            InitializeComponent();
            //изменение размера окна
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            user = user1;
            label2.Text = user.Surname + " " + user.Name + " " + user.Patronymic;
        }

        private void RegistrationPanel_Activated(object sender, EventArgs e)
        {
            label3.Text = DateTime.Today.ToShortDateString().ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            sqlstring = "SELECT procedure_date AS 'Дата', naming AS 'Должность', surname AS 'Фамилия',  name AS 'Имя', patronymic AS 'Отчество', procedure_time AS 'Время', appear AS 'Явка' FROM Assigned_procedures INNER JOIN Staff ON Staff.id_staff = Assigned_procedures.id_staff INNER JOIN Posts ON Staff.id_post = Posts.id_post";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            sqlstring = "SELECT examination_date AS 'Дата', naming AS 'Должность', surname AS 'Фамилия',  name AS 'Имя', patronymic AS 'Отчество',examination_time AS 'Время', appear AS 'Явка' FROM Examinations INNER JOIN Staff ON Staff.id_staff = Examinations.id_staff INNER JOIN Posts ON Staff.id_post = Posts.id_post";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
