using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Membership.Entities
{
    public class Group : IEntity<int>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
