using HamburgerUI.Services;
using System.Collections.Generic;
using System.IO;

namespace HamburgerUI.Models
{

    /// <summary>
    /// This is the Key type for the sorted list pairs of the archive set.
    /// </summary>
    public class Archive
    {
        public int Id { get; set; }
        public string Name { get; set; } //The name of the archive set (eg. "DVD #5")
        
        public Archive()
        {
        }

        public Archive(string name)
        {
            Name = name;
        }


    }
}
