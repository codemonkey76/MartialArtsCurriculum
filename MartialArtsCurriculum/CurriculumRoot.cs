using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace MartialArtsCurriculum
{
    public class CurriculumRoot
    {
        public List<CurriculumCategory> categories;

        public CurriculumRoot()
        {
            categories = new List<CurriculumCategory>();
        }
        public CurriculumCategory AddCategory(string name)
        {
            CurriculumCategory cat = new CurriculumCategory(name);
            this.categories.Add(cat);
            return cat;
        }
        

        public void Save(string filename)
        {
            XmlSerializer x = new XmlSerializer(typeof(CurriculumRoot));
            FileStream fs = File.Create("test.xml");
            x.Serialize(new StreamWriter(fs),this);
            fs.Close();
        }
        public static CurriculumRoot Load(string filename)
        {
            XmlSerializer x = new XmlSerializer(typeof(CurriculumRoot));
            FileStream fs = File.OpenRead("test.xml");
            CurriculumRoot root = (CurriculumRoot)x.Deserialize(fs);
            fs.Close();
            return root;
        }
    }
}
