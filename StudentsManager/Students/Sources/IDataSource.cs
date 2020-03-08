using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Sources
{
    /// <summary>
    /// Interface for sources of data
    /// </summary>
    public interface IDataSource
    {
        List<Student> GetStudents();

        List<Student> GetStudents(Func<Student, bool> condition);

        List<Student> GetStudents(Func<Student, bool> condition, Func<Student, bool> sort);
    }
}
