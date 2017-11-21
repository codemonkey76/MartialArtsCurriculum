using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Xml;

namespace MartialArtsCurriculum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BeltLevel brown_4 = new BeltLevel();
            brown_4.categories = new TechniqueCategory[4];

            TechniqueCategory tc = brown_4.categories[0]= new TechniqueCategory("Knowledge");
            
            tc.techniques = new Technique[1];
            tc.techniques[0] = new Technique("Participation and development of FCJJ");

            tc = brown_4.categories[1] = new TechniqueCategory("Solo Drills");
            tc.techniques = new Technique[1];
            tc.techniques[0] = new Technique("Demonstrate your own solo drill");

            tc = brown_4.categories[2] = new TechniqueCategory("Standing Combinations");
            tc.techniques = new Technique[3];
            tc.techniques[0] = new Technique("Double leg to run the pipe");
            tc.techniques[1] = new Technique("Run the pipe to high crotch");
            tc.techniques[2] = new Technique("Scissor takedown");

            tc = brown_4.categories[3] = new TechniqueCategory("Heel Hooks");
            tc.techniques = new Technique[8];
            tc.techniques[0] = new Technique("Heel hook defending knee shield");
            tc.techniques[1] = new Technique("Heel hook defending x-guard");
            tc.techniques[2] = new Technique("Reverse toehold");
            tc.techniques[3] = new Technique("Inverted heel hook from 50/50");
            tc.techniques[4] = new Technique("Heel hook from butterfly sweep");
            tc.techniques[5] = new Technique("Heel hook from open guard (standing)");
            tc.techniques[6] = new Technique("Knee reap submission");
            tc.techniques[7] = new Technique("Knee reap footlock");

            XmlSerializer x = new XmlSerializer(typeof(BeltLevel));
            FileStream fs = File.OpenWrite("test.xml");
            x.Serialize(new StreamWriter(fs),brown_4);
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            string xsltString = File.ReadAllText("grading-sheet.xsl");

            using (XmlReader reader = XmlReader.Create(new StringReader(xsltString)))
            {
                transform.Load(reader);
            }

            StringWriter results = new StringWriter();
            string inputXml = File.ReadAllText("test.xml");
            using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
            {
                transform.Transform(reader, null, results);
            }
            File.WriteAllText("output.html", results.ToString());
        }
    }
}
