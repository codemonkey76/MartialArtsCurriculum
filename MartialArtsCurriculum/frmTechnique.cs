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
    public partial class frmTechnique : Form
    {
        CurriculumItem curriculum;
        TechniqueCategory category;

        public frmTechnique()
        {
            InitializeComponent();
        }
        public frmTechnique(CurriculumItem curriculum) : this()
        {
            this.curriculum = curriculum;
        }
        public frmTechnique(CurriculumItem curriculum, TechniqueCategory category) : this(curriculum)
        {
            this.category = category;
        }
    }
}
