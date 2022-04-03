using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//Атрибут CLSCompliant применен на уровне сборки или модуля
[assembly: CLSCompliant(true)]
namespace SkillBoxCourseCSharp.Attribute
{
    [TestAttribute("SkillBoxCourseCSharp.Attribute",
        "Данный класс AttributeExample предоставляет понять пользу атрибутов")]
    public class AttributeExample
    {
        /// <summary>
        /// Метод для примера атрибута Obsolete, принимающий два параметра:
        /// 1) Сообщение, которое выводится при ошибки компиляции или как предупреждение
        /// 2) True - применение метода влечет ошибку компиляции
        ///    False - применение метода влечет возникновение предупреждения
        /// </summary>
        [ObsoleteAttribute("Пример работы атрибута Obsolete", true)]
        [TestAttribute("SkillBoxCourseCSharp.Attribute",
            "Данный метод ExampleObsolete предоставляет понять пользу атрибутов")]
        public static void ExampleObsolete()
        {
            Console.WriteLine("Метод успешно работает");
        }

        /// <summary>
        /// Демострация работы рефлексии, при которой атрибут находится в текущей сборке
        /// </summary>
        [TestAttribute(AssemblyName = "SkillBoxCourseCSharp.Attribute",
            Description = "Данный метод ExampleCustomAttribute предоставляет понять пользу атрибутов")]
        public static void ExampleReflectionCustomAttribute()
        {
            Type t = typeof(AttributeExample);

            object[] customAttsClass = t.GetCustomAttributes(false);

            foreach (TestAttribute atts in customAttsClass)
            {
                Console.WriteLine($"\nПространство имен типа: {atts.AssemblyName}\nОписание атрибута: {atts.Description}");
            }

            object[] customAttsMethod = t.GetMethod("ExampleObsolete").GetCustomAttributes(false);

            foreach (var atts in customAttsMethod)
            {
                if (atts is TestAttribute testAtts)
                    Console.WriteLine($"\nПространство имен типа: {testAtts.AssemblyName}\nОписание атрибута: {testAtts.Description}");
            }
        }

        /// <summary>
        /// Демострация работы позднего связывания, то есть сборка не имеет ссылку на атрибут
        /// </summary>
        public static void ExampleLateBindingCustomAttribute()
        {
            //Загрузка сборки SkillBoxCourseCSharp
            Assembly asm = Assembly.Load("SkillBoxCourseCSharp");

            //Получение информации о типе TestAttribute
            Type typeAtt = asm.GetType("SkillBoxCourseCSharp.Attribute.TestAttribute");

            //Получение всех свойств, определяемые в типе TestAttribute
            PropertyInfo[] propInf = typeAtt.GetProperties();

            //Получение всех типов из сборки SkillBoxCourseCSharp
            Type[] typeCustomAttsExample = asm.GetTypes();

            foreach (Type t in typeCustomAttsExample)
            {
                //Получение атрибута TestAttribute, содержущееся в определенном типе сборки SkillBoxCourseCSharp
                object[] objCustomAtts = t.GetCustomAttributes(typeAtt, false);

                if (objCustomAtts != null)
                {
                    //Вывод информации из атрибута TestAttribute с пмощью метода GetValue()
                    foreach (object o in objCustomAtts)
                    {
                        Console.WriteLine($"\n\nТип: {t.Name}\n" +
                            $"Пространство имен: {propInf[0].GetValue(o, null)}\n" +
                            $"Описание атрибута: {propInf[1].GetValue(o, null)}");
                    }
                }
            }
        }

        //Приводит к ошибке компиляции, так как оатрибут может быть применен только к методам или к классам
        //[TestAttribute("SkillBoxCourseCSharp.Attribute", "Данный метод предоставляет понять пользу атрибутов")]
        public enum ExampleEnum { Test1, Test2 }
    }
}
