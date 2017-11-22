using System;
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
    public partial class frmCurriculum : Form
    {
        CurriculumRoot root;
        public frmCurriculum(CurriculumRoot root)
        {
            this.root = root;
            InitializeComponent();
            populateCategories();
            cbCat.SelectedIndex = 0;
            populateLevels();
            cbLevel.SelectedIndex = 0;
        }

        public void populateCategories()
        {
            cbCat.Items.Clear();
            cbCat.DisplayMember = "name";
            foreach (CurriculumCategory cat in this.root.categories)
                cbCat.Items.Add(cat);
            cbCat.Items.Add("<New Category>");
        }
        public void populateLevels()
        {
            cbLevel.Items.Clear();
            if (cbCat.SelectedItem.GetType() != typeof(string))
            {
                CurriculumCategory cat = (CurriculumCategory)cbCat.SelectedItem;
                foreach (CurriculumLevel level in cat.levels)
                    cbLevel.Items.Add(level);                
            }
            cbLevel.Items.Add("<New Level>");
        }
        private void cbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCurriculum.Text = "";
            if (cbCat.SelectedIndex == cbCat.Items.Count - 1)
            {
                txtCategory.Visible = true;
                if (txtCategory.Text != "")
                    cbLevel.Enabled = true;
                else
                {
                    cbLevel.Enabled = false;
                    btnOK.Enabled = false;
                }
            }
            else
            {
                txtCategory.Visible = false;
                cbLevel.Enabled = true;
                populateLevels();
            }
                
        }

        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLevel.Enabled)
            {
                txtCurriculum.Text = "";
                if (cbLevel.SelectedIndex == cbLevel.Items.Count - 1)
                {
                    txtLevel.Visible = true;
                    if (txtLevel.Text != "")
                        txtCurriculum.Enabled = true;
                    else
                        txtCurriculum.Enabled = false;
                }
                else
                {
                    txtLevel.Visible = false;
                    txtCurriculum.Enabled = true;
                }
            }
        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {
            if (txtCategory.Text != "")
            {
                cbLevel.Enabled = true;
                cbLevel.SelectedIndex = -1;
                cbLevel.SelectedIndex = 0;
            }
            else
            {
                cbLevel.Enabled = false;
                txtLevel.Text = "";
                txtLevel.Visible = false;
                txtCurriculum.Text = "";
                txtCurriculum.Enabled = false;
            }
        }

        private void txtLevel_TextChanged(object sender, EventArgs e)
        {
            if (txtLevel.Text != "")
                txtCurriculum.Enabled = true;
            else
            {
                txtCurriculum.Text = "";
                txtCurriculum.Enabled = false;
                
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CurriculumCategory cat;
            CurriculumLevel level;

            if (cbCat.SelectedIndex < cbCat.Items.Count - 1)
                cat = (CurriculumCategory)cbCat.SelectedItem;
            else
            {
                cat = new CurriculumCategory(txtCategory.Text);
                root.categories.Add(cat);
            }

            if (cbLevel.SelectedIndex < cbLevel.Items.Count - 1)
                level = (CurriculumLevel)cbLevel.SelectedItem;
            else
            {
                level = new CurriculumLevel(txtLevel.Text);
                cat.levels.Add(level);
            }

            level.AddCurriculum(txtCurriculum.Text);        
        }

        private void txtCurriculum_TextChanged(object sender, EventArgs e)
        {
            if (txtCurriculum.Text != "")
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;
        }
    }
}
