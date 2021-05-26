using System;
using System.Collections.Generic;

#nullable disable

namespace ClubEqui_V_1_1.Models
{
    public partial class Client
    {
        public Client()
        {
            Seances = new HashSet<Seance>();
        }

        public int IdClient { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime? DateNais { get; set; }
        public string Photo { get; set; }
        public string IdentityNum { get; set; }
        public DateTime? DateInscription { get; set; }
        public DateTime? ValiditeAssurence { get; set; }
        public string Email { get; set; }
        public string MotPasse { get; set; }
        public int? Telephone { get; set; }
        public bool? IsActive { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Seance> Seances { get; set; }
    }
}
