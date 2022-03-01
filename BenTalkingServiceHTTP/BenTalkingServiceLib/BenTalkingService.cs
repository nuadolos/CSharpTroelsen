using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenTalkingServiceLib
{
    public class BenTalkingService : IBenTalking
    {
        private string[] answers = { "Yes", "No", "Oauu", "Hou hou hou"};

        public BenTalkingService()
        {
            Console.WriteLine("Бен взял трубку...");
        }

        public string BenAnswerToQuestion(string userQuestion)
        {
            Random rnd = new Random();
            return answers[rnd.Next(answers.Length)];
        }
    }
}
