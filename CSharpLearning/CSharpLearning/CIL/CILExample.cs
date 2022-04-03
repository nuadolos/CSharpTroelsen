using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.CIL
{
    internal class CILExample
    {
        public static void ExampeCILCode()
        {
            Assembly testCILCode = Assembly.LoadFrom(@"C:\Users\nuadolos\Desktop\TestCIL\CILAssemby\TestCILAssembly.dll");
            Reflection.ReflectionExample.DisplayTypesInAsm(testCILCode);
            Reflection.ReflectionExample.DisplayOptionalDetails(testCILCode.GetType("TestCILAssembly.Lesson.Teacher"));
        }

        public static void ExampleCreateAssembly()
        {
            //Общие характеристики новой сборки
            AssemblyName asmName = new AssemblyName();
            Console.Write("Имя сборки: ");
            asmName.Name = Console.ReadLine();
            Console.Write("Версия: ");
            asmName.Version = new Version(Console.ReadLine());

            string name = asmName.Name;

            //Создание новой сборки внутри текущего домена приложения
            AssemblyBuilder createAssebly = AppDomain.CurrentDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Save);

            //Определение модуля однофайловой сборки
            ModuleBuilder module = createAssebly.DefineDynamicModule(name, $"{name}.dll");

            //Определение открытого класса HelloWorld
            TypeBuilder helloWorldClass = module.DefineType($"{name}.HelloWorld", TypeAttributes.Public);

            //Определение закрытого поля message типа string
            FieldBuilder msgField = helloWorldClass.DefineField("message", Type.GetType("System.String"), FieldAttributes.Private);

            //Создание специального конструктора с параметром типа string
            Type[] paramsCtor = new Type[] { typeof(string) };
            ConstructorBuilder ctor = helloWorldClass.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, paramsCtor);
            ILGenerator ctorIL = ctor.GetILGenerator();
            ctorIL.Emit(OpCodes.Ldarg_0);

            //Вызов конструктора базового класса Object
            Type objClass = typeof(object);
            ConstructorInfo ctorObj = objClass.GetConstructor(new Type[0]);

            ctorIL.Emit(OpCodes.Call, ctorObj);
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Ldarg_1);
            ctorIL.Emit(OpCodes.Stfld, msgField);
            ctorIL.Emit(OpCodes.Ret);

            //Создание стандартного конструктора
            helloWorldClass.DefineDefaultConstructor(MethodAttributes.Public);

            //Создание метода GetMsg()
            MethodBuilder getMsgMethod = helloWorldClass.DefineMethod("GetMsg", MethodAttributes.Public, typeof(string), null);
            ILGenerator methodIL = getMsgMethod.GetILGenerator();
            methodIL.Emit(OpCodes.Ldarg_0);
            methodIL.Emit(OpCodes.Ldfld, msgField);
            methodIL.Emit(OpCodes.Ret);

            //Создание метода SayHello()
            MethodBuilder sayHiMethod = helloWorldClass.DefineMethod("SayHello", MethodAttributes.Public, null, null);
            methodIL = sayHiMethod.GetILGenerator();
            methodIL.EmitWriteLine("Hello from the HelloWorld class!");
            methodIL.Emit(OpCodes.Ret);

            //Завершение работ с классом HelloWorld
            helloWorldClass.CreateType();

            //Сохранение сборки в файле
            createAssebly.Save($"{name}.dll");

            Reflection.ReflectionExample.DisplayTypesInAsm(createAssebly);
            Reflection.ReflectionExample.DisplayOptionalDetails(createAssebly.GetType($"{name}.HelloWorld"));

            Console.Write("\n\n\tMethod SayHello: ");
            Type asmType = Assembly.Load(name).GetType($"{name}.HelloWorld");
            dynamic MethodView = Activator.CreateInstance(asmType);
            MethodView.SayHello();
        }
    }
}
