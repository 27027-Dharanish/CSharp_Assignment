using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Tasks
{
    /// <summary>
    /// Implements simple serialization via traditional Reflection and high-performance Reflection.Emit.
    /// </summary>
    public static class CustomSerializer
    {
        private static readonly Dictionary<Type, Action<object, StringBuilder>> EmitCache = new();

        /// <summary>
        /// Simple serialization using traditional Reflection.
        /// </summary>
        /// <param name="obj">The target object instance to serialize.</param>
        /// <returns>A flat key-value serialized string representation.</returns>
        public static string SerializeWithReflection(object? obj)
        {
            if (obj == null)
            {
                return "null";
            }

            Type type = obj.GetType();
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in properties)
            {
                if (!prop.CanRead)
                {
                    continue;
                }

                object? value = prop.GetValue(obj);
                string valueStr = value == null ? "null" : value.ToString() ?? "null";

                sb.Append($"{prop.Name}:{valueStr};");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Simple serialization using dynamic methods generated via Reflection.Emit.
        /// </summary>
        /// <param name="obj">The target object instance to serialize.</param>
        /// <returns>A flat key-value serialized string representation.</returns>
        public static string SerializeWithEmit(object? obj)
        {
            if (obj == null)
            {
                return "null";
            }

            Type type = obj.GetType();
            StringBuilder sb = new StringBuilder();

            if (!EmitCache.TryGetValue(type, out var serializer))
            {
                serializer = CompileSerializer(type);
                EmitCache[type] = serializer;
            }

            serializer(obj, sb);
            return sb.ToString();
        }

        private static Action<object, StringBuilder> CompileSerializer(Type type)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(
                $"Serialize_{type.Name}",
                null,
                new[] { typeof(object), typeof(StringBuilder) },
                typeof(CustomSerializer).Module);

            ILGenerator il = dynamicMethod.GetILGenerator();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            MethodInfo appendString = typeof(StringBuilder).GetMethod("Append", new[] { typeof(string) })
                ?? throw new InvalidOperationException("StringBuilder.Append missing.");

            MethodInfo objToString = typeof(object).GetMethod("ToString", Type.EmptyTypes)
                ?? throw new InvalidOperationException("object.ToString missing.");

            foreach (PropertyInfo prop in properties)
            {
                if (!prop.CanRead)
                {
                    continue;
                }

                MethodInfo? getMethod = prop.GetGetMethod();
                if (getMethod == null)
                {
                    continue;
                }

                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldstr, $"{prop.Name}:");
                il.Emit(OpCodes.Callvirt, appendString);
                il.Emit(OpCodes.Pop);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Castclass, type);
                il.Emit(OpCodes.Callvirt, getMethod);

                if (prop.PropertyType.IsValueType)
                {
                    il.Emit(OpCodes.Box, prop.PropertyType);
                }

                Label nullLabel = il.DefineLabel();
                Label endLabel = il.DefineLabel();
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Brfalse_S, nullLabel);
                il.Emit(OpCodes.Callvirt, objToString);
                il.Emit(OpCodes.Br_S, endLabel);
                il.MarkLabel(nullLabel);
                il.Emit(OpCodes.Pop);
                il.Emit(OpCodes.Ldstr, "null");
                il.MarkLabel(endLabel);
                il.Emit(OpCodes.Callvirt, appendString);
                il.Emit(OpCodes.Pop);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldstr, ";");
                il.Emit(OpCodes.Callvirt, appendString);
                il.Emit(OpCodes.Pop);
            }

            il.Emit(OpCodes.Ret);
            return (Action<object, StringBuilder>)dynamicMethod.CreateDelegate(typeof(Action<object, StringBuilder>));
        }
    }
}
