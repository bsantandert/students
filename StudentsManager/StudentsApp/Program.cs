using Students.Models;
using Students.Sources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("                 -- Students App --              ");

            if (args.Length != 0)
            {
                string filePath = args[0];
                if(!File.Exists(filePath))
                {
                    Console.WriteLine("The file specified as source does not exist.");
                }
                else
                {
                    Dictionary<string, string> arguments = GetSearchArguments(args);
                    Organization organization = new Organization("123", "Test Organization", "Organization to test app", DateTime.Now, DateTime.Now);
                    IDataSource source = new FileSource(filePath);
                    List<Func<Student, bool>> conditions = new List<Func<Student, bool>>();
                    foreach (KeyValuePair<string, string> currentArgument in arguments)
                    {
                        string propertyKey = currentArgument.Key;
                        string value = currentArgument.Value;
                        Func<Student, bool> condition = s => s.GetType().GetProperty(propertyKey).GetValue(s, null).ToString() == value;
                        conditions.Add(condition);
                    }

                    List<Student> students = source.GetStudents(conditions, x => x.LastModifiedDate);

                    foreach (Student currentStudent in students)
                    {
                        Console.WriteLine($"{currentStudent.Name} : {currentStudent.LastModifiedDate}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Please provide the app with source file path, E.g. Students.exe input.csv");
            }
            Console.ReadKey();
        }

        public static Dictionary<string, string> GetSearchArguments(string[] parameters)
        {
            Dictionary<string, string> parametersFound = new Dictionary<string, string>();
            for (int i = 1; i < parameters.Length; i++)
            {
                string[] param = parameters[i].Split('=');
                string propertyKey = param[0];
                string value = param[1];
                parametersFound.Add(propertyKey, value);
            }
            return parametersFound;
        }
    }
}
