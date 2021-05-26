using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubEqui_V_1_1.Models
{
    public class Client_List
    {
        public List<Client> Clients { get; set; }
        public Client_List()
        {
            this.Clients = new List<Client>();
        }
    }
}
