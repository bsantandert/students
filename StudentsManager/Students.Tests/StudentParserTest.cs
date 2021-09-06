using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Students.Parsers;
using Students.Models;

namespace Students.Tests
{
    /// <summary>
    /// Summary description for StudentParserTest
    /// </summary>
    [TestClass]
    public class StudentParserTest
    {
        private StudentParser parser;

        public StudentParserTest()
        {
        }

        /// <summary>
        /// Initialize parser
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            parser = new StudentParser();
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
        public void Parse()
        {
            string[] values = new string[5];
            values[0] = "123";
            values[1] = "Kinder";
            values[2] = "Leia";
            values[3] = "F";
            values[4] = "20130129080903";

            Student currentStudent = parser.Parse(values);

            Assert.AreEqual(currentStudent.Id, values[0]);
            Assert.AreEqual(currentStudent.Type.ToString(), values[1]);
            Assert.AreEqual(currentStudent.Name, values[2]);
            Assert.AreEqual(currentStudent.Gender.ToString(), "Female");
            Assert.AreEqual(currentStudent.LastModifiedDate.ToString(StudentParser.DATE_FORMAT), values[4]);
        }
    }
}
