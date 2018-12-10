using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3InterfaceAnalyse
{
    public class Question
    {

        public string Identifiant { get; set; }
        public string Nom { get; set; }
        public string Libelle { get; set; }

        public Question()
        {
        }

        public Question(string pNom, string id, string pLibelle)
        {
            Nom = pNom;
            Libelle = pLibelle;
            Identifiant = id;
        }
    }
}
