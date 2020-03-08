using Students.Enums;
using Students.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Parsers
{
    /// <summary>
    /// Parse class to handle Student objects
    /// </summary>
    public class StudentParser
    {
        private static string DATE_FORMAT = "yyyyMMddHHmmss";
        private static string FEMALE_KEY = "F";
        private static string FEMALE_VALUE = "Female";
        private static string MALE_KEY = "M";
        private static string MALE_VALUE = "Male";
        private static string NOT_DEFINED = "Not Defined";

        /// <summary>
        /// Parses array of string to a Student object
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public Student Parse(string[] values)
        {
            Student parsedStudent = new Student();
            parsedStudent.Type = (StudentType)Enum.Parse(typeof(StudentType), values[0]);
            parsedStudent.Name = values[1];
            string genderValue = values[2] == FEMALE_KEY ? FEMALE_VALUE : values[2] == MALE_KEY ? MALE_VALUE : NOT_DEFINED;
            parsedStudent.Gender = (Gender)Enum.Parse(typeof(Gender), genderValue);
            parsedStudent.LastModifiedDate = DateTime.ParseExact(values[3], DATE_FORMAT, CultureInfo.InvariantCulture);

            return parsedStudent;
        }
    }
}
