using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3InterfaceAnalyse
{
    public class Programme
    {
        public string Code { get; set; }
        public string Cycle { get; set; }
        public string Departement { get; set; }
        public string Nom { get; set; }
        public string Identifiant { get; set; }

        public Programme() { }

        public Programme(string id, string code, string nom, string cycle, string departement)
        {
            this.Code = code;
            this.Identifiant = id;
            this.Nom = nom;
            this.Cycle = cycle;
            this.Departement = departement;
        }
    }
}
