using DataImporter.Data;
using DataImporter.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Entities
{
    public class Group : IEntity<int>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<Import> Imports { get; set; }
        public List<Export> Exports { get; set; }
    }
}
