using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MartialArtsCurriculum
{
    public class Technique
    {
        [XmlAttribute]
        public string name;

        public Technique(string name)
        {
            this.name = name;
        }
        public Technique()
        {

        }
    }
}
