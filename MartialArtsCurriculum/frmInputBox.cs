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
    public partial class frmInputBox : Form
    {        

        public frmInputBox(string caption, string prompt, string defaultInput)
        {
            InitializeComponent();
            this.Text = caption;
            this.lblInput.Text = prompt;
            this.txtInput.Text = defaultInput;            
        }
    }
}
