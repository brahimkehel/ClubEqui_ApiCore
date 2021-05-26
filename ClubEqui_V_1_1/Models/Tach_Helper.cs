using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubEqui_V_1_1.Models
{
    public class Tach_Helper
    {
        public int IdTask { get; set; }
        public DateTime? DateDebut { get; set; }
        public int? DureeMinutes { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsDone { get; set; }
        public string UserAttached { get; set; }
        public Tach_Helper()
        { }
        public Tach_Helper(int id,DateTime date,int duree,string titre,bool isDone,string user,string description)
        {
            this.IdTask = id;
            this.DateDebut = date;
            this.DureeMinutes = duree;
            this.Title = titre;
            this.Description = description;
            this.IsDone = isDone;
            this.UserAttached = user;
        }
    }
}
