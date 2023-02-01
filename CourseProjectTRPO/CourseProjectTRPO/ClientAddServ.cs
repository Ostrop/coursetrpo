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
    public partial class ClientAddServ : Form
    {
        int client_id;
        List<TextBox> listtextbox = new List<TextBox>();
        List<Label> listlabel = new List<Label>();
        DataBase dataBase = new DataBase();
        SqlDataAdapter adapter;
        string sqlstring;
        DataSet ds;
        string tablename;
        public ClientAddServ(int _client_id, string _tablename)
        {
            InitializeComponent();
            //изменение размера окна
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            label3.Text = DateTime.Today.ToShortDateString().ToString();

            Label label;
            TextBox textbox;
            tablename = _tablename;
            client_id = _client_id;
            if (tablename == "Examinations")
            {
                label5.Visible = false;
                dataGridView3.Visible = false;
                textBox2.Visible = label6.Visible = false;
            }
            adapter = new SqlDataAdapter("SELECT Staff.id_staff, naming, surname, [name], patronymic, [date] FROM Staff INNER JOIN Posts   " +
                "ON Staff.id_post = Posts.id_post JOIN Timetable ON Staff.id_staff = Timetable.id_staff WHERE Timetable.[date] " +
                ">= convert(date, getdate()) AND naming != 'Системный администратор' AND naming != 'Регистратор' AND naming != 'Главный врач' AND naming != 'Медсестра (медбрат)'", dataBase.getConnection());

            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];


            adapter = new SqlDataAdapter("SELECT id_procedure as 'ID', type_procedure as 'Название', price as 'Цена' FROM Procedures", dataBase.getConnection());
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];

            adapter = new SqlDataAdapter("SELECT * FROM " + tablename, dataBase.getConnection());

            ds = new DataSet();
            adapter.Fill(ds);


            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            for (int i = 0; i < ds.Tables[0].Columns.Count - 4; i++)
            {
                label = new Label();
                label.Text = ds.Tables[0].Columns[i + 2].ColumnName.ToString();
                label.Size = new Size(255, 23);
                flowLayoutPanel1.Controls.Add(label);
                listlabel.Add(label);
                textbox = new TextBox();
                textbox.Size = new Size(255, 32);
                textbox.TextChanged += TextChanged;
                flowLayoutPanel1.Controls.Add(textbox);
                listtextbox.Add(textbox);
            }
        }
        //обработчик активности кнопки подтвердить
        private void TextChanged(object sender, EventArgs e)
        {
            try
                            {
                for (int i = 0; i < listtextbox.Count; i++)
                {
                    if (tablename == "Examinations")
                    {
                        if (listlabel[i].Text == "id_staff" || listlabel[i].Text == "examination_date")
                        {
                            adapter = new SqlDataAdapter($"SELECT examination_time as 'Время' FROM Examinations where id_staff = {listtextbox[0].Text} " +
                                    $"AND examination_date = '{listtextbox[1].Text}' UNION SELECT procedure_time as 'Время' FROM [Assigned_procedures] where id_staff = {listtextbox[0].Text} AND " +
                                    $"procedure_date = '{listtextbox[1].Text}'", dataBase.getConnection());

                            ds = new DataSet();
                            adapter.Fill(ds);
                            dataGridView2.DataSource = ds.Tables[0];
                        }
                        else
                            dataGridView2.Rows.Clear();
                        if (listtextbox[i].Text == "")
                        {
                            button1.Enabled = false;
                            break;
                        }
                        else
                            button1.Enabled = true;
                    }
                    else
                    {
                        if (listlabel[i].Text == "id_staff" || listlabel[i].Text == "procedure_date")
                        {
                            adapter = new SqlDataAdapter($"SELECT examination_time as 'Время' FROM Examinations where id_staff = {listtextbox[0].Text} " +
                                    $"AND examination_date = '{listtextbox[2].Text}' UNION SELECT procedure_time as 'Время' FROM [Assigned_procedures] where id_staff = {listtextbox[0].Text} AND " +
                                    $"procedure_date = '{listtextbox[2].Text}'", dataBase.getConnection());

                            ds = new DataSet();
                            adapter.Fill(ds);
                            dataGridView2.DataSource = ds.Tables[0];
                        }
                        else
                            dataGridView2.Rows.Clear();
                        if (listtextbox[i].Text == "")
                        {
                            button1.Enabled = false;
                            break;
                        }
                        else
                            button1.Enabled = true;
                    }

                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            adapter = new SqlDataAdapter($"SELECT * FROM {tablename}", dataBase.getConnection());
            ds = new DataSet();
            adapter.Fill(ds);

            sqlstring = $"INSERT INTO {tablename} (";
            for (int i = 0; i < ds.Tables[0].Columns.Count - 2; i++)
                sqlstring += $"{ds.Tables[0].Columns[i].ColumnName.ToString()}, ";

            sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            sqlstring += $") VALUES ({ds.Tables[0].Rows.Count}, {client_id}, ";
            for (int i = 0; i < listtextbox.Count; i++)
                sqlstring += "'" + listtextbox[i].Text + "', ";

            sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            sqlstring += ");";
            var command = new SqlCommand(sqlstring, dataBase.getConnection());
            dataBase.openConnection();
            command.ExecuteNonQuery();
            dataBase.closedConnection();
            this.Close();
        }

        //Метод поиска
        private void Search(DataGridView dgw)
        {
            try
            {
                string searchString = $"SELECT Staff.id_staff, naming, surname, [name], patronymic, [date] FROM Staff INNER JOIN Posts   " +
                "ON Staff.id_post = Posts.id_post JOIN Timetable ON Staff.id_staff = Timetable.id_staff WHERE Timetable.[date] " +
                ">= convert(date, getdate()) AND naming != 'Системный администратор' AND naming != 'Регистратор' AND naming != 'Главный врач' AND naming != 'Медсестра (медбрат)' " +
                $"AND concat(Staff.id_staff, naming, surname, [name], patronymic, [date]) like '%{textBox1.Text}%'";

                adapter = new SqlDataAdapter(searchString, dataBase.getConnection());
                ds = new DataSet();
                adapter.Fill(ds);
                dgw.DataSource = ds.Tables[0];
            }
            catch
            {
                adapter = new SqlDataAdapter("SELECT Staff.id_staff, naming, surname, [name], patronymic, [date] FROM Staff INNER JOIN Posts   " +
                    "ON Staff.id_post = Posts.id_post JOIN Timetable ON Staff.id_staff = Timetable.id_staff WHERE Timetable.[date] " +
                    ">= convert(date, getdate()) AND naming != 'Системный администратор' AND naming != 'Регистратор' AND naming != 'Главный врач' AND naming != 'Медсестра (медбрат)'", dataBase.getConnection());

                ds = new DataSet();
                adapter.Fill(ds);
                dgw.DataSource = ds.Tables[0];
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                adapter = new SqlDataAdapter("SELECT id_procedure as 'ID', type_procedure as 'Название', price as 'Цена' FROM Procedures " +
                   $"WHERE type_procedure like '%{textBox2.Text}%'", dataBase.getConnection());
                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView3.DataSource = ds.Tables[0];
            }
            catch
            {
                adapter = new SqlDataAdapter("SELECT id_procedure as 'ID', type_procedure as 'Название', price as 'Цена' FROM Procedures", dataBase.getConnection());
                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView3.DataSource = ds.Tables[0];
            }
        }
    }
}
