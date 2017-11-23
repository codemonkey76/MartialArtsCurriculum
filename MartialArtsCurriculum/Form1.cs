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
            BindTechniques();
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
            TreeNode currentNode = tvCurriculum.SelectedNode;
            if (currentNode == null)
                return;
            CurriculumItem item = (CurriculumItem)currentNode.Tag;

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
        private void MoveUp(TreeNode tn)
        {
            TreeNode parent = tn.Parent;
            int originalIndex = tn.Index;
            TreeNode cloned = (TreeNode)tn.Clone();
            tn.Remove();
            if (parent != null)
            {                
                parent.Nodes.Insert(originalIndex - 1, cloned);
                parent.TreeView.SelectedNode = cloned;
            }
            else
            {
                tvCurriculum.Nodes.Insert(originalIndex - 1, cloned);
                tvCurriculum.SelectedNode = cloned;
            }
        }

        private void MoveToPrev(TreeNode tn)
        {
            TreeNode parent = tn.Parent;
            TreeNode cloned = (TreeNode)tn.Clone();
            tn.Remove();
            parent.PrevNode.Nodes.Add(cloned);
            parent.TreeView.SelectedNode = cloned;
        }
        private void MoveDown(TreeNode tn)
        {
            TreeNode parent = tn.Parent;
            int originalIndex = tn.Index;
            TreeNode cloned = (TreeNode)tn.Clone();
            tn.Remove();
            if (parent != null)
            {
                parent.Nodes.Insert(originalIndex + 1, cloned);
                parent.TreeView.SelectedNode = cloned;
            }
            else
            {
                tvCurriculum.Nodes.Insert(originalIndex + 1, cloned);
                tvCurriculum.SelectedNode = cloned;
            }
        }

        private void MoveToNext(TreeNode tn)
        {
            TreeNode parent = tn.Parent;
            TreeNode cloned = (TreeNode)tn.Clone();
            tn.Remove();
            parent.NextNode.Nodes.Insert(0,cloned);
            parent.TreeView.SelectedNode = cloned;
        }

        private void btnCurUp_Click(object sender, EventArgs e)
        {
            if (tvCurriculum.SelectedNode != null)
            {
                TreeNode tn = tvCurriculum.SelectedNode;

                if (tn.Index != 0)
                    MoveUp(tn);
                else
                    if (tn.Parent !=null && tn.Parent.Index != 0)
                        MoveToPrev(tn);
            }
            tvCurriculum.Focus();
        }

        private void btnCurDown_Click(object sender, EventArgs e)
        {
            if (tvCurriculum.SelectedNode != null)
            {
                TreeNode tn = tvCurriculum.SelectedNode;

                if (tn.NextNode != null)
                    MoveDown(tn);
                else
                    if (tn.Parent != null && tn.Parent.Index != tn.Parent.Nodes.Count-1)
                    MoveToNext(tn);
            }
            tvCurriculum.Focus();
        }

        private void btnAddTechnique_Click(object sender, EventArgs e)
        {
            Type NodeType = tvCurriculum.SelectedNode.Tag.GetType();
            frmTechnique f = null;

            if (NodeType != typeof(CurriculumItem))
            {
                MessageBox.Show("Click a curriculum item in the left tree first");
                return;
            }

            CurriculumItem item = (CurriculumItem)tvCurriculum.SelectedNode.Tag;
            if (tvTechniques.SelectedNode != null)
            {
                Type TechNodeType = tvTechniques.SelectedNode.Tag.GetType();
                if (TechNodeType == typeof(Technique))
                    f = new frmTechnique(item, (TechniqueCategory)tvTechniques.SelectedNode.Parent.Tag);
                else
                    f = new frmTechnique(item, (TechniqueCategory)tvTechniques.SelectedNode.Tag);
            }
            else
                f = new frmTechnique(item);

            DialogResult rslt = f.ShowDialog();
            if (rslt == DialogResult.OK)
                BindTechniques();
        }
    }
}
