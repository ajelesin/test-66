using System.Linq;
using System.Reflection;
using System.Text;

namespace test1
{
    public class ClassInfo
    {
        public TypeInfo Class { get; set; }
        public MethodInfo[] Methods { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder();

            if (Class != null)
            {
                str.AppendLine($"- {Class.FullName}");
            }

            Methods?
                .Select(o => "  - {Class?.FullName}.{o?.Name}")
                .ForEach(o => str.AppendLine(o));

            return str.ToString();
        }
    }
}
