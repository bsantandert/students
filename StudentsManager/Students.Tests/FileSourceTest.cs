using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Students.Sources;
using Students.Models;
using Students.Enums;
using System.IO;
using System.Threading;

namespace Students.Tests
{
    /// <summary>
    /// Summary description for FileSourceTest
    /// </summary>
    [TestClass]
    public class FileSourceTest
    {
        private FileSource inputSource;
        private FileSource outputSource;
        private FileSource inputDeleteSource;
        private FileSource inputThreadsSource;
        private string inputFilePath;
        private string inputDeleteFilePath;        
        private string outputFilePath;
        private string inputThreadsFilePath;

        public FileSourceTest()
        {
        }

        /// <summary>
        /// Initialize files configuration
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            inputFilePath = @"input.csv";
            inputSource = new FileSource(inputFilePath);
            outputFilePath = @"output.csv";
            outputSource = new FileSource(outputFilePath);
            inputDeleteFilePath = @"inputDelete.csv";
            inputDeleteSource = new FileSource(inputDeleteFilePath);
            inputThreadsFilePath = @"inputThreads.csv";
            inputThreadsSource = new FileSource(inputThreadsFilePath);
        }

        /// <summary>
        /// Deletes files with students added
        /// </summary>
        [TestCleanup]
        public void Clean()
        {
            DeleteFile(outputFilePath);
            DeleteFile(inputThreadsFilePath);
        }


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void AddStudent()
        {
            Student newStudent = new Student("123", "Brandon", StudentType.University, Gender.Male, DateTime.Now.AddYears(-18), DateTime.Now, DateTime.Now);
            outputSource.AddStudent(newStudent);
            var testc = outputSource.GetStudents(x => x.Name == "Brandon").Count;
            Assert.AreEqual(1, outputSource.GetStudents(x => x.Name == "Brandon").Count);
        }


        [TestMethod]
        public void DeleteStudent()
        {
            inputDeleteSource.DeleteStudent("1");
            Assert.AreEqual(10, inputDeleteSource.GetStudents().Count);
        }

        [TestMethod]
        public void GetStudents()
        {
            List<Student> students = inputSource.GetStudents();

            Assert.AreEqual(11, students.Count);
        }

        [TestMethod]
        public void GetStudents_Condition()
        {
            List<Student> students = inputSource.GetStudents(s => s.Name == "Leia");

            Assert.AreEqual(2, students.Count);
            Assert.AreEqual(Gender.Female, students[0].Gender);
        }

        /// <summary>
        /// Made this test to check if getting properties from a query string we could send the condition
        /// </summary>
        [TestMethod]
        public void GetStudents_ConditionWithOnlyString()
        {
            List<Student> students = inputSource.GetStudents(s => s.GetType().GetProperty("Name").GetValue(s, null).ToString() == "Leia");

            Assert.AreEqual(2, students.Count);
            Assert.AreEqual(Gender.Female, students[0].Gender);
        }

        [TestMethod]
        public void GetStudents_ByStudentTypeSortedLastModified()
        {
            List<Student> students = inputSource.GetStudents(s => s.Type == StudentType.Kinder, x => x.LastModifiedDate);

            Assert.AreEqual(3, students.Count);
            Assert.AreEqual("10/20/2014 2:59:34 PM", students[0].LastModifiedDate.ToString());
        }

        [TestMethod]
        public void GetStudents_ByNameSorted()
        {
            List<Student> students = inputSource.GetStudents(s => s.Name == "Leia", x => x.Name);

            Assert.AreEqual(2, students.Count);
            Assert.AreEqual(Gender.Female, students[0].Gender);
        }

        [TestMethod]
        public void GetStudents_ByGenderAndTypeSortedLastModified()
        {
            List<Func<Student, bool>> whereExpressions = new List<Func<Student, bool>>();
            whereExpressions.Add(s => s.Gender == Gender.Female);
            whereExpressions.Add(s => s.Type == StudentType.Elementary);
            List<Student> students = inputSource.GetStudents(whereExpressions, x => x.LastModifiedDate);

            Assert.AreEqual(1, students.Count);
            Assert.AreEqual(Gender.Female, students[0].Gender);
        }

        [TestMethod]
        public void AddStudents_MultipleThreads()
        {
            Thread[] threadsArray = new Thread[20];
            int numberOfStudents = 2500;            

            for (int index = 0; index < threadsArray.Length; index++)
            {
                threadsArray[index] = new Thread(AddStudents);
                threadsArray[index].Start(numberOfStudents);
            }

            for (int index = 0; index < threadsArray.Length; index++)
            {
                threadsArray[index].Join();
            }

            List<Student> students = inputThreadsSource.GetStudents();

            Assert.AreEqual(50000, students.Count);
        }

        /// <summary>
        /// Add multiple students to file
        /// </summary>
        /// <param name="number"></param>
        private void AddStudents(object number)
        {
            int numberParameter = (int)number;
            for (int index = 0; index < numberParameter; index++)
            {
                Student newStudent = new Student(index.ToString(), "Brandon", Enums.StudentType.Kinder, Enums.Gender.Male, DateTime.Now.AddYears(-18), DateTime.Now, DateTime.Now);
                inputThreadsSource.AddStudent(newStudent);
            }
        }

        /// <summary>
        /// Delete output file for add schedules varification 
        /// </summary>
        private void DeleteFile(string filePath)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
