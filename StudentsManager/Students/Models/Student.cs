using Students.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Models
{
    /// <summary>
    /// Student class
    /// </summary>
    public class Student
    {
        private string _name;

        private StudentType _type;

        private Gender _gender;

        private DateTime _birthDate;

        private DateTime _createdDate;

        private DateTime _lastModifiedDate;

        public Student() { }

        public Student(string name, StudentType type, Gender gender, DateTime birthDate, DateTime createdDate, DateTime lastModifiedDate)
        {
            Name = name;
            Type = type;
            Gender = gender;
            BirthDate = birthDate;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public StudentType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Gender Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public DateTime LastModifiedDate
        {
            get { return _lastModifiedDate; }
            set { _lastModifiedDate = value; }
        }
    }
}
