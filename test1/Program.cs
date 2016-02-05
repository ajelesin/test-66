using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace test1
{
    class Program
    {
        public IEnumerable<ClassInfo> ExplorerFolder(DirectoryInfo directory)
        {
            var files = directory.GetFiles("*.dll");
            var types = files.SelectMany(GetTypes);
            var methods = types.Select(GetPublicAndProtectedMethods);

            return methods;
        }

        private IEnumerable<TypeInfo> GetTypes(FileInfo file)
        {
            var assembly = Assembly.LoadFile(file.FullName);
            var types = assembly.GetTypes();
            var typesInfo = types.Select(type => type.GetTypeInfo());
            return typesInfo;
        }

        private ClassInfo GetPublicAndProtectedMethods(TypeInfo type)
        {
            var methods = type.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(o => o.IsFamily || o.IsPublic);

            var methodInfos = methods.ToArray();
            return new ClassInfo {Class = type, Methods = methodInfos};
        }


        public static void Main(string[] args)
        {
            try
            {
                var dirName = Console.ReadLine();
                var dirInfo = new DirectoryInfo(dirName);

                var program = new Program();
                var classInfo = program.ExplorerFolder(dirInfo);

                classInfo.ForEach(Console.WriteLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }

            // просто чтобы консолька не закрывалась
            Console.ReadLine();
        }
    }
}
