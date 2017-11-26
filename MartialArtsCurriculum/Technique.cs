using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MartialArtsCurriculum
{
    public class Technique : iHasName
    {
        [XmlAttribute]
        public string name { get { return _name; } set { _name = value; } }
        string _name;

        public Technique(string name)
        {
            this.name = name;
        }
        public Technique()
        {

        }
    }
}
