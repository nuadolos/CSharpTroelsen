using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.LINQ
{
    internal class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlaybackTime { get; set; }
        public string Genre { get; set; }

        public Song() : this(0, "", 0, "")
        {

        }

        public Song(int id, string name, int playbackTime, string genre)
        {
            Id = id;
            Name = name;
            PlaybackTime = playbackTime;
            Genre = genre;
        }
    }
}
