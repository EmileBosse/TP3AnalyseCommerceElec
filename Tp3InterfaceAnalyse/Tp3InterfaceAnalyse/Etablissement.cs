using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3InterfaceAnalyse
{
    public class Etablissement
    {
        public string Identifiant { get; set; }
        public string Nom { get; set; }

        public Etablissement()
        {
        }

        public Etablissement(string pNom, string id)
        {
            Nom = pNom;
            Identifiant = id;
        }
    }
}
