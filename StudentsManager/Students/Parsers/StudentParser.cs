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

        private static int ID_COLUMN = 0;
        private static int TYPE_COLUMN = 1;
        private static int NAME_COLUMN = 2;
        private static int GENDER_COLUMN = 3;
        private static int LAST_MODIFIED_COLUMN = 4;

        /// <summary>
        /// Parses array of string to a Student object
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public Student Parse(string[] values)
        {
            Student parsedStudent = new Student();
            parsedStudent.Id = values[ID_COLUMN];
            parsedStudent.Type = (StudentType)Enum.Parse(typeof(StudentType), values[TYPE_COLUMN]);
            parsedStudent.Name = values[NAME_COLUMN];
            string genderValue = values[GENDER_COLUMN] == FEMALE_KEY ? FEMALE_VALUE : values[GENDER_COLUMN] == MALE_KEY ? MALE_VALUE : NOT_DEFINED;
            parsedStudent.Gender = (Gender)Enum.Parse(typeof(Gender), genderValue);
            parsedStudent.LastModifiedDate = DateTime.ParseExact(values[LAST_MODIFIED_COLUMN], DATE_FORMAT, CultureInfo.InvariantCulture);

            return parsedStudent;
        }
    }
}
