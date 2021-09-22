using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Entities
{
    public class Import : IEntity<int>
    {
        public int Id { get; set; }
        public string ExcelFileName { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public Guid UserId { get; set; }
        public DateTime ImportDate { get; set; }
        public string Status { get; set; }
        public List<ExcelData> ExcelDatas { get; set; }
    }
}
