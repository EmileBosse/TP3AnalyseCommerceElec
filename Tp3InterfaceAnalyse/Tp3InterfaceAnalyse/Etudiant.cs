using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3InterfaceAnalyse
{
    public class Etudiant
    {
        private string identifiant;
        private string nom;
        private string pays;
        private string prenom;
        private string adresse;
        private string ville;
        private string codePermanent;

        public Etudiant()
        {
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
