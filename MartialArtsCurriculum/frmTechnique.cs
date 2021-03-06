﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MartialArtsCurriculum
{
    public partial class frmTechnique : Form
    {
        CurriculumItem curriculum;
        TechniqueCategory category;
        public Technique newTechnique;
        public frmTechnique()
        {
            InitializeComponent();
            populateCategories();
        }
        public frmTechnique(CurriculumItem curriculum)
        {
            this.curriculum = curriculum;
            InitializeComponent();
            populateCategories();

        }
        public frmTechnique(CurriculumItem curriculum, TechniqueCategory category)
        {
            this.category = category;
            this.curriculum = curriculum;
            InitializeComponent();
            populateCategories();
        }

        public void populateCategories()
        {
            cbCategory.Items.Clear();
            cbCategory.DisplayMember = "name";
            //cbCategory.SelectedIndex = 0;
            foreach (TechniqueCategory cat in curriculum.categories)
            {
                int index = cbCategory.Items.Add(cat);
                if (this.category != null && this.category == cat)
                {
                    cbCategory.SelectedIndex = index;
                    txtCategory.Visible = false;
                    txtTechnique.Enabled = true;
                }
            }
            cbCategory.Items.Add("<New Category>");
            if (cbCategory.SelectedIndex < 0)
                cbCategory.SelectedIndex = 0;

        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCategory.Text = "";

            if (cbCategory.SelectedIndex == cbCategory.Items.Count - 1)
            {
                txtCategory.Visible = true;
                if (txtCategory.Text != "")
                    txtTechnique.Enabled = true;
                else
                {
                    txtTechnique.Enabled = false;
                    btnOK.Enabled = false;
                }
            }
            else
            {
                txtCategory.Visible = false;
                txtTechnique.Enabled = true;
            }
        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {
            if (txtCategory.Text != "")
            {
                txtTechnique.Enabled = true;
            }
            else
            {
                txtTechnique.Enabled = false;
                btnOK.Enabled = false;
            }
        }

        private void txtTechnique_TextChanged(object sender, EventArgs e)
        {
            if (txtTechnique.Text != "")
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex == cbCategory.Items.Count - 1)
            {
                TechniqueCategory cat = curriculum.AddTechCategory(txtCategory.Text);
                newTechnique = cat.AddTechnique(txtTechnique.Text);
            }
            else
            {
                TechniqueCategory cat = (TechniqueCategory)cbCategory.SelectedItem;
                newTechnique = cat.AddTechnique(txtTechnique.Text);
            }
        }
    }
}
