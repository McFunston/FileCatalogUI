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
        public IList<IFile> FileSet { get; set; }

        private int count;        
        public int Count
        {            
            get { return count; }            
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

        public Archive(string name)
        {
            Name = name;
            dateCreated = DateTimeOffset.Now;
            if (FileSet != null )  count = FileSet.Count;
        }

        public Archive(string name, IList<IFile> fileSet)
        {
            Name = name;
            dateCreated = DateTimeOffset.Now;
            FileSet = fileSet;
            if (FileSet != null) count = FileSet.Count;            
        }

    }
}
