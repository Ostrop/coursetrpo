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
    public partial class RegistrationPanel : Form
    {
        checkUser user;
        public RegistrationPanel(checkUser user1)
        {
            InitializeComponent();
            user = user1;
        }
    }
}
