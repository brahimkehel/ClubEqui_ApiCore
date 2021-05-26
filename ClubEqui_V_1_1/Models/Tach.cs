using System;
using System.Collections.Generic;

#nullable disable

namespace ClubEqui_V_1_1.Models
{
    public partial class Tach
    {
        public int IdTask { get; set; }
        public DateTime? DateDebut { get; set; }
        public int? DureeMinutes { get; set; }
        public string Title { get; set; }
        public bool? IsDone { get; set; }
        public int? UserAttached { get; set; }
        public string Description { get; set; }

        public virtual Utilisateur UserAttachedNavigation { get; set; }
    }
}
