using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Students.Sources;
using Students.Models;

namespace Students.Tests
{
    /// <summary>
    /// Summary description for FileSourceTest
    /// </summary>
    [TestClass]
    public class FileSourceTest
    {
        private FileSource source;
        private string filePath;

        public FileSourceTest()
        {
        }

        [TestInitialize]
        public void Setup()
        {
            filePath = @"input.csv";
            source = new FileSource(filePath);
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
        public void LoadStudents()
        {
            List<Student> students = source.GetStudents();

            Assert.AreEqual(10, students.Count);
        }
    }
}
