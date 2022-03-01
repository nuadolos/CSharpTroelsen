using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.AdvancedTools
{
    internal class User : IBalance, IComparable<User>
    {
        public delegate void UserHandler(object sender, UserEventArgs e);
        public static event UserHandler CancelSubscriptionFailed;
        public static event UserHandler PaymentFailed;
        
        public string Login { get; set; }
        public string Password { get; set; }
        public int Subscription { get; set; }
        public int Balance { get; set; }

        public User(): this ("Ubob", "psg22", 150, 450)
        { }

        public User(string login, string password, int subscription, int balance)
        {
            Login = login;
            Password = password;
            Subscription = subscription;
            Balance = balance;
        }

        public static User operator +(User us1, int balance) => new User(us1.Login, us1.Password, us1.Subscription, us1.Balance + balance);
        public static User operator -(User us1, int balance) => new User(us1.Login, us1.Password, us1.Subscription, us1.Balance - balance);
        public static bool operator ==(User us1, User us2) => us1.Equals(us2);
        public static bool operator !=(User us1, User us2) => !us1.Equals(us2);
        public static bool operator >(User us1, User us2) => us1.CompareTo(us2) > 0;
        public static bool operator <(User us1, User us2) => us1.CompareTo(us2) < 0;
        public static bool operator >=(User us1, User us2) => us1.CompareTo(us2) >= 0;
        public static bool operator <=(User us1, User us2) => us1.CompareTo(us2) <= 0;

        public override bool Equals(object obj) => this.ToString() == obj.ToString();

        public override int GetHashCode() => this.ToString().GetHashCode();

        public override string ToString() => $"[{Login}, {Password}, {Subscription}, {Balance}]";

        public void CancelSubscription()
        {
            if (Balance - 100 > 0)
            {
                Balance -= 100;
                Subscription = 0;
            }
            else
                CancelSubscriptionFailed?.Invoke(this, new UserEventArgs("Невозможно отменить подписку!"));
        }

        public void Payment(int commission)
        {
            if (Balance - Subscription - commission > 0)
                Balance -= (Subscription + commission);
            else
            {
                PaymentFailed?.Invoke(this, new UserEventArgs("Не хватает денежных средств для оплаты подписки!"));
                Balance = 0;
            }
        }

        public int CompareTo(User secondUser)
        {
            if (this.Balance > secondUser.Balance)
                return 1;
            else if (this.Balance < secondUser.Balance)
                return -1;
            else
                return 0;
        }
    }
}
