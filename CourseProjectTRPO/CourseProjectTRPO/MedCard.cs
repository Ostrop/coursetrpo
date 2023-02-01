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
    public partial class MedCard : Form
    {
        DataBase dataBase = new DataBase();
        SqlDataAdapter adapter;
        DataSet ds;
        string sqlstring = string.Empty;
        int client_id;
        ClientAddServ form;
        public MedCard(int _client_id, bool IsDoctor, RegistrationPanel form)
        {
            client_id = _client_id;
            InitializeComponent();
            //изменение размера окна
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            form.areOpened = true;
            LoadDGW();
            if (IsDoctor)
            {
                button1.Visible = button2.Visible = Changebutton.Visible = Exambutton.Visible = Procedurebutton.Visible = false;
                Client_dates.ReadOnly = button3.Visible = true;
                dgwDiagnoses.ReadOnly = false;
                dgwDiagnoses.AllowUserToAddRows = true;
                button4.Visible = false;
            }
        }

        private void LoadDGW()
        {
            ds = new DataSet();
            sqlstring = "SELECT surname AS 'Фамилия', name AS 'Имя', patronymic AS 'Отчество', passport as 'Паспорт', birth_date as 'Дата рождения'," +
                " height as 'Рост', phone_number as 'Ном. тел.', email as 'Эл. почта', residential_address as 'Место прож.' FROM Medical_cards WHERE id_patient = " + client_id;
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            Client_dates.DataSource = ds.Tables[0];

            ds = new DataSet();
            sqlstring = "SELECT Staff.surname as 'Фам. врача', Staff.name as 'Имя врача', examination_date as 'Дата', examination_time as 'Время'," +
                " symptoms as 'Жалобы', doc_comments " +
                "as 'Комм. врача', appear as 'Явка' FROM Examinations" +
                " INNER JOIN Staff ON Staff.id_staff = Examinations.id_staff WHERE id_patient = " + client_id;
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            dgwExams.DataSource = ds.Tables[0];


            ds = new DataSet();
            sqlstring = "SELECT Staff.surname as 'Фам. врача', Staff.name as 'Имя врача', type_procedure as 'Назн. процедура', procedure_date as 'Дата', procedure_time as 'Время', doctors_report " +
                "as 'Комм. врача', appear as 'Явка' FROM Assigned_procedures INNER JOIN Procedures ON Procedures.id_procedure = Assigned_procedures.id_procedure" +
                " INNER JOIN Staff ON Staff.id_staff = Assigned_procedures.id_staff WHERE id_patient = " + client_id;
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            dgwProcedures.DataSource = ds.Tables[0];

            ds = new DataSet();
            sqlstring = "SELECT id_assigned_diagnosis, naming as 'Диагноз', start_date as 'Дата выявления', finish_date as 'Дата выздоровления', Assigned_diagnoses.doc_comments " +
                "as 'Комм. врача' FROM Assigned_diagnoses INNER JOIN Diagnoses ON Assigned_diagnoses.id_diagnosis = Diagnoses.id_diagnosis" +
                " WHERE id_patient = " + client_id;
            adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
            adapter.Fill(ds);
            ds.Tables[0].Columns.Add("IsNew");
            dgwDiagnoses.DataSource = ds.Tables[0];
            for (int i = 0; i < dgwDiagnoses.RowCount; i++)
                dgwDiagnoses.Rows[i].Cells[dgwDiagnoses.Columns.Count - 1].Value = "MedifiedNew";
            dgwDiagnoses.Columns[0].Visible = false;
            dgwDiagnoses.Columns[dgwDiagnoses.Columns.Count - 1].Visible = false;
        }

        private void Changebutton_Click(object sender, EventArgs e)
        {
            var updateQuery = "update Medical_cards set ";
            updateQuery += $"surname = '{Client_dates.Rows[0].Cells[0].Value.ToString()}', ";
            updateQuery += $"name = '{Client_dates.Rows[0].Cells[1].Value.ToString()}', ";
            updateQuery += $"patronymic = '{Client_dates.Rows[0].Cells[2].Value.ToString()}', ";
            updateQuery += $"passport = '{Client_dates.Rows[0].Cells[3].Value.ToString()}', ";
            updateQuery += $"height = '{Client_dates.Rows[0].Cells[5].Value.ToString()}', ";
            updateQuery += $"phone_number = '{Client_dates.Rows[0].Cells[6].Value.ToString()}', ";
            updateQuery += $"email = '{Client_dates.Rows[0].Cells[7].Value.ToString()}', ";
            updateQuery += $"residential_address = '{Client_dates.Rows[0].Cells[8].Value.ToString()}'";
            updateQuery += $" WHERE id_patient = {client_id}";
            var command = new SqlCommand(updateQuery, dataBase.getConnection());
            dataBase.openConnection();
            command.ExecuteNonQuery();
            dataBase.closedConnection();
        }
        private void Realodbutton_Click(object sender, EventArgs e)
        {
            LoadDGW();
        }
        private void Exambutton_Click(object sender, EventArgs e)
        {
            form = new ClientAddServ(client_id, "Examinations");
            form.ShowDialog();
        }
        private void Procedurebutton_Click(object sender, EventArgs e)
        {
            form = new ClientAddServ(client_id, "Assigned_procedures");
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            int index = dgwExams.CurrentCell.RowIndex;
            var deleteQuery = $"delete from Examinations where id_patient = {client_id} AND examination_date = '{dgwExams.Rows[index].Cells[2].Value}' AND " +
                $"examination_time = '{dgwExams.Rows[index].Cells[3].Value}'";
            var command = new SqlCommand(deleteQuery, dataBase.getConnection());
            command.ExecuteNonQuery();
            dataBase.closedConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            int index = dgwProcedures.CurrentCell.RowIndex;
            var deleteQuery = $"delete from Assigned_procedures where id_patient = {client_id} AND procedure_date = '{dgwProcedures.Rows[index].Cells[3].Value}' AND " +
                $"procedure_time = '{dgwProcedures.Rows[index].Cells[4].Value}'";
            var command = new SqlCommand(deleteQuery, dataBase.getConnection());
            command.ExecuteNonQuery();
            dataBase.closedConnection();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            try
            {
                for (int i = 0; i < dgwDiagnoses.RowCount - 1; i++)
                {
                    if (dgwDiagnoses.Rows[i].Cells[dgwDiagnoses.ColumnCount - 1].Value.ToString() == "Modified" && dgwDiagnoses.Rows[i].Cells[1].Value != string.Empty)
                    {
                        ds = new DataSet();
                        sqlstring = "SELECT * FROM Assigned_diagnoses";
                        adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
                        adapter.Fill(ds);
                        var updateQuery = "INSERT INTO Assigned_diagnoses ( id_assigned_diagnosis, id_diagnosis, id_patient, start_date, finish_date, doc_comments) VALUES (";
                        updateQuery += $"{ds.Tables[0].Rows.Count}, ";
                        sqlstring = $"SELECT id_diagnosis FROM Diagnoses where naming = '{dgwDiagnoses.Rows[i].Cells[1].Value}'";
                        adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
                        ds = new DataSet();
                        adapter.Fill(ds);
                        updateQuery += $"{ds.Tables[0].Rows[0].ItemArray[0]}, {client_id}, ";
                        if (dgwDiagnoses.Rows[i].Cells[2].Value.ToString() == string.Empty)
                            updateQuery += $"NULL, ";
                        else
                            updateQuery += $"'{dgwDiagnoses.Rows[i].Cells[2].Value}', ";
                        if (dgwDiagnoses.Rows[i].Cells[3].Value.ToString() == string.Empty)
                            updateQuery += $"NULL, '{dgwDiagnoses.Rows[i].Cells[4].Value}')";
                        else
                            updateQuery += $"'{dgwDiagnoses.Rows[i].Cells[3].Value}', '{dgwDiagnoses.Rows[i].Cells[4].Value}')";
                        var command = new SqlCommand(updateQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        ds = new DataSet();
                        sqlstring = $"SELECT id_diagnosis FROM Diagnoses where naming = '{dgwDiagnoses.Rows[i].Cells[1].Value}'";
                        adapter = new SqlDataAdapter(sqlstring, dataBase.getConnection());
                        adapter.Fill(ds);
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            var id = Convert.ToInt32(dgwDiagnoses.Rows[i].Cells[0].Value);
                            var updateQuery = "update Assigned_diagnoses set ";

                            updateQuery += $"id_diagnosis = {ds.Tables[0].Rows[0].ItemArray[0]}, id_patient = {client_id}, ";
                            updateQuery += dgwDiagnoses.Rows[i].Cells[2].Value.ToString() == string.Empty ? "start_date = null, " : $"start_date = '{dgwDiagnoses.Rows[i].Cells[2].Value}', ";
                            updateQuery += dgwDiagnoses.Rows[i].Cells[3].Value.ToString() == string.Empty ? "finish_date = null, " : $"finish_date = '{dgwDiagnoses.Rows[i].Cells[3].Value}', ";
                            updateQuery += $"doc_comments = '{dgwDiagnoses.Rows[i].Cells[4].Value}'";
                            updateQuery += $" WHERE id_assigned_diagnosis = {id} ";
                            var command = new SqlCommand(updateQuery, dataBase.getConnection());
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch { }
            dataBase.closedConnection();
        }



        private void dgwDiagnoses_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                var selectedRowIndex = dgwDiagnoses.CurrentCell.RowIndex;
                dgwDiagnoses.Rows[selectedRowIndex].Cells[dgwDiagnoses.ColumnCount - 1].Value = "Modified";
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TableChange form = new TableChange("Полисы", client_id.ToString());
            form.ShowDialog();
        }
    }
}

