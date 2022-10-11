using System.Reflection;

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
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(field.FieldType.Name);
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(" ");
                Console.Write(field.Name);

                Console.Write(" = ");

                var value = field.GetValue(field.Name);
                if (value is string)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('\"');
                    Console.Write(value);
                    Console.Write('\"');
                }
                else
                {
                    Console.Write(value);
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private static void PrintMethods(MethodInfo[] methods)
        {
            foreach (var method in methods)
            {
                // return type
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(method.ReturnType.Name);
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(" ");

                // method name
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(method.Name);
                Console.ForegroundColor = ConsoleColor.Gray;

                // args
                var methodParameters = method.GetParameters(); ;
                Console.Write("(");
                for (int i = 0; i < methodParameters.Length; i++)
                {
                    var methodParameter = methodParameters[i];
                    Console.Write(methodParameter.ParameterType.Name);
                    Console.Write(" ");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(methodParameter.Name);

                    Console.ForegroundColor = ConsoleColor.Gray;

                    // add ',' if arg is not last
                    if (i != methodParameters.Length - 1)
                    {
                        Console.Write(", ");
                    }
                }

                Console.WriteLine(")");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
