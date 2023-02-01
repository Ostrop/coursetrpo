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
        public bool areOpened;

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
            tabControl1_SelectedIndexChanged(null, null);
        }

        private void RegistrationPanel_Activated(object sender, EventArgs e)
        {
            label3.Text = DateTime.Today.ToShortDateString().ToString();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Расписание")
            {
                ds = new DataSet();
                sqlstring = "SELECT id_patient, procedure_date AS 'Дата', naming AS 'Должность', " +
                    "surname AS 'Фамилия',  name AS 'Имя', patronymic AS 'Отчество', procedure_time AS 'Время', " +
                    "appear AS 'Явка'FROM Assigned_procedures INNER JOIN Staff ON Staff.id_staff = Assigned_procedures.id_staff " +
                    "INNER JOIN Posts ON Staff.id_post = Posts.id_post WHERE naming != 'Системный администратор' AND naming != 'Регистратор' " +
                    "AND naming != 'Главный врач' AND naming != 'Медсестра (медбрат)'";
                if (checkBox1.Checked)
                    sqlstring += $" AND procedure_date >= DATEADD(day, -1, convert(date, getdate()))";
                adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
                adapter.Fill(ds);
                sqlstring = "SELECT id_patient, examination_date AS 'Дата', naming AS 'Должность', " +
                    "surname AS 'Фамилия',  name AS 'Имя', patronymic AS 'Отчество',examination_time AS 'Время', " +
                    "appear AS 'Явка' FROM Examinations INNER JOIN Staff ON Staff.id_staff = Examinations.id_staff " +
                    "INNER JOIN Posts ON Staff.id_post = Posts.id_post WHERE naming != 'Системный администратор' AND naming != 'Регистратор' " +
                    "AND naming != 'Главный врач' AND naming != 'Медсестра (медбрат)'";
                if (checkBox1.Checked)
                    sqlstring += $" AND examination_date >= DATEADD(day, -1, convert(date, getdate()))";
                adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                ds = new DataSet();
                adapter = new SqlDataAdapter("SELECT * FROM Medical_cards", dataBase.getConnection());
                adapter.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.Columns[0].Visible = false;
            }
        }

        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedRow = e.RowIndex;
                if (tabControl1.SelectedTab.Text == "Расписание")
                {
                    DataGridViewRow row = dataGridView1.Rows[selectedRow];
                    MedCard form = new MedCard(Convert.ToInt32(row.Cells[0].Value), false, this);
                    form.ShowDialog();
                }
                else
                {
                    DataGridViewRow row = dataGridView2.Rows[selectedRow];
                    MedCard form = new MedCard(Convert.ToInt32(row.Cells[0].Value), false, this);
                    form.ShowDialog();
                }
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(null, null);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            sqlstring = $"SELECT * FROM Medical_cards WHERE concat(surname, name, patronymic, passport, residential_address) like '%{textBox2.Text}%'";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            sqlstring = "SELECT id_patient, procedure_date AS 'Дата', naming AS 'Должность', " +
                "surname AS 'Фамилия',  name AS 'Имя', patronymic AS 'Отчество', procedure_time AS 'Время', " +
                "appear AS 'Явка' FROM Assigned_procedures INNER JOIN Staff ON Staff.id_staff = Assigned_procedures.id_staff " +
                "INNER JOIN Posts ON Staff.id_post = Posts.id_post WHERE naming != 'Системный администратор' AND naming != 'Регистратор' " +
                "AND naming != 'Главный врач' AND naming != 'Медсестра (медбрат)' AND concat(procedure_date, naming, surname, name, patronymic" +
                $" procedure_time) like '%{textBox1.Text}%'";
            if (checkBox1.Checked)
                sqlstring += $" AND procedure_date >= DATEADD(day, -1, convert(date, getdate()))";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            sqlstring = "SELECT id_patient, examination_date AS 'Дата', naming AS 'Должность', " +
                "surname AS 'Фамилия',  name AS 'Имя', patronymic AS 'Отчество',examination_time AS 'Время', " +
                "appear AS 'Явка' FROM Examinations INNER JOIN Staff ON Staff.id_staff = Examinations.id_staff " +
                "INNER JOIN Posts ON Staff.id_post = Posts.id_post WHERE naming != 'Системный администратор' AND naming != 'Регистратор' " +
                "AND naming != 'Главный врач' AND naming != 'Медсестра (медбрат)' AND concat(examination_date, naming, surname, name, patronymic" +
                $" examination_time) like '%{textBox1.Text}%'";
            if (checkBox1.Checked)
                sqlstring += $" AND examination_date >= DATEADD(day, -1, convert(date, getdate()))";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Visible = false;
        }

    }
}
