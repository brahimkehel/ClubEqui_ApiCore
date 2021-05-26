using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubEqui_V_1_1.Models
{
    public class Taches_List
    {
        public List<Tach_Helper> Taches { get; set; }
        public Taches_List()
        {
            this.Taches = new List<Tach_Helper>();
        }
    }
}
