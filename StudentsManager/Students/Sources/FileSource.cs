using Students.Models;
using Students.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Sources
{
    /// <summary>
    /// File Source, it retrieves information from files
    /// </summary>
    public class FileSource
    {
        private string _filePath;
        private StudentParser _studentParser;

        public FileSource(string filePath)
        {
            FilePath = filePath;
            _studentParser = new StudentParser();
        }

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public List<Student> GetStudents()
        {
            return this.LoadStudents();
        }

        public List<Student> GetStudents(Func<Student, bool> condition)
        {            
            return this.LoadStudents().Where(condition).OrderByDescending(s=>s.Name).ToList();
        }

        private List<Student> LoadStudents()
        {
            List<Student> students = new List<Student>();
            using (StreamReader reader = new StreamReader(FilePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    students.Add(_studentParser.Parse(values));
                }
            }
            return students;
        }
    }
}
