using System.Reflection;

namespace Reflection.Tasks
{
    /// <summary>
    /// Provides utility methods to inspect and log metadata from .NET assemblies.
    /// </summary>
    public static class AssemblyInspector
    {
        /// <summary>
        /// Inspects the specified assembly file and prints its public and private metadata to the console.
        /// </summary>
        /// <param name="assemblyPath">The full file system path to the target assembly (.dll or .exe).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="assemblyPath"/> is null or empty.</exception>
        internal static void InspectAssembly(string assemblyPath)
        {
            if (string.IsNullOrWhiteSpace(assemblyPath))
            {
                throw new ArgumentException("Assembly path cannot be null, empty, or white space.", nameof(assemblyPath));
            }

            try
            {
                Assembly assembly = Assembly.LoadFile(assemblyPath);

                Console.WriteLine($"=== Inspecting Assembly: {assembly.FullName} ===\n");

                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    PrintTypeMetadata(type);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to load or inspect assembly at '{assemblyPath}': {ex.Message}");
            }
        }

        /// <summary>
        /// Extracts and prints comprehensive member metadata for a single <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type definition to inspect.</param>
        private static void PrintTypeMetadata(Type type)
        {
            Console.WriteLine($"Type: {type.FullName} ({type.Attributes})");
            Console.WriteLine(new string('-', 40));
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            FieldInfo[] fields = type.GetFields(flags);
            if (fields.Length > 0)
            {
                Console.WriteLine("  Fields:");
                foreach (FieldInfo field in fields)
                {
                    Console.WriteLine($"    - {field.FieldType.Name} {field.Name}");
                }
            }

            PropertyInfo[] properties = type.GetProperties(flags);
            if (properties.Length > 0)
            {
                Console.WriteLine("  Properties:");
                foreach (PropertyInfo prop in properties)
                {
                    Console.WriteLine($"    - {prop.PropertyType.Name} {prop.Name} (CanRead: {prop.CanRead}, CanWrite: {prop.CanWrite})");
                }
            }

            MethodInfo[] methods = type.GetMethods(flags | BindingFlags.DeclaredOnly);
            if (methods.Length > 0)
            {
                Console.WriteLine("  Methods:");
                foreach (MethodInfo method in methods)
                {
                    Console.WriteLine($"    - {method.ReturnType.Name} {method.Name}()");
                }
            }

            EventInfo[] events = type.GetEvents(flags);
            if (events.Length > 0)
            {
                Console.WriteLine("  Events:");
                foreach (EventInfo ev in events)
                {
                    Console.WriteLine($"    - Event: {ev.EventHandlerType?.Name} {ev.Name}");
                }
            }

            Console.WriteLine();
        }
    }
}
