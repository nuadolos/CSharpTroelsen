using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.LINQ
{
    internal class TrackList
    {
        private bool isPause;

        public List<Song> ListSongs { get; set; }

        public TrackList()
        {
            isPause = false;

            ListSongs = new List<Song>()
            {
                new Song(),
                new Song(1, "dance alone ft. caeisdead", 152, "Альтернатива"),
                new Song(2, "Just for me", 183, "Рэп"),
                new Song(3, "Revenge", 99, "Хип-хоп"),
                new Song(4, "goodbye my love.", 215, "Альтернатива"),
                new Song(5, "Buzzing", 285, "Хип-хоп"),
                new Song(6, "Happy Life", 175, "Рэп"),
                new Song(7, "moonlight.", 124, "Хип-хоп")
            };
        }

        public void Play()
        {
            Console.WriteLine("Плейлист включен");
        }

        public void Pause()
        {
            if (!isPause)
            {
                Console.WriteLine("Плейлист на паузе!");
                isPause = true;
            }
            else
            {
                Console.WriteLine("Плейлист снова играет!");
                isPause = false;
            }
        }

        public void Stop()
        {
            Console.WriteLine("Плейлист выключен");
        }
    }
}
