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
    /// Dynamically constructs types at runtime using reflection emit.
    /// </summary>
    public static class DynamicTypeBuilder
    {
        /// <summary>
        /// Builds a custom "DynamicUser" type with a dynamic property and method.
        /// </summary>
        /// <returns>The created Type definition, or null if creation fails.</returns>
        public static Type? BuildCustomType()
        {
            AssemblyName assemblyName = new AssemblyName("DynamicAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");
            TypeBuilder typeBuilder = moduleBuilder.DefineType(
                "Reflection.DynamicUser",
                TypeAttributes.Public | TypeAttributes.Class);
            FieldBuilder fieldBuilder = typeBuilder.DefineField("_customStatus", typeof(string), FieldAttributes.Private);
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(
                "CustomStatus",
                PropertyAttributes.HasDefault,
                typeof(string),
                null);

            MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            MethodBuilder getMethodBuilder = typeBuilder.DefineMethod("get_CustomStatus", getSetAttr, typeof(string), Type.EmptyTypes);
            ILGenerator getIL = getMethodBuilder.GetILGenerator();
            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, fieldBuilder);
            getIL.Emit(OpCodes.Ret);
            MethodBuilder setMethodBuilder = typeBuilder.DefineMethod("set_CustomStatus", getSetAttr, null, new[] { typeof(string) });
            ILGenerator setIL = setMethodBuilder.GetILGenerator();
            setIL.Emit(OpCodes.Ldarg_0);
            setIL.Emit(OpCodes.Ldarg_1);
            setIL.Emit(OpCodes.Stfld, fieldBuilder);
            setIL.Emit(OpCodes.Ret);
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("PrintStatus", MethodAttributes.Public, null, Type.EmptyTypes);
            ILGenerator methodIL = methodBuilder.GetILGenerator();
            methodIL.Emit(OpCodes.Ldstr, "Dynamic Status: ");
            methodIL.Emit(OpCodes.Ldarg_0);
            methodIL.Emit(OpCodes.Call, getMethodBuilder);
            MethodInfo concatMethod = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) })
                ?? throw new InvalidOperationException("Failed to find string.Concat method through reflection.");

            MethodInfo writeLineMethod = typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })
                ?? throw new InvalidOperationException("Failed to find Console.WriteLine method through reflection.");

            methodIL.Emit(OpCodes.Call, concatMethod);
            methodIL.Emit(OpCodes.Call, writeLineMethod);
            methodIL.Emit(OpCodes.Ret);

            return typeBuilder.CreateType();
        }
    }
}
