using System;
using System.Collections.Generic;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            CV cv = new CVBuilder()
                .AddImage("photo.jpg")
                .AddName("Saikat Das")
                .AddProject("Data importer", new DateTime(2020, 2, 2),
                    new DateTime(2020, 3, 3), new List<string> { "C#", "Asp.net","Worker Service" })
                .GetCV();

        }
    }
}
