using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mobileloyaltyapi.Models
{
    public class Profile
    {
        public string UserName { get; set; }

        public int Balance { get; set; }

        public List<string> Transactions { get; set; }
    }
}