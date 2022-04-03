using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.LINQ
{
    internal class LINQExample
    {
        public static void ExampleLINQ()
        {
            TrackList tracks = new TrackList();

            IEnumerable<Song> songQuary = from s in tracks.ListSongs
                                          where s.PlaybackTime > 150
                                          select s;

            Console.WriteLine("Треки, играющее больше 150 секунд:");
            foreach (Song s in songQuary)
            {
                TimeSpan dt = new TimeSpan(0, 0, s.PlaybackTime);
                Console.WriteLine($"{s.Name} {dt}");
            }

            IEnumerable<Song> songsQuary2 = from s in tracks.ListSongs
                                            where s.Genre == "Хип-хоп"
                                            orderby s.Id descending
                                            select s;
            Console.WriteLine("Треки с жанром \"Хип-хоп\", сортированные в обратном порядке:");
            foreach (Song s in songsQuary2)
            {
                TimeSpan dt = new TimeSpan(0, 0, s.PlaybackTime);
                Console.WriteLine($"{s.Name} {dt}");
            }
        }

        public static void ExampleLinqOfType()
        {
            System.Collections.ArrayList arraySong = new System.Collections.ArrayList()
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

            //Трансформация необобщенного массива в тип, совместимый с IEnumerable<T>
            var transformIEnumerable = arraySong.OfType<Song>();

            IEnumerable<Song> quarySong = from s in transformIEnumerable
                                          where s.Genre == "Рэп" && s.PlaybackTime < 180
                                          select s;

            Console.WriteLine("Треки с жанром Рэп, играющие меньше 180 секунд:");
            foreach (Song s in quarySong)
            {
                TimeSpan dt = new TimeSpan(0, 0, s.PlaybackTime);
                Console.WriteLine($"{s.Name} {dt}");
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Анонимный объект:");

            Array obj = GetQuaryArray(arraySong);

            foreach (object s in obj)
            {
                Console.WriteLine(s);
            }
        }

        static Array GetQuaryArray(System.Collections.ArrayList array)
        {
            var quaryArray = from a in array.OfType<Song>()
                             where a.Genre == "Альтернатива"
                             select new { a.Name, a.PlaybackTime};

            //Передача аннонимного набора объектов посредственно на объект Array
            return quaryArray.ToArray();
        }
    }
}
