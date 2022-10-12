using System.Reflection;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace ConsoleAppReflection
{
    public class ReflectionPrinter
    {
        public static void PrintClass(Type classType)
        {
            var methods = classType.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            var fields = classType.GetFields(BindingFlags.Static | BindingFlags.NonPublic);

            Console.WriteLine("\t\t\t Class type \t\t\t");
            Console.WriteLine(classType.Name);

            Console.WriteLine("\t\t\t Fields \t\t\t");
            PrintFields(fields);

            Console.WriteLine();
            Console.WriteLine("\t\t\t Methods \t\t\t");
            PrintMethods(methods);

            Console.ForegroundColor = ConsoleColor.Gray;
        }


        private static void PrintFields(FieldInfo[] fields)
        {
            foreach (FieldInfo field in fields)
            {
                // field type
                Print(field.FieldType.Name, ConsoleColor.Blue);
                
                // field name
                Console.Write($" {field.Name} = ");
                
                // field value
                var value = field.GetValue(field.Name);
                if (value is string)
                {
                    Print($"\"{value}\"", ConsoleColor.Red);
                }
                else
                {
                    Console.Write(value);
                }

                Console.WriteLine();
            }
        }

        private static void PrintMethods(MethodInfo[] methods)
        {
            foreach (var method in methods)
            {
                // method return type
                Print(method.ReturnType.Name, ConsoleColor.Blue);
                Console.Write(" ");
                
                // method name
                Print(method.Name, ConsoleColor.Yellow);

                // method args
                var methodParameters = method.GetParameters(); ;
                Console.Write("(");
                for (int i = 0; i < methodParameters.Length; i++)
                {
                    var methodParameter = methodParameters[i];
                    
                    // method arg type
                    Console.Write(methodParameter.ParameterType.Name);
                    Console.Write(" ");

                    // method arg name
                    Print(methodParameter.Name, ConsoleColor.Cyan);

                    if (i != methodParameters.Length - 1)
                    {
                        Console.Write(", ");
                    }
                }

                Console.WriteLine(")");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void Print(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
