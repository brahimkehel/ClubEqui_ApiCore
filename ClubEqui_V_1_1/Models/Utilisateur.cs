using System;
using System.Collections.Generic;

#nullable disable

namespace ClubEqui_V_1_1.Models
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Seances = new HashSet<Seance>();
            Taches = new HashSet<Tach>();
        }

        public int IdUtilisateur { get; set; }
        public string Email { get; set; }
        public string MotPasse { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool? IsActive { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string TypeUtilsateur { get; set; }
        public string Photo { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? Telephone { get; set; }

        public virtual ICollection<Seance> Seances { get; set; }
        public virtual ICollection<Tach> Taches { get; set; }
    }
}
