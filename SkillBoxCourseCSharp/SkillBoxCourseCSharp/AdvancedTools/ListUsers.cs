using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.AdvancedTools
{
    internal class ListUsers : IEnumerable
    {
        private List<User> lUsers;

        public User this[int index]
        {
            get => lUsers[index];
        }
        public int Count => lUsers.Count;

        public ListUsers()
        {
            lUsers = new List<User>()
            {
                new User(),
                new User("Андрюша221", "ogds52", 100, 1000),
                new User("Владим400", "oggds52", 500, 500),
                new User("Крутой2003", "o42152", 100, 50),
                new User("ПерецНаРезной", "o42152", 150, 0),
                new User("ЯшаЛава", "o42152", 120, 1500),
                new User("ЯшаЛава", "o42152", 120, 1500),
                new User("ЯНеГребу", "o42152", 1020, 2500)
            };
        }

        public IEnumerator GetEnumerator()
        {
            return lUsers.GetEnumerator();
        }
    }
}
