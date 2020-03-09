using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApp.Helpers
{
    public class QueryHelper
    {
        public static List<Func<Student, bool>> GetConditions(Dictionary<string, string> arguments)
        {
            List<Func<Student, bool>> conditions = new List<Func<Student, bool>>();
            foreach (KeyValuePair<string, string> currentArgument in arguments)
            {
                string propertyKey = currentArgument.Key;
                string value = currentArgument.Value;
                Func<Student, bool> condition = s => s.GetType().GetProperty(propertyKey).GetValue(s, null).ToString() == value;
                conditions.Add(condition);
            }
            return conditions;
        }
    }
}
