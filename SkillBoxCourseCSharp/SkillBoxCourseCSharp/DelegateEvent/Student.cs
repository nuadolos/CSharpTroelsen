using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.DelegateEvent
{
    //Выявление атрибутов TestAttribute во время выполнения с помощью рефлексии
    [Attribute.TestAttribute(AssemblyName = "SkillBoxCourseCSharp.DelegateEvent",
            Description = "Данный метод ExampleCustomAttribute предоставляет понять пользу атрибутов")]
    public class Student : IBudgetGroup
    {
        /// <summary>
        /// Делегат, неявно создающий запечатанный класс StudentHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void StudentHandler(object sender, StudentEventArgs e);
        /*
         * Данный класс генерируется комплятором
         * 
         * sealed class StudentHandler : System.MulticastDelegate
           {
                public void Invoke (object sender, StudentEventArgs e);
                public IAsyncResult Beginlnvoke(object sender, StudentEventArgs e,
                  AsyncCallback cb, object state);
                public void Endlnvoke(IAsyncResult result);
           }
         * 
        */

        //События используются на уровне класса
        public static event StudentHandler StJoinClub;
        public static event StudentHandler StSuspended;
        public static event StudentHandler StUnSuspended;

        public enum СlubsStudent 
        {
            AnimalHusbandry = 0,
            SewingClub = 1,
            Volleyball = 2,
            Literature = 3
        }

        public string FIO { get; set; }
        public int Age { get; set; }
        public string PlaceOfStudy { get; set; }
        public int Course { get; set; }
        public string Club { get; private set; }
        public bool Suspended { get; private set; }
        public bool BudgetGroup { get; set; }
        public int Payment  { get => BudgetGroup ? 0 : 36000; }
        public int TotalYearsOfStudy { get; set; }

        public static Student operator +(Student st1, Student st2)
        {
            return new Student() { Age = st1.Age + st2.Age};
        }
        public static Student operator ++(Student st1)
        {
            return new Student() { Age = st1.Age++ };
        }

        public Student() : this ("Первокурсник", 16, "г. Москва", 1, false, 3)
        { }

        public Student(string fio, int age, string placeOfStudy, int course, bool budgetGroup, int totalYearsOfStudy)
        {
            FIO = fio;
            Age = age;
            PlaceOfStudy = placeOfStudy;
            Course = course;
            BudgetGroup = budgetGroup;
            TotalYearsOfStudy = totalYearsOfStudy;
        }

        public void JoinClub(int indexClub)
        {
            switch(indexClub)
            {
                case 0:
                    Club = СlubsStudent.AnimalHusbandry.ToString();
                    break;
                case 1:
                    Club = СlubsStudent.SewingClub.ToString();
                    break;
                case 2:
                    Club = СlubsStudent.Volleyball.ToString();
                    break;
                case 3:
                    Club = СlubsStudent.Literature.ToString();
                    break;
            }

            StJoinClub?.Invoke(this, new StudentEventArgs("Студент вступил в клуб!"));
        }

        public int TotalPayment() => Payment != 0 ? Payment * TotalYearsOfStudy : 0;

        public void SuspendFromClasses()
        {
            Suspended = true;
            StSuspended?.Invoke(this, new StudentEventArgs("Студент был отстранен от занятий!"));
        }

        public void UnSuspendFromClasses()
        {
            Suspended = false;
            StUnSuspended?.Invoke(this, new StudentEventArgs("Студент был допущен к занятиям!"));
        }
    }
}
