using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3InterfaceAnalyse
{
    public class Etablissement
    {
        public Guid Identifiant { get; set; }
        public string Nom { get; set; }
        public string Pays { get; set; }
        public string Ville { get; set; }

        public Etablissement()
        {
        }

        public Etablissement(string pNom, Guid id, string pPays, string pVille)
        {
            Nom = pNom;
            Identifiant = id;
            Pays = pPays;
            Ville = pVille;
        }

        public Etablissement(string pNom, Guid id)
        {
            Nom = pNom;
            Identifiant = id;
        }
    }
}
