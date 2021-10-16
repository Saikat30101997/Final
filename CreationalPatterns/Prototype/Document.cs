using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    public class Document : ICloneable
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string OwnerName { get; set; }
        public DateTime CreateDate { get; set; }

        public object Clone()
        {
            return Copy();
        }

        public Document Copy()
        {
            var newDoc = new Document();
            newDoc.Name = Name;
            newDoc.Content = Content;
            newDoc.OwnerName = OwnerName;
            newDoc.CreateDate = DateTime.Now;

            return newDoc;
        }

    }
}
