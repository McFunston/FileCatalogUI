using HamburgerUI.Services;
using System;
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
        public List<File> Files { get; set; }

        private int count;        
        public int Count
        {            
            get { return count; }  
            set { count = value; }          
        }

        private DateTimeOffset dateCreated;

        public DateTimeOffset DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
        
        public Archive()
        {

        }

        public Archive(string name, List<File> files)
        {
            Name = name;
            DateCreated = DateTimeOffset.Now;
            Files = files;
            if (Files != null) Count = Files.Count;            
        }

    }
}
