using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3InterfaceAnalyse
{
    public class Etudiant
    {
        public string identifiant { get; set; }
        public string nom { get; set; }
        public string pays { get; set; }
        public string prenom { get; set; }
        public string adresse { get; set; }
        public string ville { get; set; }
        public string codePermanent { get; set; }

        public Etudiant()
        {
        }

        public Etudiant(string pNom, string id)
        {
            nom = pNom;
            identifiant = id;
        }

        public Etudiant(string pNom, string id, string pPrenom, string pAdresse, string pVille, string pPays, string pCodePermanent)
        {
            nom = pNom;
            identifiant = id;
            prenom = pPrenom;
            adresse = pAdresse;
            ville = pVille;
            pays = pPays;
            codePermanent = pCodePermanent;
        }
    }
}
