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
using System.Text.RegularExpressions;

namespace MartialArtsCurriculum
{
    public partial class frmGenerateGradingSheets : Form
    {
        CurriculumRoot data;
        string outputPath;
        public frmGenerateGradingSheets(CurriculumRoot data)
        {
            InitializeComponent();
            this.data = data;
            outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MartialArtsCurriculum", "Grading Sheets");
        }

        private void btnBrowseTemplate_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"MartialArtsCurriculum", "html");
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Web Pages (*.htm;*.html)|*.HTM;*.HTML";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
                lblTemplate.Text = openFileDialog1.FileName;
        }

        private void btnBrowseStudents_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MartialArtsCurriculum");
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "CSV Files (*.csv)|*.CSV";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
                lblStudents.Text = openFileDialog1.FileName;
        }
        public void GenerateGradingSheets()
        {
            string html = File.ReadAllText(lblTemplate.Text);
            string patternFirstName = "{{ FirstName }}";
            string patternLastName = "{{ LastName }}";
            string patternDate = "{{ Date }}";
            string patternInstructor = "{{ Instructor }}";
            string patternCurrentRank = "{{ CurrentRank }}";
            string patternBeltAttempting = "{{ BeltAttempting }}";
            string gradingDate = "24/11/2017";

            List<Student> students = Student.LoadStudents(lblStudents.Text);
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            foreach (Student student in students)
            {
                List<CurriculumItem> sheets = student.GetStudentCurriculum(data);
                string outputHTML = html.Replace(patternFirstName, student.FirstName);
                outputHTML = outputHTML.Replace(patternLastName, student.LastName);
                outputHTML = outputHTML.Replace(patternDate, gradingDate);
                outputHTML = outputHTML.Replace(patternInstructor, student.Instructor);
                outputHTML = outputHTML.Replace(patternCurrentRank, student.Rank);
                outputHTML = outputHTML.Replace(patternBeltAttempting, student.RankAttempting);

                string startHTML = outputHTML.Substring(0, outputHTML.IndexOf("{{ StartCategorySection }}"));
                string endHTML = outputHTML.Substring(outputHTML.IndexOf("{{ EndCategorySection }}") + 24);
                Match m = Regex.Match(outputHTML, "{{ StartCategorySection }}(?<CategorySection>.*?){{ EndCategorySection }}", RegexOptions.Singleline);


                string categoryHTML = m.Groups["CategorySection"].Value;
                string startCatHTML = categoryHTML.Substring(0, categoryHTML.IndexOf("{{ StartTechniqueSection }}"));
                string endCatHTML = categoryHTML.Substring(categoryHTML.IndexOf("{{ EndTechniqueSection }}") + 25);
                Match m1 = Regex.Match(categoryHTML, "{{ StartTechniqueSection }}(?<TechniqueSection>.*?){{ EndTechniqueSection }}", RegexOptions.Singleline);
                string techniqueHTML = m1.Groups["TechniqueSection"].Value;
                foreach (CurriculumItem curriculum in sheets)
                {
                    string middleHTML = "";
                    foreach (TechniqueCategory cat in curriculum.categories)
                    {

                        middleHTML += startCatHTML.Replace("{{ TechniqueCategory }}", cat.name);
                        foreach (Technique tech in cat.techniques)
                        {
                            middleHTML += techniqueHTML.Replace("{{ Technique }}", tech.name);
                        }
                        middleHTML += endCatHTML;
                    }
                    string output = startHTML + middleHTML + endHTML;
                    output = output.Replace("images/logo.png", "../html/images/logo.png");
                    StreamWriter sw = File.CreateText(Path.Combine(outputPath, student.FirstName + student.LastName + "-" + curriculum.name + ".html"));
                    sw.Write(output);
                    sw.Close();
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GenerateGradingSheets();
            this.Cursor = Cursors.Default;
            MessageBox.Show("Done.");
        }
    }
}
