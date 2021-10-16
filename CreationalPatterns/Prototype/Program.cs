using System;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = GetItem();
            var newDoc = (Document)doc.Clone();
            newDoc.Content = "Content was updated";

            Console.WriteLine(newDoc.OwnerName);

        }

        public static ICloneable GetItem()
        {
            Document doc = new Document();
            doc.Name = "Experiment";
            doc.Content = "This is an experiment";
            doc.OwnerName = "Saikat";
            doc.CreateDate = DateTime.Now;

            return doc;
        }
    }
}
