﻿using System;
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
            user = user1;
        }

    }
}
