using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProjectTRPO
{
    public partial class AdministrationPanel : Form
    {
        checkUser user;
        public AdministrationPanel(checkUser user1)
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

        private void AdministrationPanel_Activated(object sender, EventArgs e)
        {
            label3.Text = DateTime.Today.ToShortDateString().ToString();
        }
        //Медкарты
        private void button1_Click(object sender, EventArgs e)
        {
            Button obj = (Button)sender;
            TableChange form1 = new TableChange(obj.Text, string.Empty);
            this.Hide();
            form1.ShowDialog();
            this.Show();
        }
    }
}
