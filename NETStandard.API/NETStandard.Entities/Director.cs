using System;
using System.Collections.Generic;
using System.Text;

namespace NETStandard.Entities
{
    public class Director
    {
        public Director()
        {
            Movies = new HashSet<Movie>();
        }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
