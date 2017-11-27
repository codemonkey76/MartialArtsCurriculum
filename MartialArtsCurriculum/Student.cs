using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartialArtsCurriculum
{
    public class Student
    {
        public string FirstName;
        public string LastName;
        public string Instructor;
        public string Age;
        public string Category;
        public string Level;
        public string Rank;
        public string LevelAttempting;
        public string RankAttempting;

        public List<CurriculumItem> GetStudentCurriculum(CurriculumRoot data)
        {
            CurriculumCategory cat = data.categories.Find(x => x.name == this.Category);
            List<CurriculumLevel> levelList = new List<CurriculumLevel>();
            bool found = false;
            List<CurriculumItem> curriculumList = new List<CurriculumItem>();

            foreach (CurriculumLevel level in cat.levels)
            {
                if (level.name == this.Level)
                {
                    foreach (CurriculumItem item in level.curriculum)
                    {
                        if (this.Rank == "")
                            found = true;

                        if (item.name == this.Rank)
                        {
                            found = true;
                            continue;
                        }

                        if (found)
                        {
                            curriculumList.Add(item);
                        }
                        if (item.name == this.RankAttempting)
                        {
                            found = false;
                            break;
                        }
                    }
                    break;
                }
            }

            return curriculumList;
        }

        public static List<Student> LoadStudents(string path)
        {
            List<Student> students = new List<Student>();
            
            if (File.Exists(path))
            {
                using (TextFieldParser csvParser = new TextFieldParser(path))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { "," });
                    csvParser.HasFieldsEnclosedInQuotes = true;

                    //Skip header row
                    csvParser.ReadLine();

                    while (!csvParser.EndOfData)
                    {
                        string[] fields = csvParser.ReadFields();
                        Student student = new Student();
                        student.FirstName = fields[0];
                        student.LastName = fields[1];
                        student.Instructor = fields[2];
                        student.Category = fields[3];
                        student.Level = fields[4];
                        student.Rank = fields[5];
                        student.RankAttempting = fields[6];
                        students.Add(student);
                    }
                }
            }
            return students;
        }
    }
}
