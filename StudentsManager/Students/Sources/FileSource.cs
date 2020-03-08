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

        /// <summary>
        /// File Source constructor
        /// </summary>
        /// <param name="filePath">Path of the file with students information</param>
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

        public bool AddStudent(Student student)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine(student.ToString());
            }
            return true;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        public List<Student> GetStudents()
        {
            return this.LoadStudents();
        }

        /// <summary>
        /// Get students by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<Student> GetStudents(Func<Student, bool> condition)
        {
            return this.LoadStudents().Where(condition).OrderByDescending(s => s.Name).ToList();
        }

        /// <summary>
        /// Get students by condition and sorted
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<Student> GetStudents(Func<Student, bool> condition, Func<Student, string> sort)
        {
            return this.LoadStudents().Where(condition).OrderByDescending(sort).ToList();
        }

        /// <summary>
        /// Get students by condition and sorted by date
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<Student> GetStudents(Func<Student, bool> condition, Func<Student, DateTime> sort)
        {
            return this.LoadStudents().Where(condition).OrderByDescending(sort).ToList();
        }

        /// <summary>
        /// Get students by multiple conditions and sorted
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<Student> GetStudents(List<Func<Student, bool>> conditions, Func<Student, DateTime> sort)
        {
            //TODO: there must be a better way to use multiple conditions and do not refresh the list for every condition, maybe where.where not sure about performance
            List<Student> students = this.LoadStudents();
            foreach (Func<Student, bool> condition in conditions)
            {
                students = students.Where(condition).ToList();
            }

            return students;
        }

        /// <summary>
        /// Get all students from source file
        /// </summary>
        /// <returns></returns>
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
