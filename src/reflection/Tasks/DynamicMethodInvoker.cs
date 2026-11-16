using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Tasks
{
    /// <summary>
    /// Dynamically invokes method during runtime
    /// </summary>
    public static class DynamicMethodInvoker
    {
        /// <summary>Finds a method by name and executes it dynamically on the target object.</summary>
        /// <param name="obj">The target object instance.</param>
        /// <param name="methodName">The name of the method to call.</param>
        /// <param name="parameters">Optional parameters required by the method.</param>
        internal static void InvokeMethod(object obj, string methodName, params object[] parameters)
        {
            if (obj == null || string.IsNullOrWhiteSpace(methodName))
            {
                Console.Error.WriteLine("[Error] Target object or method name cannot be null.");
                return;
            }

            Type type = obj.GetType();
            MethodInfo? method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase);
            if (method == null)
            {
                Console.Error.WriteLine($"[Error] Method '{methodName}' not found on type {type.Name}.");
                return;
            }

            try
            {
                object? result = method.Invoke(obj, parameters.Length == 0 ? null : parameters);
                if (method.ReturnType != typeof(void))
                {
                    Console.WriteLine($"[Success] Method '{method.Name}' returned: {result ?? "null"}");
                }
                else
                {
                    Console.WriteLine($"[Success] Method '{method.Name}' executed successfully.");
                }
            }
            catch (TargetInvocationException tie)
            {
                Console.Error.WriteLine($"[Runtime Error] Exception inside '{method.Name}': {tie.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to invoke method '{method.Name}': {ex.Message}");
            }
        }
    }
}
