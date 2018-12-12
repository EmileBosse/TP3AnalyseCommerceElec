using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3InterfaceAnalyse
{
    public class Mission
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Pays { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        public Mission()
        {
        }

        public Mission(string pNom, Guid pId)
        {
            Nom = pNom;
            Id = pId;
        }

        public Mission(Guid pId, string pNom, string pPays, DateTime pDateDebut, DateTime pDateFin)
        {
            Id = pId;
            Nom = pNom;
            Pays = pPays;
            DateDebut = pDateDebut;
            DateFin = pDateFin;
        }

    }
}
