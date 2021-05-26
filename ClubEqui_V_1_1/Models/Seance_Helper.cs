using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubEqui_V_1_1.Models
{
    public class Seance_Helper
    {
        public int IdSeance { get; set; }
        public string IdClient { get; set; }
        public string IdMoniteur { get; set; }
        public DateTime? DateDebut { get; set; }
        public int? DureeMinutes { get; set; }
        public bool? IsDone { get; set; }
        public int? IdPayement { get; set; }
        public string Commentaires { get; set; }

        public Seance_Helper()
        {

        }
        public Seance_Helper(int IdSeance,string IdClient,string IdMoniteur,DateTime DateDebut,bool IsDone, int IdPayement, string Commentaires)
        {
            this.IdSeance = IdSeance;
            this.IdClient = IdClient;
            this.IdMoniteur = IdMoniteur;
            this.DateDebut = DateDebut;
            this.IsDone = IsDone;
            this.IdPayement = IdPayement;
            this.Commentaires = Commentaires;
        }
    }
}
