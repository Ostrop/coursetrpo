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
    public partial class MainDoctorPanel : Form
    {
        checkUser user;
        SqlDataAdapter adapter;
        DataSet ds;
        readonly string sqlstring = "SELECT Staff.id_staff as 'ID', surname as 'Фамилия', name as 'Имя', patronymic as 'Отчество', naming as 'Должность', " +
            "date as 'Дата' FROM Staff INNER JOIN Posts ON Posts.id_post = Staff.id_post INNER JOIN Timetable" +
            " ON Staff.id_staff = Timetable.id_staff";
        string newstring;
        DataBase dataBase = new DataBase();
        int selectedRow;
        enum RowState
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }
        public MainDoctorPanel(checkUser user1)
        {
            InitializeComponent();
            user = user1;
            //изменение размера окна
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            label2.Text = user.Surname + " " + user.Name + " " + user.Patronymic;
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            ds = new DataSet();
            adapter.Fill(ds);
            ds.Tables[0].Columns.Add("IsNew");
            dataGridView1.DataSource = ds.Tables[0];
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = RowState.ModifiedNew;

            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = false;

        }

        private void MainDoctorPanel_Activated(object sender, EventArgs e)
        {
            label3.Text = DateTime.Today.ToShortDateString().ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter(sqlstring + $" WHERE concat(surname,name,patronymic,naming,date) like '%{textBox1.Text}%'", dataBase.getConnection());
            ds = new DataSet();
            adapter.Fill(ds);
            ds.Tables[0].Columns.Add("IsNew");
            dataGridView1.DataSource = ds.Tables[0];
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = RowState.ModifiedNew;

            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[5].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox2.Visible = false;
            groupBox1.Text = "Создание новой записи";
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            newstring = "INSERT INTO Timetable (id_staff, date) VALUES (";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == string.Empty || textBox3.Text == string.Empty)
                    button7.Enabled = false;
                else
                    button7.Enabled = true;
            }
            catch { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            newstring += $"{textBox2.Text}, '{textBox3.Text}')";
            dataBase.openConnection();
            var command = new SqlCommand(newstring, dataBase.getConnection());
            command.ExecuteNonQuery();
            dataBase.closedConnection();
            groupBox3.Visible = false;
            groupBox2.Visible = true;
            groupBox1.Text = "Запись";
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            button7.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            groupBox2.Visible = true;
            groupBox1.Text = "Запись";
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }
        //Удалить
        private void button4_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.CurrentCell = null;
            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].ToString() == String.Empty)
            {
                dataGridView1.Rows[index].Cells[dataGridView1.ColumnCount - 1].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[dataGridView1.ColumnCount - 1].Value = RowState.Deleted;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            ds.Tables[0].Columns.Add("IsNew");
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = RowState.ModifiedNew;

            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = false;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var rowState = dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value.ToString();

                if (rowState == RowState.Deleted.ToString())
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var date = Convert.ToDateTime(dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 2].Value);
                    var deleteQuery = $"delete from timetable where id_staff = {id} AND date = '{date}'";
                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closedConnection();
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            {
                int sum = 0;
                if (comboBox1.Text == "Статистика о заболеваемости за указанный прошедший месяц")
                {
                    DateTime time = Convert.ToDateTime(textBox4.Text);
                    newstring = "SELECT naming as 'Название', COUNT(id_assigned_diagnosis) AS 'Кол-во заб.' FROM Assigned_diagnoses INNER JOIN Diagnoses " +
                        $"ON Assigned_diagnoses.id_diagnosis = Diagnoses.id_diagnosis where start_date <= convert(date, {time.AddMonths(1)}) GROUP BY naming";
                    adapter = new SqlDataAdapter(newstring, dataBase.getConnection());
                    ds = new DataSet();
                    adapter.Fill(ds);
                    ds.Tables[0].Columns.Add("%");
                    dataGridView2.DataSource = ds.Tables[0];
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        sum += Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        dataGridView2.Rows[i].Cells[2].Value = (Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value) / sum) * 100;


                }
                else if (comboBox1.Text == "Статистика о заболеваемости за указанный прошедший год")
                {
                    DateTime time = Convert.ToDateTime(textBox4.Text);
                    newstring = "SELECT naming as 'Название', COUNT(id_assigned_diagnosis) AS 'Кол-во заб.' FROM Assigned_diagnoses INNER JOIN Diagnoses " +
                        $"ON Assigned_diagnoses.id_diagnosis = Diagnoses.id_diagnosis where start_date <= convert(date, {time.AddYears(1)}) GROUP BY naming";
                    adapter = new SqlDataAdapter(newstring, dataBase.getConnection());
                    ds = new DataSet();
                    adapter.Fill(ds);
                    ds.Tables[0].Columns.Add("%");
                    dataGridView2.DataSource = ds.Tables[0];
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        sum += Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        dataGridView2.Rows[i].Cells[2].Value = (Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value) / sum) * 100;
                }
                else if (comboBox1.Text == "Информация о выполненнх работах за указанный прошедший месяц")
                {

                }
                else if (comboBox1.Text == "Информация о выполненнх работах за указанный прошедший год")
                {

                }
            }
            //catch { }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(null, null);
        }
    }
}
