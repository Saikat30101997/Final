using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class CVBuilder
    {
        private CV _cv;
        public CVBuilder()
        {
            _cv = new CV();
            _cv.References = new List<string>();
            _cv.Educations = new List<string>();
            _cv.Projects = new List<Project>();
            _cv.Skills = new List<string>();
        }

        public CVBuilder AddName(string name)
        {
            _cv.Name = name;
            return this;
        }

        public CVBuilder AddImage(string image)
        {
            _cv.Image = image;
            return this;
        }

        public CVBuilder AddReference(string reference)
        {
            _cv.References.Add(reference);
            return this;
        }

        public CVBuilder AddProject(string projectName, DateTime start, DateTime end, List<string> skills)
        {
            _cv.Projects.Add(new Project { Name = projectName, Start = start, End = end, Skills = skills });
            return this;
        }

        public CV GetCV()
        {
            return _cv;
        }
    }
}
