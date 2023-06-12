using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeather
{
    public static class DotEnv
    {
        private static string _path = AppDomain.CurrentDomain.BaseDirectory;

        public static string Load(string filePath)
        {
            string _filePath = Path.Combine(_path, filePath);

            Debug.WriteLine(_filePath);

            string output = "";

            if (!File.Exists(filePath))
                return null;

            foreach (var line in File.ReadAllLines(_filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                {
                    continue;
                }

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
                output = parts[1];
            }

            return output;
        }
    }
}
