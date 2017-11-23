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
    public partial class frmGradingSheet : Form
    {
        public frmGradingSheet()
        {
            InitializeComponent();
            webBrowser1.Navigate("file://" + Application.StartupPath + "/html/index.html");
        }
    }
}
