using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3InterfaceAnalyse
{
    public class Etudiant
    {
        public string Identifiant { get; set; }
        public string Nom { get; set; }
        public string Pays { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string CodePermanent { get; set; }
        public string Etat { get; set; }

        public Etudiant()
        {
        }

        public Etudiant(string pNom, string id)
        {
            Nom = pNom;
            Identifiant = id;
        }

        public Etudiant(string pNom, string id, string pPrenom, string pAdresse, string pVille, string pPays, string pCodePermanent)
        {
            Nom = pNom;
            Identifiant = id;
            Prenom = pPrenom;
            Adresse = pAdresse;
            Ville = pVille;
            Pays = pPays;
            CodePermanent = pCodePermanent;
        }
    }
}
