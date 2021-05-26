using System;
using System.Collections.Generic;

#nullable disable

namespace ClubEqui_V_1_1.Models
{
    public partial class Seance
    {
        public int IdSeance { get; set; }
        public int? IdClient { get; set; }
        public int? IdMoniteur { get; set; }
        public DateTime? DateDebut { get; set; }
        public int? DureeMinutes { get; set; }
        public bool? IsDone { get; set; }
        public int? IdPayement { get; set; }
        public string Commentaires { get; set; }

        public virtual Client IdClientNavigation { get; set; }
        public virtual Utilisateur IdMoniteurNavigation { get; set; }
    }
}
