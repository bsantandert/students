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
    public class Student : Entity
    {
        private string _name;

        private StudentType _type;

        private Gender _gender;

        private DateTime _birthDate;

        public Student() : base() { }

        public Student(string id, string name, StudentType type, Gender gender, DateTime birthDate, DateTime createdDate, DateTime lastModifiedDate) 
            : base(id, createdDate, lastModifiedDate)
        {
            Name = name;
            Type = type;
            Gender = gender;
            BirthDate = birthDate;
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

        public override string ToString()
        {
            return $"{Id},{Type.ToString()},{Name},{Gender.ToString()[0]},{LastModifiedDate.ToString("yyyyMMddHHmmss")}";
        }

        public string Print()
        {
            return $"Name: {Name}, Gender: {Gender.ToString()}, Type: {Type.ToString()}, Modified date: {LastModifiedDate.ToString()}";
        }
    }
}
