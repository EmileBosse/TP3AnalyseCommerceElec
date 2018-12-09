using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.ServiceModel;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk.Query;

namespace Tp3InterfaceAnalyse
{
    public partial class SignIn : Form
    {
        private CRM crm = null;

        public SignIn()
        {
            InitializeComponent();
            crm = new CRM();
        }

        private void btnConnexionSignIn_Click(object sender, EventArgs e)
        {
            /**
             * *******************************************************
             *  HERE WE SHOULD CHANGE THE CODE TO ACCES THE REEL DATA
             * *******************************************************
             **/
            try
            {
                crm.Connexion();
                Guid maybe = crm.getEmployeID(txtNomSignIn.Text, txtPrenomSignIn.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (txtNomSignIn.Text != "" && txtPrenomSignIn.Text != "")
            {
                MainPannelSGM main = new MainPannelSGM();
                main.setPreviousWindow(this);
                main.SetCrmGen(crm);
                main.Show();
                this.Hide();
            }
        }
    }
}
