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
                //Here we should getBy(name, lastname)
                //but it's easyer to get a list :')
                //Guid maybe = crm.getEmployeID(txtNomSignIn.Text, txtPrenomSignIn.Text);
                bool trouve = false;
                foreach (var item in crm.RetrieveEmployes())
                {
                    Console.WriteLine("  -> "+ item.Attributes["new_name"].ToString()+", "+ item.Attributes["new_prenom"].ToString());
                    if (txtNomSignIn.Text == item.Attributes["new_name"].ToString() && txtPrenomSignIn.Text == item.Attributes["new_prenom"].ToString())
                    {
                        trouve = true;
                        Employe employe = new Employe();
                        employe.nom = txtNomSignIn.Text;
                        employe.prenom = txtPrenomSignIn.Text;
                        employe.id = (Guid)item.Attributes["new_employeuniversietjkweid"];
                        MainPannelSGM main = new MainPannelSGM();
                        main.setPreviousWindow(this);
                        main.SetCrmGen(crm);
                        main.setEmploye(employe.nom, employe.prenom, employe.id);
                        main.Show();
                        this.Hide();
                    }
                }
                if(!trouve) MessageBox.Show("Le nom que vous avez entré n'est pas un nom d'employé valide.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void btnAnnulerSignIn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
