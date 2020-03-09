using Students.Models;
using Students.Sources;
using StudentsApp.Helpers;
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
        private static string APP_TITLE = "                     -- Students App --                       ";
        private static string FILE_NOT_FOUND = "The file specified as source does not exist.";
        private static string NO_PARAMETERS = "Please provide the app with source file path, E.g. Students.exe input.csv";

        static void Main(string[] args)
        {
            Console.WriteLine(APP_TITLE);
            try
            {
                if (args.Length != 0)
                {
                    string filePath = args[0];
                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine(FILE_NOT_FOUND);
                    }
                    else
                    {
                        Dictionary<string, string> arguments = ArgumentsHelper.GetArguments(args);
                        List<Func<Student, bool>> conditions = QueryHelper.GetConditions(arguments);

                        IDataSource source = new FileSource(filePath);
                        List<Student> students = source.GetStudents(conditions, x => x.LastModifiedDate);

                        Organization organization = new Organization("123", "Test Organization", "Organization to test app", DateTime.Now, DateTime.Now);
                        organization.AssignStudents(students);

                        foreach (Student currentStudent in organization.GetStudents())
                        {
                            Console.WriteLine(currentStudent.Print());
                        }
                    }
                }
                else
                {
                    Console.WriteLine(NO_PARAMETERS);
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error ocurred white loading application.", ex);
            }
            Console.ReadKey();
        }

    }

}

