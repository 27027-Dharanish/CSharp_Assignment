using System.Reflection;
using System.Reflection.Emit;

namespace Reflection.Tasks
{
    /// <summary>
    /// Generates dynamic mock implementations of interfaces at runtime for testing purposes.
    /// </summary>
    public static class DynamicMockBuilder
    {
        /// <summary>
        /// Creates a dynamic implementation type for a specified interface type.
        /// </summary>
        /// <typeparam name="T">The interface type to mock.</typeparam>
        /// <returns>A new Type implementing the target interface.</returns>
        public static Type? CreateMockType<T>()
            where T : class
        {
            Type interfaceType = typeof(T);
            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException($"Type '{interfaceType.FullName}' must be an interface to be mocked.");
            }

            AssemblyName assemblyName = new AssemblyName("DynamicMockAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicMockModule");
            string mockTypeName = $"Mock{interfaceType.Name}";
            TypeBuilder typeBuilder = moduleBuilder.DefineType(mockTypeName, TypeAttributes.Public | TypeAttributes.Class);
            typeBuilder.AddInterfaceImplementation(interfaceType);
            MethodInfo[] methods = interfaceType.GetMethods();
            foreach (MethodInfo method in methods)
            {
                ParameterInfo[] parameters = method.GetParameters();
                Type[] parameterTypes = Array.ConvertAll(parameters, p => p.ParameterType);
                MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot;
                MethodBuilder methodBuilder = typeBuilder.DefineMethod(method.Name, methodAttributes, method.ReturnType, parameterTypes);
                ILGenerator il = methodBuilder.GetILGenerator();
                if (method.ReturnType == typeof(void))
                {
                    il.Emit(OpCodes.Ret);
                }
                else if (method.ReturnType == typeof(bool))
                {
                    il.Emit(OpCodes.Ldc_I4_0);
                    il.Emit(OpCodes.Ret);
                }
                else if (!method.ReturnType.IsValueType)
                {
                    il.Emit(OpCodes.Ldnull);
                    il.Emit(OpCodes.Ret);
                }
                else
                {
                    LocalBuilder local = il.DeclareLocal(method.ReturnType);
                    il.Emit(OpCodes.Ldloca_S, local);
                    il.Emit(OpCodes.Initobj, method.ReturnType);
                    il.Emit(OpCodes.Ldloc, local);
                    il.Emit(OpCodes.Ret);
                }

                typeBuilder.DefineMethodOverride(methodBuilder, method);
            }

            return typeBuilder.CreateType();
        }
    }
}
