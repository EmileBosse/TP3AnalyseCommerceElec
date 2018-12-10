using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3InterfaceAnalyse
{
    public class Mission
    {
        private string identifiant;
        private string nom;
        private string pays;
        private DateTime dateDebut;
        private DateTime dateFin;

        public Mission()
        {
        }

        public Mission(string pNom, string id)
        {
            nom = pNom;
            identifiant = id;
        }

    }
}
