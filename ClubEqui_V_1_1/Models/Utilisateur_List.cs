using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubEqui_V_1_1.Models
{
    public class Utilisateur_List
    {
        public List<Utilisateur> Utilisateurs { get; set; }
        public Utilisateur_List()
        {
            this.Utilisateurs = new List<Utilisateur>();
        }
    }
}
