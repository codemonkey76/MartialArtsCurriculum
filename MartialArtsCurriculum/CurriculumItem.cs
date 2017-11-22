using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartialArtsCurriculum
{
    public class CurriculumItem
    {
        public string name;

        public List<TechniqueCategory> categories;
        public TechniqueCategory AddTechCategory(string name)
        {
            TechniqueCategory cat = new TechniqueCategory(name);
            this.categories.Add(cat);
            return cat;
        }

        public CurriculumItem(string name)
        {
            this.name = name;
            this.categories = new List<TechniqueCategory>();
        }
        public CurriculumItem()
        {
            this.categories = new List<TechniqueCategory>();
        }
    }
}
