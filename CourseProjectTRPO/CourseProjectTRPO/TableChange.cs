using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CourseProjectTRPO
{
    public partial class TableChange : Form
    {
        enum RowState
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }
        SqlDataAdapter adapter;
        DataSet ds;
        string sqlstring;
        List<TextBox> listtextbox = new List<TextBox>();
        List<Label> listlabel = new List<Label>();
        DataBase dataBase = new DataBase();
        int selectedRow;

        string tablename;
        //Конструктор формы
        public TableChange(string _tablename)
        {
            InitializeComponent();
            //изменение размера окна
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            groupBox3.Visible = false;
            dataGridView1.ReadOnly = true;
            button6.Enabled = false;
            label3.Text = _tablename;
            switch (_tablename)
            {
                case "Медицинские карты": tablename = "Medical_cards"; break;
                case "Полисы": tablename = "Policies"; break;
                case "Осмотры": tablename = "Examinations"; break;
                case "Процедуры": tablename = "Procedures"; break;
                case "Назначенные процедуры": tablename = "Assigned_procedures"; break;
                case "Диагнозы": tablename = "Diagnoses"; break;
                case "Поставленные диагнозы": tablename = "Assigned_diagnoses"; break;
                case "Персонал": tablename = "Staff"; break;
                case "Зарплата": tablename = "Salary"; break;
                case "Документы об образовании": tablename = "Documents_on_education"; break;
                case "Должности": tablename = "Posts"; break;
                case "Расписание": tablename = "Timetable"; break;
            }

            adapter = new SqlDataAdapter("SELECT * FROM " + tablename, dataBase.getConnection());

            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            ds.Tables[0].Columns.Add("IsNew");
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = RowState.ModifiedNew;

            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = false;
            GetCollections();
        }

        private void TableChange_Activated(object sender, EventArgs e)
        {
            label1.Text = DateTime.Today.ToShortDateString().ToString();
        }
        //создание текстбоксов и лейблов
        private void GetCollections()
        {
            Label label;
            TextBox textbox;
            switch (label3.Text)
            {
                case "Расписание":
                case "Полисы":
                case "Зарплата":
                case "Документы об образовании":
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        if (i != dataGridView1.ColumnCount - 1)
                        {
                            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
                            label = new Label();
                            label.Text = dataGridView1.Columns[i].HeaderCell.Value.ToString();
                            label.Size = new Size(255, 23);
                            flowLayoutPanel1.Controls.Add(label);
                            listlabel.Add(label);
                            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
                            textbox = new TextBox();
                            textbox.Size = new Size(255, 32);
                            textbox.TextChanged += TextChanged;
                            if (ds.Tables[0].Columns[i].DataType.ToString() == "System.Int32" || ds.Tables[0].Columns[i].DataType.ToString() == "System.Double")
                                textbox.KeyPress += textBox_KeyPress;
                            flowLayoutPanel1.Controls.Add(textbox);
                            listtextbox.Add(textbox);
                        }
                    }
                    break;
                default:
                    for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                    {
                        if (i != dataGridView1.ColumnCount - 2)
                        {
                            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
                            label = new Label();
                            label.Text = dataGridView1.Columns[i + 1].HeaderCell.Value.ToString();
                            label.Size = new Size(255, 23);
                            flowLayoutPanel1.Controls.Add(label);
                            listlabel.Add(label);
                            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
                            textbox = new TextBox();
                            textbox.Size = new Size(255, 32);
                            textbox.TextChanged += TextChanged;
                            if (ds.Tables[0].Columns[i + 1].DataType.ToString() == "System.Int32" || ds.Tables[0].Columns[i + 1].DataType.ToString() == "System.Double")
                                textbox.KeyPress += textBox_KeyPress;

                            flowLayoutPanel1.Controls.Add(textbox);
                            listtextbox.Add(textbox);
                        }
                    }
                    break;
            }
        }
        //обработчик числовых полей
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!double.TryParse(textBox.Text + e.KeyChar.ToString(), out double a) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        //обработчик активности кнопки подтвердить
        private void TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < listtextbox.Count; i++)
                {
                    if (listlabel[i].Text == "password" || listlabel[i].Text == "login")
                        break;
                    else if (listtextbox[i].Enabled == false && listtextbox[i].Text == string.Empty)
                    {
                        button6.Enabled = false;
                        break;
                    }
                    else if (listtextbox[i].Text == "")
                    {
                        button6.Enabled = false;
                        break;
                    }
                    else
                        button6.Enabled = true;
                }
            }
            catch { }
        }
        //кнопка обновить
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            adapter = new SqlDataAdapter("SELECT * FROM " + tablename, dataBase.getConnection());
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            ds.Tables[0].Columns.Add("IsNew");
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = RowState.ModifiedNew;

            foreach (var item in listtextbox)
                item.Text = string.Empty;
            selectedRow = 0;
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = false;
        }
        //кнопка удалить
        private void button3_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            foreach (var item in listtextbox)
            {
                item.Text = string.Empty;
            }
            dataGridView1.CurrentCell = null;
            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].ToString() == String.Empty)
            {
                dataGridView1.Rows[index].Cells[dataGridView1.ColumnCount - 1].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[dataGridView1.ColumnCount - 1].Value = RowState.Deleted;


        }

        private void Update()
        {
            dataBase.openConnection();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                var rowState = dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 1].Value.ToString();

                if (rowState == RowState.Existed.ToString())
                    continue;
                if (rowState == RowState.Deleted.ToString() && label3.Text == "Зарплата")
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var month = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                    var year = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                    var deleteQuery = "delete from " + tablename + " where " + dataGridView1.Columns[0].HeaderCell.Value.ToString() + " = " + id;
                    deleteQuery += $" AND {dataGridView1.Columns[1].HeaderCell.Value.ToString()} = {month} AND {dataGridView1.Columns[2].HeaderCell.Value.ToString()} = {year}";
                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
                else if (rowState == RowState.Deleted.ToString() && label3.Text == "Расписание")
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var date = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                    var deleteQuery = "delete from " + tablename + " where " + dataGridView1.Columns[0].HeaderCell.Value.ToString() + " = " + id;
                    deleteQuery += $" AND {dataGridView1.Columns[1].HeaderCell.Value.ToString()} = '{date}'";
                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
                else if (rowState == RowState.Deleted.ToString())
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var deleteQuery = "delete from " + tablename + " where " + dataGridView1.Columns[0].HeaderCell.Value.ToString() + " = " + id;
                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState.Modified.ToString() && label3.Text == "Зарплата")
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var month = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                    var year = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                    var updateQuery = "update " + tablename + " set ";
                    updateQuery += $"{dataGridView1.Columns[1].HeaderCell.Value.ToString()} = {month}, {dataGridView1.Columns[2].HeaderCell.Value.ToString()} = {year}";
                    updateQuery += $" WHERE {dataGridView1.Columns[0].HeaderCell.Value.ToString()} = {id} ";
                    var command = new SqlCommand(updateQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
                else if (rowState == RowState.Modified.ToString() && label3.Text == "Расписание")
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var date = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                    var updateQuery = "update " + tablename + " set ";
                    updateQuery += $" {dataGridView1.Columns[1].HeaderCell.Value.ToString()} = '{date}'";
                    updateQuery += $" WHERE {dataGridView1.Columns[0].HeaderCell.Value.ToString()} = {id} AND {dataGridView1.Columns[1].HeaderCell.Value.ToString()} = '{date}'";
                    var command = new SqlCommand(updateQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
                else if (rowState == RowState.Modified.ToString() && label3.Text == "Персонал")
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var updateQuery = "update " + tablename + " set ";
                    for (int q = 0; q < listtextbox.Count; q++)
                    {
                        if (listlabel[q].Text == "password")
                            updateQuery += $"{dataGridView1.Columns[q + 1].HeaderCell.Value.ToString()} = '{md5.hashPassword(dataGridView1.Rows[i].Cells[q + 1].Value.ToString())}', ";
                        else
                            updateQuery += $"{dataGridView1.Columns[q + 1].HeaderCell.Value.ToString()} = '{dataGridView1.Rows[i].Cells[q + 1].Value.ToString()}', ";
                    }
                    updateQuery = updateQuery.Remove(updateQuery.Length - 2);
                    updateQuery += $" WHERE {dataGridView1.Columns[0].HeaderCell.Value.ToString()} = {id}";
                    var command = new SqlCommand(updateQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
                else if (rowState == RowState.Modified.ToString())
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var updateQuery = "update " + tablename + " set ";
                    for (int q = 0; q < listtextbox.Count; q++)
                    {
                        updateQuery += $"{dataGridView1.Columns[q + 1].HeaderCell.Value.ToString()} = '{dataGridView1.Rows[i].Cells[q + 1].Value.ToString()}', ";
                    }
                    updateQuery = updateQuery.Remove(updateQuery.Length - 2);
                    updateQuery += $" WHERE {dataGridView1.Columns[0].HeaderCell.Value.ToString()} = {id}";
                    var command = new SqlCommand(updateQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closedConnection();
        }

        //кнопка сохранить
        private void button5_Click(object sender, EventArgs e)
        {
            Update();
        }
        //выбор ячейки
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                switch (label3.Text)
                {
                    case "Расписание":
                        if (e.RowIndex >= 0)
                        {
                            for (int i = 0; i < listtextbox.Count; i++)
                                listtextbox[i].Text = row.Cells[i].Value.ToString();
                        }

                        adapter = new SqlDataAdapter();
                        SqlCommand command = new SqlCommand($"SELECT Staff.id_staff FROM Staff INNER JOIN Posts ON Staff.id_post = Posts.id_post WHERE surname = '{listtextbox[1].Text}' AND naming = '{listtextbox[0].Text}'", dataBase.getConnection());
                        DataTable table = new DataTable();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        listtextbox[2].Text = table.Rows[0].ItemArray[0].ToString();
                        break;
                    case "Полисы":
                    case "Зарплата":
                    case "Документы об образовании":
                        if (e.RowIndex >= 0)
                        {
                            for (int i = 0; i < listtextbox.Count; i++)
                                listtextbox[i].Text = row.Cells[i].Value.ToString();
                        }
                        break;
                    default:
                        if (e.RowIndex >= 0)
                        {
                            for (int i = 0; i < listtextbox.Count; i++)
                            {
                                listtextbox[i].Text = row.Cells[i + 1].Value.ToString();
                            }
                        }
                        break;
                }
            }
            catch { }

        }
        //кнопка новая запись
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox2.Visible = false;
            groupBox1.Visible = false;
            button1_Click(sender, e);
            label2.Text = "Создание новой записи";
            switch (label3.Text)
            {
                case "Медицинские карты":
                    sqlstring = "INSERT INTO " + tablename + "(id_patient, surname, [name], patronymic, passport, birth_date, height, phone_number, email, residential_address) VALUES (";
                    break;
                case "Полисы":
                    break;
                case "Осмотры":
                    break;
                case "Процедуры":
                    sqlstring = "INSERT INTO [" + tablename + "](id_procedure, type_procedure, price ) VALUES (";
                    break;
                case "Назначенные процедуры":
                    break;
                case "Диагнозы":
                    sqlstring = "INSERT INTO " + tablename + "(id_diagnosis, naming) VALUES (";
                    break;
                case "Поставленные диагнозы":
                    break;
                case "Персонал":
                    sqlstring = "INSERT INTO " + tablename + "(id_staff, id_post, surname, [name], patronymic, passport, birth_date, phone_number, email, residential_address) VALUES (";
                    break;
                case "Зарплата":
                    sqlstring = "INSERT INTO " + tablename + "(id_staff, [month], [year], salary, award) VALUES (";
                    break;
                case "Документы об образовании":
                    sqlstring = "INSERT INTO " + tablename + "(document_number, id_staff, issue_date, organization) VALUES (";
                    break;
                case "Должности":
                    sqlstring = "INSERT INTO " + tablename + "(id_post, naming) VALUES (";
                    break;
                case "Расписание":
                    sqlstring = "INSERT INTO " + tablename + "(id_staff, date) VALUES (";
                    break;
            }
        }
        //кнопка отменить
        private void button7_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            label2.Text = "Запись";
            button1_Click(sender, e);
        }
        //Подтвердить создание новой записи
        private void button6_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            switch (label3.Text)
            {
                case "Расписание":
                case "Полисы":
                case "Зарплата":
                case "Документы об образовании":
                    for (int i = 0; i < listtextbox.Count; i++)
                    {
                        if (ds.Tables[0].Columns[i].DataType.ToString() == "System.Int32" || ds.Tables[0].Columns[i + 1].DataType.ToString() == "System.Double")
                            sqlstring += listtextbox[i].Text + ", ";
                        else
                            sqlstring += "'" + listtextbox[i].Text + "', ";
                    }
                    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                    sqlstring += ");";
                    break;
                case "Персонал":
                    sqlstring += dataGridView1.RowCount + 1 + ", ";
                    for (int i = 0; i < listtextbox.Count; i++)
                    {
                        if (i > listtextbox.Count - 1 && (ds.Tables[0].Columns[i].DataType.ToString() == "System.Int32" || ds.Tables[0].Columns[i + 1].DataType.ToString() == "System.Double"))
                            sqlstring += listtextbox[i].Text + ", ";
                        else if (i > listtextbox.Count - 1)
                            sqlstring += "'" + listtextbox[i].Text + "', ";
                        else
                            sqlstring += "'" + md5.hashPassword(listtextbox[i].Text) + "', ";
                    }
                    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                    sqlstring += ");";
                    break;
                default:
                    sqlstring += dataGridView1.RowCount;
                    for (int i = 0; i < listtextbox.Count; i++)
                    {
                        if (ds.Tables[0].Columns[i + 1].DataType.ToString() == "System.Int32" || ds.Tables[0].Columns[i + 1].DataType.ToString() == "System.Double")
                            sqlstring += ", " + listtextbox[i].Text;
                        else
                            sqlstring += ", '" + listtextbox[i].Text + "'";
                    }
                    sqlstring += ");";
                    break;
            }
            var command = new SqlCommand(sqlstring, dataBase.getConnection());
            command.ExecuteNonQuery();
            dataBase.closedConnection();
            groupBox3.Visible = false;
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            label2.Text = "Запись";
            button1_Click(sender, e);
        }
        //кнопка изменить
        private void button4_Click(object sender, EventArgs e)
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            switch (label3.Text)
            {
                case "Расписание":
                case "Полисы":
                case "Зарплата":
                case "Документы об образовании":
                    for (int i = 0; i < listtextbox.Count; i++)
                        dataGridView1.Rows[selectedRowIndex].Cells[i].Value = listtextbox[i].Text;
                    dataGridView1.Rows[selectedRowIndex].Cells[dataGridView1.ColumnCount - 1].Value = RowState.Modified;
                    break;
                default:
                    for (int i = 0; i < listtextbox.Count; i++)
                        dataGridView1.Rows[selectedRowIndex].Cells[i + 1].Value = listtextbox[i].Text;
                    dataGridView1.Rows[selectedRowIndex].Cells[dataGridView1.ColumnCount - 1].Value = RowState.Modified;
                    break;
            }
        }
    }
}

