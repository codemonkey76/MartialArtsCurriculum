using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MartialArtsCurriculum
{
    [XmlRoot("category")]
    public class TechniqueCategory
    {
        [XmlAttribute]
        public string name;

        public List<Technique> techniques;
        public Technique AddTechnique(string name)
        {
            Technique tech = new Technique(name);
            this.techniques.Add(tech);
            return tech;
        }
        public TechniqueCategory(string name)
        {
            this.name = name;
            this.techniques = new List<Technique>();
        }
       

        public TechniqueCategory()
        {
            this.techniques = new List<Technique>();
        }
        public override string ToString()
        {
            return name;
        }
    }
}
