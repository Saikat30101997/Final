using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class CreateImportModel
    {
        public string Name { get; set; }

     
        internal void Create()
        {
            var contactModel = new ContactModel();
            //contactModel.GroupList.Add(Name);
        }
    }
}
