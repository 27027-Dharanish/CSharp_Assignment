using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Tasks
{
    /// <summary>
    /// Provides utility methods to dynamically inspect and modify object properties at runtime.
    /// </summary>
    public static class DynamicObjectInspector
    {
        /// <summary>
        /// Inspects the given object, printing all property names and their current values.
        /// </summary>
        /// <param name="obj">The object instance to inspect.</param>
        internal static void InspectObject(object obj)
        {
            if (obj == null)
            {
                Console.WriteLine("Cannot inspect a null object.");
                return;
            }

            Type type = obj.GetType();
            Console.WriteLine($"=== Inspecting Instance of: {type.FullName} ===");
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (properties.Length == 0)
            {
                Console.WriteLine("No properties found on this object.");
                return;
            }

            foreach (PropertyInfo prop in properties)
            {
                try
                {
                    if (prop.CanRead)
                    {
                        object value = prop.GetValue(obj) ?? "null";
                        Console.WriteLine($"  Property: {prop.Name} | Type: {prop.PropertyType.Name} | Current Value: {value}");
                    }
                    else
                    {
                        Console.WriteLine($"  Property: {prop.Name} | Type: {prop.PropertyType.Name} | (Write-Only)");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Property: {prop.Name} | Error reading value: {ex.Message}");
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Accepts a property name and a new value, then sets the property to the new value using SetValue.
        /// </summary>
        /// <param name="obj">The target object instance to modify.</param>
        /// <param name="propertyName">The name of the property to change.</param>
        /// <param name="newValue">The new raw value string or object.</param>
        /// <returns>True if the modification succeeded; false otherwise.</returns>
        internal static bool TrySetPropertyValue(object obj, string propertyName, object newValue)
        {
            if (obj == null || string.IsNullOrWhiteSpace(propertyName))
            {
                Console.Error.WriteLine("[Error] Invalid target object or property name.");
                return false;
            }

            Type type = obj.GetType();
            PropertyInfo? prop = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop == null)
            {
                Console.Error.WriteLine($"[Error] Property '{propertyName}' not found on type {type.Name}.");
                return false;
            }

            if (!prop.CanWrite)
            {
                Console.Error.WriteLine($"[Error] Property '{prop.Name}' is read-only and cannot be updated.");
                return false;
            }

            try
            {
                object convertedValue = newValue;

                if (newValue is string stringValue && prop.PropertyType != typeof(string))
                {
                    convertedValue = Convert.ChangeType(stringValue, prop.PropertyType);
                }

                prop.SetValue(obj, convertedValue);
                Console.WriteLine($"[Success] Property '{prop.Name}' updated to: {convertedValue}");
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to set property '{prop.Name}': {ex.Message}");
                return false;
            }
        }
    }
}
