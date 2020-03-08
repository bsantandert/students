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
    public class Organization : Entity
    {
        private string _name;

        private string _description;

        private List<Student> _students;

        public Organization() : base()
        {
            _students = new List<Student>();
        }

        public Organization(string id, string name, string description, DateTime createdDate, DateTime lastModifiedDate)
            : base(id, createdDate, lastModifiedDate)
        {
            Name = name;
            Description = description;
            _students = new List<Student>();
        }

        public List<Student> GetStudents()
        {
            return _students;
        }

        public void AssignStudents(List<Student> students)
        {
            _students = students;
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
    }
}
