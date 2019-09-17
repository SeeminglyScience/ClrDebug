using System;

namespace CalliBuilder
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            string projectName = args?[0];
            string path = args?[1];
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            AssemblyBuilder.Create(projectName, path);
        }
    }
}
