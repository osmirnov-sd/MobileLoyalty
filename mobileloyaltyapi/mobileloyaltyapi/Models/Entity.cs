using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mobileloyaltyapi.Models
{
    public class Entity
    {
        [Key]
        public Guid EntityId { get; set; }
    }
}