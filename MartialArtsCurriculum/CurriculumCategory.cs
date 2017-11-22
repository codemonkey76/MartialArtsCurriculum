using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartialArtsCurriculum
{
    public class CurriculumCategory
    {
        public string name;

        public List<CurriculumLevel> levels;
        public CurriculumLevel AddLevel(string name)
        {
            CurriculumLevel level = new CurriculumLevel(name);
            this.levels.Add(level);
            return level;
        }
        public CurriculumCategory(string name)
        {
            this.name = name;
            this.levels = new List<CurriculumLevel>();
        }
        public CurriculumCategory()
        {
            this.levels = new List<CurriculumLevel>();
        }
        public override string ToString()
        {
            return name;
        }
    }
}
