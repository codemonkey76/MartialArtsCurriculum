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

        public Technique[] techniques;

        public TechniqueCategory(string name)
        {
            this.name = name;
        }

        public TechniqueCategory()
        {

        }
    }
}
