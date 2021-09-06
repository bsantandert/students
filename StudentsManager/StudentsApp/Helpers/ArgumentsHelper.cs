using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApp.Helpers
{
    /// <summary>
    /// Arguments helper
    /// </summary>
    public static class ArgumentsHelper
    {
        /// <summary>
        /// Retrieves arguments from array of strings and returns a dictionary
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetArguments(string[] parameters)
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
