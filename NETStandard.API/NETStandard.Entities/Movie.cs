using System;
using System.Collections.Generic;
using System.Text;

namespace NETStandard.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public override string ToString()
        {
            return $"{Id} - {Title}, {Duration}";
        }
    }
}
