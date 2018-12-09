﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tp3InterfaceAnalyse
{
    public partial class MainPannelSGM : Form
    {
        private SignIn previousWindow;
        private CRM crm;

        public MainPannelSGM()
        {
            InitializeComponent();
        }

        public void setPreviousWindow(SignIn window)
        {
            previousWindow = window;
        }
        /*
        public void setCrmGen(CRM crmGen)
        {
            this.crm = crmGen;
        }
        */
        private void MainPannelSGM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.previousWindow != null)
            {
                this.previousWindow.Close();
            }
        }
    }
}