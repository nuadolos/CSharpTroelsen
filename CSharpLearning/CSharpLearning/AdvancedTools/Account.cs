using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.AdvancedTools
{
    //Выявление атрибутов TestAttribute во время выполнения с помощью рефлексии
    [Attribute.TestAttribute(AssemblyName = "SkillBoxCourseCSharp.AdvancedTools",
            Description = "Данный метод ExampleCustomAttribute предоставляет понять пользу атрибутов")]
    internal class Account
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Account() : this("Bubu", "Bibi")
        { }

        public Account(string login, string password)
        {
            Login = login;
            Password = password;
        }

        /// <summary>
        /// Явное преобразование Account в User.
        /// Неявное преобразование недопустимо.
        /// </summary>
        /// <param name="acc"></param>
        public static explicit operator User(Account acc)
        {
            return new User() { Login = acc.Login, Password = acc.Password};
        }

        /// <summary>
        /// Неявное преобразование User в Account.
        /// Использование явного преобразования допустимо.
        /// </summary>
        /// <param name="user"></param>
        public static implicit operator Account(User user)
        {
            return new Account(user.Login, user.Password);
        }

        /// <summary>
        /// Явное преобразование string в Account.
        /// Неявное преобразование недопустимо.
        /// </summary>
        /// <param name="password"></param>
        public static explicit operator Account(string password)
        {
            return new Account() { Password = password};
        }

        /// <summary>
        /// Неявное преобразование Account в string.
        /// Использование явного преобразования допустимо.
        /// </summary>
        /// <param name="acc"></param>
        public static implicit operator string(Account acc)
        {
            return acc.Login;
        }
    }
}
