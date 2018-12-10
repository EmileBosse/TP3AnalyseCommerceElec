﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Tp3InterfaceAnalyse
{
    public partial class MainPannelSGM : Form
    {
        private SignIn previousWindow;
        private CRM crm;
        private Employe employe;
        private List<Etudiant> etudiants;

        public MainPannelSGM()
        {
            InitializeComponent();
            etudiants = new List<Etudiant>();
        }

        public void setPreviousWindow(SignIn window)
        {
            previousWindow = window;
        }

        public void SetCrmGen(CRM crmGen)
        {
            crm = crmGen;
        }

        public void setEmploye(string nom, string prenom, string id)
        {
            employe = new Employe();
            employe.nom = nom;
            employe.prenom = prenom;
            employe.id = id;
        }

        private void MainPannelSGM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.previousWindow != null)
            {
                this.previousWindow.Close();
            }
        }

        private void MainPannelSGM_Load(object sender, EventArgs e)
        {
            this.Text += " ( "+this.employe.nom+", "+this.employe.prenom+" )";
            lbMissionsSGM.DisplayMember = "nom";
            lbMissionsSGM.ValueMember = "identifiant";

            foreach(var item in crm.RetrieveMissions())
            {
                var nom = item.Attributes["new_name"].ToString();
                var id = item.Attributes["new_missionjkweid"].ToString();
                lbMissionsSGM.Items.Add(new ListItem(nom,id));
            }

        }

        private void lbMissionsSGM_SelectedValueChanged(object sender, EventArgs e)
        {

            List<Etudiant> result = new List<Etudiant>();

            if (lbMissionsSGM.SelectedIndex != -1)
            {
                lbMissionsSGM.DisplayMember = "nom";
                lbMissionsSGM.ValueMember = "identifiant";

                lbEtudiantsSGM.Items.Clear();
                foreach (var item in crm.RetrieveEtudiantForMission(((ListItem)lbMissionsSGM.SelectedItem).Value))
                {
                    var id = item.Attributes["new_etudiantjkweid"].ToString();
                    var nom = item.Attributes["new_name"].ToString();
                    var pays = item.Attributes["new_pays"].ToString();
                    var prenom = item.Attributes["new_prenom"].ToString();
                    var adresse = item.Attributes["new_adresse"].ToString();
                    var ville = item.Attributes["new_ville"].ToString();
                    var codepermanent = item.Attributes["new_codepermanent"].ToString();
                    lbEtudiantsSGM.Items.Add(new ListItem(nom, id));
                    result.Add(new Etudiant(nom, id, prenom, adresse, ville, pays, codepermanent));
                }

                gvEtudiant.AutoGenerateColumns = true;
                gvEtudiant.Columns.Clear();
                var bindinList = new BindingList<Etudiant>(result);
                gvEtudiant.DataSource = new BindingSource(bindinList, null);
                gvEtudiant.Columns["Identifiant"].Visible = false;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void lblEtudiantListSGM_Click(object sender, EventArgs e)
        {

        }

        private void gvEtudiant_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void gvEtudiant_SelectionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("on change");
        }

        #region tabEtudiant
        private void lbEtudiantsSGM_SelectedIndexChanged(object sender, EventArgs e)
        {
            //here we need to fill the info box to the right
            MessageBox.Show(lbEtudiantsSGM.SelectedItem.ToString());
            //MessageBox.Show(lbEtudiantsSGM.SelectedValue.ToString());
            foreach (Etudiant et in etudiants)
            {
                if(et.Identifiant == lbEtudiantsSGM.SelectedValue.ToString())
                {
                    txtCodePermanentSGM.Text = et.CodePermanent;
                    txtNomSGM.Text = et.Nom;
                    txtPrenomSGM.Text = et.Prenom;
                    txtAdresseSGM.Text = et.Adresse;
                    txtVilleSGM.Text = et.Ville;
                    txtPaysSGM.Text = et.Pays;
                    //this one should be added in CRM
                    //txtEtatSGM.Text = et.Etat;
                }
            }
        }

        #endregion

        private void tabRecherche_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(tabRecherche.SelectedTab.Name.ToString());
            if (tabRecherche.SelectedTab.Name.ToString() == "tabEtudiant")
            {
                foreach (var item in crm.RetrieveEtudiants())
                {
                    var id = item.Attributes["new_etudiantjkweid"].ToString();
                    var nom = item.Attributes["new_name"].ToString();
                    var pays = item.Attributes["new_pays"].ToString();
                    var prenom = item.Attributes["new_prenom"].ToString();
                    var adresse = item.Attributes["new_adresse"].ToString();
                    var ville = item.Attributes["new_ville"].ToString();
                    var codepermanent = item.Attributes["new_codepermanent"].ToString();
                    lbEtudiantsSGM.Items.Add(new ListItem(nom, id));
                    MessageBox.Show(nom + ", " + id);
                    etudiants.Add(new Etudiant(nom, id, prenom, adresse, ville, pays, codepermanent));
                }
            }
        }
    }
}
