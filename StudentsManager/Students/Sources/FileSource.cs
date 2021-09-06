using Students.Models;
using Students.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Students.Sources
{
    /// <summary>
    /// File Source, it retrieves information from files
    /// </summary>
    public class FileSource : IDataSource
    {
        private string _filePath;
        private StudentParser _studentParser;
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

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

        /// <summary>
        /// Adds a student to source file
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool AddStudent(Student student)
        {
            _readWriteLock.EnterWriteLock();
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath, true))
                {
                    writer.WriteLine(student.ToString());
                }
            }
            catch (Exception ex)
            {
                // Log something here
                throw ex;
            }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }
            return true;
        }


        /// <summary>
        /// Deletes a student from file source
        /// </summary>
        /// <param name="id">identifier of student</param>
        /// <returns></returns>
        public bool DeleteStudent(string id)
        {
            List<Student> students = new List<Student>();
            string newCsvFileInfo = string.Empty;
            _readWriteLock.EnterWriteLock();

            try
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');
                        Student currentStudent = _studentParser.Parse(values);
                        if (currentStudent.Id != id)
                        {
                            newCsvFileInfo += currentStudent.ToString() + "\r\n";
                        }
                    }
                }

                using (FileStream fileStream = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    lock (fileStream)
                    {
                        fileStream.SetLength(0);
                    }
                }

                using (StreamWriter writer = new StreamWriter(FilePath, true))
                {
                    writer.Write(newCsvFileInfo);
                }
            }
            catch (Exception ex)
            {
                // log something here
                throw ex;
            }
            finally
            {
                _readWriteLock.ExitWriteLock();
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
            //TODO: there must be a better way to use multiple conditions and do not reassign the list for every condition, maybe where.where not sure about performance
            List<Student> students = this.LoadStudents();
            foreach (Func<Student, bool> condition in conditions)
            {
                students = students.Where(condition).ToList();
            }

            students = students.OrderByDescending(sort).ToList();

            return students;
        }

        /// <summary>
        /// Get all students from source file, evrything call needs to get all items, in other source type like database the query parameters can go into the query string directly
        /// </summary>
        /// <returns></returns>
        private List<Student> LoadStudents()
        {
            List<Student> students = new List<Student>();
            _readWriteLock.EnterWriteLock();
            try
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');
                        students.Add(_studentParser.Parse(values));
                    }
                }
            }
            catch (Exception ex)
            {
                // log something here
                throw ex;
            }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }
            return students;
        }
    }
}
