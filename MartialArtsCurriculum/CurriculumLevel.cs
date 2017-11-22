using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartialArtsCurriculum
{
    public class CurriculumLevel
    {
        public string name;

        public List<CurriculumItem> curriculum;
        public CurriculumItem AddCurriculum(string name)
        {
            CurriculumItem item = new CurriculumItem(name);
            this.curriculum.Add(item);
            return item;
        }
        public CurriculumLevel(string name)
        {
            this.name = name;
            this.curriculum = new List<CurriculumItem>();
        }
        public CurriculumLevel()
        {
            this.curriculum = new List<CurriculumItem>();
        }
        public override string ToString()
        {
            return name;
        }
    }
}
