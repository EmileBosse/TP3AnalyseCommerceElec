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

        public MainPannelSGM()
        {
            InitializeComponent();
        }

        public void setPreviousWindow(SignIn window)
        {
            previousWindow = window;
        }


        public void SetCrmGen(CRM crmGen)
        {
            crm = crmGen;
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
                    var nom = item.Attributes["new_name"].ToString();
                    var id = item.Attributes["new_etudiantjkweid"].ToString();
                    lbEtudiantsSGM.Items.Add(new ListItem(nom, id));
                    result.Add(new Etudiant(nom, id));
                }

                gvEtudiant.AutoGenerateColumns = true;
                var source = new BindingSource();
                source.DataSource = result;
                gvEtudiant.DataSource = source;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void lbEtudiantsSGM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblEtudiantListSGM_Click(object sender, EventArgs e)
        {

        }

        private void gvEtudiant_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
