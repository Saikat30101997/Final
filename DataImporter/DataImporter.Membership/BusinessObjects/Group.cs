using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Membership.BusinessObjects
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public Guid UserId { get; set; }
    }
}
