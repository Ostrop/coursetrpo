using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CourseProjectTRPO;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestProject1
    {
        [TestMethod]
        public void DbConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=DIKHNICHHONOR\SQLEXPRESS;Initial Catalog=CourseProjectTRPO1;Integrated Security=True");
            sqlConnection.Open();

            bool areOpened = false, expectedResult = true;
            if (sqlConnection.State == ConnectionState.Open)
            { areOpened = true; };

            Assert.AreEqual(areOpened, expectedResult);
            sqlConnection.Close();
        }

        [TestMethod]
        public void HashPass()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=DIKHNICHHONOR\SQLEXPRESS;Initial Catalog=CourseProjectTRPO1;Integrated Security=True");
            sqlConnection.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            string loginAdmin = "adm17", passwordAdmin = md5.hashPassword("admn173");
            string expectedResult = $"{loginAdmin} {passwordAdmin}";
            string querystr = $"SELECT login, password FROM Staff " +
                              $"WHERE id_staff = 0";

            SqlCommand command = new SqlCommand(querystr, sqlConnection);
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);

            sqlConnection.Close();

            string stroke = $"{dataTable.Rows[0][0].ToString()} {dataTable.Rows[0][1].ToString()}";

            Assert.AreEqual(stroke, expectedResult);

        }

        [TestMethod]
        public void AdminAuthorization()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=DIKHNICHHONOR\SQLEXPRESS;Initial Catalog=CourseProjectTRPO1;Integrated Security=True");
            sqlConnection.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            string loginAdmin = "adm17", passwordAdmin = md5.hashPassword("admn173");

            string querystr = $"SELECT surname, name, patronymic FROM Staff " +
                              $"WHERE login = '{loginAdmin}' AND password = '{passwordAdmin}'";

            SqlCommand command = new SqlCommand(querystr, sqlConnection);
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);

            sqlConnection.Close();

            string stroke = $"{dataTable.Rows[0][0].ToString()} {dataTable.Rows[0][1].ToString()} {dataTable.Rows[0][2].ToString()}";

            Assert.AreEqual(stroke, "Дихнич Олег Анатольевич");

        }

        [TestMethod]
        public void AdminForm()
        {
            string loginAdmin = "adm17", passwordAdmin = "admn173";
            bool expectedResult = true;
            Authorization form = new Authorization();
            form.textBox1.Text = loginAdmin;
            form.textBox2.Text = passwordAdmin;
            form.button1_Click(this, new EventArgs());

            Assert.AreEqual(expectedResult, form.areOpened);

        }

        [TestMethod]
        public void MedicalCardOpen()
        {
            bool expectedResult = true;
            RegistrationPanel form = new RegistrationPanel(new checkUser());
            DataGridViewCellEventArgs dg = new DataGridViewCellEventArgs(0, 0);
            form.dataGridView1_CellClick(this, dg);

            Assert.AreEqual(expectedResult, form.areOpened);

        }

    }
}