using System;
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
            List<Question> result = new List<Question>();

            var rowsCount = gvEtudiant.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;

            var row = gvEtudiant.SelectedRows[0];
            if (row == null) return;

            // Get questions de l'étudiant
            var idEtudiant = gvEtudiant.CurrentRow.Cells["identifiant"].Value.ToString();
            foreach(var item in crm.RetrieveQuestionForEtudiant(idEtudiant))
            {
                var nom = item.Attributes["new_name"].ToString();
                var id = item.Attributes["new_questionjkweid"].ToString();
                var libelle = item.Attributes["new_libelle"].ToString();
                result.Add(new Question(nom, id, libelle));
            }

            gvQuestions.AutoGenerateColumns = true;
            gvQuestions.Columns.Clear();
            var bindingList = new BindingList<Question>(result);
            gvQuestions.DataSource = new BindingSource(bindingList, null);
                
            
        }

        #region tabEtudiant
        private enum Btn1State { ajouter, confirmerAjout, confirmerModif};
        private Btn1State btn1State = Btn1State.ajouter;
        private enum Btn2State { modifier, annulerAjout, annulerModif };
        private Btn2State btn2State = Btn2State.modifier;

        private void lbEtudiantsSGM_SelectedIndexChanged(object sender, EventArgs e)
        {
            //here we need to fill the info box to the right
            clearEtudiantFields(false);
        }

        private void enableEtudiantFields(bool enable)
        {
            txtCodePermanentSGM.Enabled = enable;
            txtNomSGM.Enabled = enable;
            txtPrenomSGM.Enabled = enable;
            txtAdresseSGM.Enabled = enable;
            txtVilleSGM.Enabled = enable;
            txtPaysSGM.Enabled = enable;
            txtEtatSGM.Enabled = enable;

            lbEtudiantsSGM.Enabled = !enable;
        }

        private void clearEtudiantFields(bool clear)
        {
            if (clear)
            {
                txtCodePermanentSGM.Text = "";
                txtNomSGM.Text = "";
                txtPrenomSGM.Text = "";
                txtAdresseSGM.Text = "";
                txtVilleSGM.Text = "";
                txtPaysSGM.Text = "";
                txtEtatSGM.Text = "";
            }
            else
            {
                foreach (Etudiant et in etudiants)
                {
                    if (et.Nom + ", " + et.Prenom == lbEtudiantsSGM.SelectedItem.ToString())
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
        }

        private void btnAction1EtudiantSGM_Click(object sender, EventArgs e)
        {
            switch (btn1State)
            {
                case Btn1State.ajouter:
                    //clear all the fields
                    enableEtudiantFields(true);
                    clearEtudiantFields(true);
                    //switch btn1 to confirmerAjout
                    btn1State = Btn1State.confirmerAjout;
                    btnAction1EtudiantSGM.Text = "Confirmer";
                    //switch btn2 to annulerAjout
                    btn2State = Btn2State.annulerAjout;
                    btnAction2EtudiantSGM.Text = "Annuler";
                    break;
                case Btn1State.confirmerAjout:
                    //throw the creation action to CRM

                    onloadEtudiantTab();
                    enableEtudiantFields(false);
                    //switch btn1 to ajouter
                    btn1State = Btn1State.ajouter;
                    btnAction1EtudiantSGM.Text = "Ajouter";
                    //switch btn2 to modifier
                    btn2State = Btn2State.modifier;
                    btnAction2EtudiantSGM.Text = "Modifier";
                    break;
                case Btn1State.confirmerModif:
                    //throw the modification action to CRM

                    onloadEtudiantTab();
                    enableEtudiantFields(false);
                    //switch btn1 to ajouter
                    btn1State = Btn1State.ajouter;
                    btnAction1EtudiantSGM.Text = "Ajouter";
                    //switch btn2 to modifier
                    btn2State = Btn2State.modifier;
                    btnAction1EtudiantSGM.Text = "Modifier";
                    break;
            }
        }

        private void btnAction2EtudiantSGM_Click(object sender, EventArgs e)
        {
            switch (btn2State)
            {
                case Btn2State.modifier:
                    //Enable the fields
                    enableEtudiantFields(true);
                    //switch btn2 to annulerModif
                    btn2State = Btn2State.annulerModif;
                    btnAction2EtudiantSGM.Text = "Annuler";
                    //switch btn1 to confirmerModif
                    btn1State = Btn1State.confirmerModif;
                    btnAction1EtudiantSGM.Text = "Confirmer";
                    break;
                case Btn2State.annulerAjout:
                    //messagebox of validation
                    DialogResult dialogResult1 = MessageBox.Show("Êtes-vous certain de vouloir annuler l'ajout?", "SGM", MessageBoxButtons.YesNo);
                    //if yes fill the field with the initial state
                    if (dialogResult1 == DialogResult.Yes)
                    {
                        enableEtudiantFields(false);
                        clearEtudiantFields(false);
                        //switch btn2 to modifier
                        btn2State = Btn2State.modifier;
                        btnAction2EtudiantSGM.Text = "Modifier";
                        //switch btn1 to ajouter
                        btn1State = Btn1State.ajouter;
                        btnAction1EtudiantSGM.Text = "Ajouter";
                    }
                    //else do nothing
                    break;
                case Btn2State.annulerModif:
                    //messagebox of validation
                    DialogResult dialogResult2 = MessageBox.Show("Êtes-vous certain de vouloir annuler les modifications?", "SGM", MessageBoxButtons.YesNo);
                    //if yes fill the field with the initial state
                    if (dialogResult2 == DialogResult.Yes)
                    {
                        enableEtudiantFields(false);
                        clearEtudiantFields(false);
                        //switch btn2 to modifier
                        btn2State = Btn2State.modifier;
                        btnAction2EtudiantSGM.Text = "Modifier";
                        //switch btn1 to ajouter
                        btn1State = Btn1State.ajouter;
                        btnAction1EtudiantSGM.Text = "Ajouter";
                    }
                    //else do nothing
                    break;
            }
        }

        private void onloadEtudiantTab()
        {
            lbEtudiantsSGM.Items.Clear();
            foreach (var item in crm.RetrieveEtudiants())
            {
                var id = item.Attributes["new_etudiantjkweid"].ToString();
                var nom = item.Attributes["new_name"].ToString();
                var pays = item.Attributes["new_pays"].ToString();
                var prenom = item.Attributes["new_prenom"].ToString();
                var adresse = item.Attributes["new_adresse"].ToString();
                var ville = item.Attributes["new_ville"].ToString();
                var codepermanent = item.Attributes["new_codepermanent"].ToString();
                lbEtudiantsSGM.Items.Add(new ListItem(nom + ", " + prenom, id));
                etudiants.Add(new Etudiant(nom, id, prenom, adresse, ville, pays, codepermanent));
            }
        }

        #endregion

        private void tabRecherche_SelectedIndexChanged(object sender, EventArgs e)
        {
            //you can add your tab change here if you want to do an onload thing
            if (tabRecherche.SelectedTab.Name.ToString() == "tabEtudiant")
            {
                onloadEtudiantTab();
            }
        }

        private void btnPaysOriginSGM_Click(object sender, EventArgs e)
        {
            // Trier selon le pays en ordre alphabetique
        }

        private void btnTrieCycleEtudeSGM_Click(object sender, EventArgs e)
        {
            // Trier selon le cycle d'etude en ordre alphabetique
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Trier selon le programme d'etude en ordre alphabetique
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Trier selon l'etablissement d'origine en ordre alphabetique
        }

        private void gvQuestions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblQuestions_Click(object sender, EventArgs e)
        {

        }
    }
}
