using System;
using System.Windows.Forms;
using System.IO;
using System.Xml.Xsl;
using System.Xml;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;

namespace MartialArtsCurriculum
{
    public partial class Form1 : Form
    {
        CurriculumRoot data;
        CurriculumItem selectedCurriculum;
        string DataFile;
        string SavePath;
        public Form1()
        {
            InitializeComponent();

            SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MartialArtsCurriculum");
            DataFile = "Data.xml";
            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);
                
            data = LoadData(Path.Combine(SavePath, DataFile));
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
            selectedCurriculum = (CurriculumItem)currentNode.Tag;

            foreach (TechniqueCategory cat in selectedCurriculum.categories)
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

        private void btnAddCurriculum_Click(object sender, EventArgs e)
        {
            frmCurriculum f = new frmCurriculum(data);
            f.ShowDialog();
            BindCurriculum();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            data.Save(Path.Combine(SavePath, DataFile));
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
        private void MoveUp(TreeNode tn, TreeView tv)
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
                tv.Nodes.Insert(originalIndex - 1, cloned);
                tv.SelectedNode = cloned;
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
        private void MoveDown(TreeNode tn, TreeView tv)
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
                tv.Nodes.Insert(originalIndex + 1, cloned);
                tv.SelectedNode = cloned;
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
                    MoveUp(tn, tvCurriculum);
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
                    MoveDown(tn, tvCurriculum);
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

            CurriculumItem item = selectedCurriculum;
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
            {                
                BindTechniques();
            }
        }

        private void btnDeleteTechnique_Click(object sender, EventArgs e)
        {
            TreeNode tn = tvTechniques.SelectedNode;
            DialogResult rslt = MessageBox.Show("Are you sure you want to delete the selected node?", "Delete Node", MessageBoxButtons.YesNo);

            if (rslt == DialogResult.Yes)
            {
                Type NodeType = tn.Tag.GetType();

                if (NodeType == typeof(Technique))
                {
                    TechniqueCategory cat = (TechniqueCategory)tn.Parent.Tag;
                    cat.techniques.Remove((Technique)tn.Tag);
                }

                if (NodeType == typeof(TechniqueCategory))
                {
                    TechniqueCategory cat = (TechniqueCategory)tn.Tag;
                    selectedCurriculum.categories.Remove(cat);
                }
                BindTechniques();
            }
        }

        
        

        public bool InputBox(string caption, string prompt, object clickedNode)
        {
            iHasName node = (iHasName)clickedNode;

            frmInputBox f = new frmInputBox(caption, prompt, node.name);

            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                node.name = f.txtInput.Text;
                return true;
            }
            return false;
        }

        private void tvCurriculum_DoubleClick(object sender, EventArgs e)
        {
            if (InputBox("Rename item", "Enter the new name:",tvCurriculum.SelectedNode.Tag))
                BindCurriculum();                
        }

        private void tvTechniques_DoubleClick(object sender, EventArgs e)
        {
            if (InputBox("Rename item", "Enter the new name:", tvTechniques.SelectedNode.Tag))
                BindTechniques();
        }

        private void tvTechniques_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void btnTechUp_Click(object sender, EventArgs e)
        {
            if (tvTechniques.SelectedNode != null)
            {
                TreeNode tn = tvTechniques.SelectedNode;

                if (tn.Index != 0)
                    MoveUp(tn, tvTechniques);
                else
                    if (tn.Parent != null && tn.Parent.Index != 0)
                    MoveToPrev(tn);
            }
            tvTechniques.Focus();
        }

        private void btnTechDown_Click(object sender, EventArgs e)
        {
            if (tvTechniques.SelectedNode != null)
            {
                TreeNode tn = tvTechniques.SelectedNode;

                if (tn.NextNode != null)
                    MoveDown(tn, tvTechniques);
                else
                    if (tn.Parent != null && tn.Parent.Index != tn.Parent.Nodes.Count - 1)
                    MoveToNext(tn);
            }
            tvTechniques.Focus();
        }

        private void btnGenerateGradingSheets_Click(object sender, EventArgs e)
        {
            frmGenerateGradingSheets f = new frmGenerateGradingSheets(data);
            f.ShowDialog();
        }
    }
}
