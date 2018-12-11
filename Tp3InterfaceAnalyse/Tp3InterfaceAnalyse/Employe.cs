using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3InterfaceAnalyse
{
    public class Employe
    {
        public string id { get; set; }
        public string prenom { get; set; }
        public string nom { get; set; }

        public string adresse { get; set; }

        public Employe(string pId, string pPrenom, string pNom, string pAdresse)
        {
            id = pId;
            prenom = pPrenom;
            nom = pNom;
            adresse = pAdresse;            
        }

        public Employe() { }
    }
}
