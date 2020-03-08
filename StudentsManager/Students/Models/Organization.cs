using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Models
{
    /// <summary>
    /// Organization class for students
    /// </summary>
    public class Organization
    {
        private string _name;

        private string _description;

        private DateTime _createdDate;

        private DateTime _lastModifiedDate;

        private List<Student> _students;

        public Organization()
        {
            _students = new List<Student>();
        }

        public Organization(string name, string description, DateTime createdDate, DateTime lastModifiedDate)
        {
            Name = name;
            Description = description;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
            _students = new List<Student>();
        }

        public List<Student> GetStudents()
        {
            return _students;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
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
