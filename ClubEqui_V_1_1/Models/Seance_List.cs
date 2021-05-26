using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubEqui_V_1_1.Models
{
    public class Seance_List
    {
        public List<Seance_Helper> Seances { get; set; }
        public Seance_List()
        {
            this.Seances = new List<Seance_Helper>();
        }

    }
}
