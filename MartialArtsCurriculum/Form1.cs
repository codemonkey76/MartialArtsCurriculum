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
        CurriculumRoot data;
        public Form1()
        {
            InitializeComponent();
            data = LoadData("Data.xml");
            BindCurriculum();
        }

        public void BindCurriculum()
        {
            this.tvCurriculum.Nodes.Clear();
            
            foreach (CurriculumCategory cat in data.categories)
            {
                TreeNode tn = new TreeNode(cat.name);
                tn.Tag = cat;
                foreach (CurriculumLevel level in cat.levels)
                {
                    TreeNode tn1 = new TreeNode(level.name);
                    tn1.Tag = level;
                    foreach (CurriculumItem item in level.curriculum)
                    {
                        TreeNode tn2 = new TreeNode(item.name);
                        tn2.Tag = item;
                        tn1.Nodes.Add(tn2);
                    }
                    tn.Nodes.Add(tn1);
                }
                this.tvCurriculum.Nodes.Add(tn);
            }
        }
        public void BindTechniques()
        {
            this.tvTechniques.Nodes.Clear();

            CurriculumItem item = (CurriculumItem)tvCurriculum.SelectedNode.Tag;

            foreach (TechniqueCategory cat in item.categories)
            {
                TreeNode tn = new TreeNode(cat.name);
                tn.Tag = cat;
                foreach (Technique tech in cat.techniques)
                {
                    TreeNode tn1 = new TreeNode(tech.name);
                    tn1.Tag = tech;
                    tn.Nodes.Add(tn1);
                }
                this.tvTechniques.Nodes.Add(tn);
            }            
        }
        public CurriculumRoot LoadData(string filename)
        {
            return CurriculumRoot.Load(filename);
        }

        CurriculumRoot TestData()
        {
            CurriculumRoot testData = new CurriculumRoot();

            
            CurriculumCategory tempCat = testData.AddCategory("Adults BJJ");
            CurriculumLevel tempLevel = tempCat.AddLevel("White Belt");
            CurriculumItem tempItem = tempLevel.AddCurriculum("White Belt - 1st Degree");
            TechniqueCategory tempTechCat = tempItem.AddTechCategory("Knowledge");
            Technique tempTech = tempTechCat.AddTechnique("Dojo etiquette, basic disease control, meaning of osu");

            tempTechCat = tempItem.AddTechCategory("Solo Drills");            
            tempTechCat.AddTechnique("Backward break fall");
            tempTechCat.AddTechnique("Forward break fall");
            tempTechCat.AddTechnique("Side break fall");

            tempTechCat = tempItem.AddTechCategory("Partner Drill");
            tempTechCat.AddTechnique("Run around the legs");
            tempTechCat = tempItem.AddTechCategory("Standing");
            tempTechCat.AddTechnique("Kumi kata (judo grips)");

            return testData;
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

        private void btnAddCurriculum_Click(object sender, EventArgs e)
        {
            frmCurriculum f = new frmCurriculum(data);
            f.ShowDialog();
            BindCurriculum();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            data.Save("test.xml");
        }

        private void btnDeleteCurriculum_Click(object sender, EventArgs e)
        {
            TreeNode tn = tvCurriculum.SelectedNode;
            DialogResult rslt = MessageBox.Show("Are you sure you want to delete the selected node?", "Delete Node", MessageBoxButtons.YesNo);

            if (rslt == DialogResult.Yes)
            {
                Type NodeType = tn.Tag.GetType();

                if (NodeType == typeof(CurriculumItem))
                {
                    CurriculumLevel level = (CurriculumLevel)tn.Parent.Tag;
                    level.curriculum.Remove((CurriculumItem)tn.Tag);
                }

                if (NodeType == typeof(CurriculumLevel))
                {
                    CurriculumCategory cat = (CurriculumCategory)tn.Parent.Tag;
                    cat.levels.Remove((CurriculumLevel)tn.Tag);                    
                }

                if (NodeType == typeof(CurriculumCategory))
                {                    
                    data.categories.Remove((CurriculumCategory)tn.Tag);
                }
                BindCurriculum();

            }
        }

        private void tvCurriculum_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Type NodeType = tvCurriculum.SelectedNode.Tag.GetType();
            if (NodeType == typeof(CurriculumItem))
            {
                tvTechniques.Enabled = true;
                BindTechniques();
            }
            else
                tvTechniques.Enabled = false;
        }
    }
}
