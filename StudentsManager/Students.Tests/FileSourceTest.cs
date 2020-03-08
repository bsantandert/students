using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Students.Sources;
using Students.Models;
using Students.Enums;

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
        private string inputFilePath;
        private string outputFilePath;

        public FileSourceTest()
        {
        }

        [TestInitialize]
        public void Setup()
        {
            inputFilePath = @"input.csv";
            inputSource = new FileSource(inputFilePath);
            outputFilePath = @"output.csv";
            outputSource = new FileSource(outputFilePath);
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AddStudent()
        {
            Student newStudent = new Student("123", "Brandon", StudentType.University, Gender.Male, DateTime.Now.AddYears(-18), DateTime.Now, DateTime.Now);
            outputSource.AddStudent(newStudent);
            var test = outputSource.GetStudents();
            Assert.AreNotEqual(0, outputSource.GetStudents(x=>x.Name == "Brandon").Count);
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

    }
}
