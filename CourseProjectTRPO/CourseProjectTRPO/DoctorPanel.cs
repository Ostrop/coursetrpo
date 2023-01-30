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
    public partial class DoctorPanel : Form
    {
        checkUser user;
        DataBase dataBase = new DataBase();
        SqlDataAdapter adapter;
        DataSet ds;
        DataSet dataSet;
        string sqlstring = string.Empty;
        int selectedRow;
        public DoctorPanel(checkUser user1)
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
            ds = new DataSet();
            sqlstring = "SELECT naming, doc_comments FROM Diagnoses";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            ds = new DataSet();
            sqlstring = "SELECT type_procedure, price FROM Procedures";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage1);
        }

        private void DoctorPanel_Activated(object sender, EventArgs e)
        {
            label3.Text = DateTime.Today.ToShortDateString().ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            sqlstring = "SELECT Medical_cards.id_patient, procedure_date AS 'Дата', procedure_time AS 'Время', " +
                "Medical_cards.surname AS 'Фамилия', Medical_cards.name AS 'Имя', Medical_cards.patronymic AS 'Отчество', " +
                "appear AS 'Явка' FROM Assigned_procedures INNER JOIN Medical_cards ON Medical_cards.id_patient = Assigned_procedures.id_patient " +
                $"INNER JOIN Staff ON Staff.id_staff = Assigned_procedures.id_staff WHERE Staff.surname = '{user.Surname}' AND Staff.name = '{user.Name}' " +
                $"AND Staff.patronymic = '{user.Patronymic}'";
            if (checkBox1.Checked)
                sqlstring += " AND procedure_date >= convert(date, getdate())";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            sqlstring = "SELECT Medical_cards.id_patient, examination_date AS 'Дата', examination_time AS 'Время', " +
                "Medical_cards.surname AS 'Фамилия', Medical_cards.name AS 'Имя', Medical_cards.patronymic AS 'Отчество', " +
                "appear AS 'Явка' FROM Examinations INNER JOIN Medical_cards ON Medical_cards.id_patient = Examinations.id_patient " +
                $"INNER JOIN Staff ON Staff.id_staff = Examinations.id_staff WHERE Staff.surname = '{user.Surname}' AND Staff.name = '{user.Name}' " +
                $"AND Staff.patronymic = '{user.Patronymic}'";
            if (checkBox1.Checked)
                sqlstring += $" AND examination_date >= convert(date, getdate())";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            ds = new DataSet();
            sqlstring = "SELECT Medical_cards.id_patient, procedure_date AS 'Дата', procedure_time AS 'Время', " +
                "Medical_cards.surname AS 'Фамилия', Medical_cards.name AS 'Имя', Medical_cards.patronymic AS 'Отчество', " +
                "appear AS 'Явка' FROM Assigned_procedures INNER JOIN Medical_cards ON Medical_cards.id_patient = Assigned_procedures.id_patient " +
                $"INNER JOIN Staff ON Staff.id_staff = Assigned_procedures.id_staff WHERE Staff.surname = '{user.Surname}' AND Staff.name = '{user.Name}' " +
                $"AND Staff.patronymic = '{user.Patronymic}' AND concat(procedure_date, procedure_time, Medical_cards.surname, Medical_cards.name, " +
                $"Medical_cards.patronymic) like '%{textBox1.Text}%'";
            if (checkBox1.Checked)
                sqlstring += $" AND procedure_date >= convert(date, getdate())";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            sqlstring = "SELECT Medical_cards.id_patient, examination_date AS 'Дата', examination_time AS 'Время', " +
                "Medical_cards.surname AS 'Фамилия', Medical_cards.name AS 'Имя', Medical_cards.patronymic AS 'Отчество', " +
                "appear AS 'Явка' FROM Examinations INNER JOIN Medical_cards ON Medical_cards.id_patient = Examinations.id_patient " +
                $"INNER JOIN Staff ON Staff.id_staff = Examinations.id_staff WHERE Staff.surname = '{user.Surname}' AND Staff.name = '{user.Name}' " +
                $"AND Staff.patronymic = '{user.Patronymic}' AND concat(examination_date, examination_time, Medical_cards.surname, Medical_cards.name, " +
                $"Medical_cards.patronymic) like '%{textBox1.Text}%'";
            if (checkBox1.Checked)
                sqlstring += $" AND examination_date >= convert(date, getdate())";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(null, null);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab.Text == "Расписание")
                {
                    selectedRow = e.RowIndex;
                    DataGridViewRow row = dataGridView1.Rows[selectedRow];
                    MedCard form = new MedCard(Convert.ToInt32(row.Cells[0].Value), true);
                    form.ShowDialog();
                }
                else
                {
                    MedCard form = new MedCard(Convert.ToInt32(dataSet.Tables[0].Rows[0].ItemArray[1]), true);
                    form.ShowDialog();
                }
            }
            catch { }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            sqlstring = $"SELECT naming, doc_comments FROM Diagnoses where concat(naming, doc_comments) like '%{textBox2.Text}%'";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            sqlstring = $"SELECT type_procedure, price FROM Procedures where concat(type_procedure, price) like '%{textBox3.Text}%'";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dataSet = new DataSet();
                sqlstring = $"SELECT id_assigned_procedure, id_patient, type_procedure FROM Assigned_procedures INNER JOIN Staff ON Staff.id_staff = Assigned_procedures.id_staff " +
                    $"INNER JOIN Procedures ON Procedures.id_procedure = Assigned_procedures.id_procedure WHERE id_assigned_procedure = " +
                    $"{id_numer.Value} AND procedure_time = '{time_masked.Text}' AND procedure_date = '{label3.Text}' AND surname = '{user.Surname}' " +
                    $"AND name = '{user.Name}' AND patronymic = '{user.Patronymic}'";
                adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count == 1)
                {
                    id_numer.Enabled = time_masked.Enabled = false;
                    button1.Enabled = button2.Enabled = button3.Enabled = richTextBox1.Enabled = true;
                    label12.Visible = label13.Visible = true;
                    label13.Text = dataSet.Tables[0].Rows[0].ItemArray[2].ToString();
                    tabControl1.TabPages.Remove(tabPage1);
                    return;
                }
                else
                    dataSet = new DataSet();
                sqlstring = $"SELECT id_examination, id_patient FROM Examinations  INNER JOIN Staff ON Staff.id_staff = Examinations.id_staff " +
                   $"WHERE id_examination = {id_numer.Value} AND examination_time = '{time_masked.Text}' AND examination_date = '{label3.Text}' AND surname = '{user.Surname}' " +
                   $"AND name = '{user.Name}' AND patronymic = '{user.Patronymic}'";
                adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
                dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count == 1)
                {
                    id_numer.Enabled = time_masked.Enabled = false;
                    button1.Enabled = button2.Enabled = button3.Enabled = richTextBox1.Enabled = true;
                }
                else
                    dataSet = new DataSet();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1_CellClick(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            sqlstring = $"SELECT id_assigned_procedure, id_patient FROM Assigned_procedures INNER JOIN Staff ON Staff.id_staff = Assigned_procedures.id_staff " +
                $"WHERE id_assigned_procedure = {id_numer.Value} AND procedure_time = '{time_masked.Text}' AND procedure_date = '{label3.Text}' AND surname = '{user.Surname}' " +
                $"AND name = '{user.Name}' AND patronymic = '{user.Patronymic}'";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count == 1)
            {
                sqlstring = $"update Assigned_procedures set doctors_report = '{richTextBox1.Text}', appear = 1 where id_assigned_procedure = {ds.Tables[0].Rows[0].ItemArray[0]}";
                var command = new SqlCommand(sqlstring, dataBase.getConnection());
                dataBase.openConnection();
                command.ExecuteNonQuery();
                dataBase.closedConnection();
                button3_Click(null, null);
                return;
            }

            ds = new DataSet();
            sqlstring = $"SELECT id_examination, id_patient FROM Examinations  INNER JOIN Staff ON Staff.id_staff = Examinations.id_staff " +
                $"WHERE id_examination = {id_numer.Value} AND examination_time = '{time_masked.Text}' AND examination_date = '{label3.Text}' AND surname = '{user.Surname}' " +
                $"AND name = '{user.Name}' AND patronymic = '{user.Patronymic}'";
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count == 1)
            {
                sqlstring = $"update Examinations set doc_comments = '{richTextBox1.Text}', appear = 1 where id_examination {ds.Tables[0].Rows[0].ItemArray[0]}";
                var command = new SqlCommand(sqlstring, dataBase.getConnection());
                dataBase.openConnection();
                command.ExecuteNonQuery();
                dataBase.closedConnection();
                button3_Click(null, null);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            id_numer.Enabled = time_masked.Enabled = true;
            button1.Enabled = button2.Enabled = button3.Enabled = richTextBox1.Enabled = false;
            time_masked.Text = id_numer.Text = richTextBox1.Text = string.Empty;
            label12.Visible = label13.Visible = false;
            tabControl1.TabPages.Add(tabPage1);
        }
    }
}
