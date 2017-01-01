﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Models
{
    public sealed class Catalog
    {
        private static Catalog cat = new Catalog();

        private Catalog()
        {
            FileSet = new Dictionary<Archive, ICollection<IFile>>();                       
        }
        
        public static Catalog Cat
        {
            get
            {
                return cat;
            }
            //set
            //{
            //    cat = value;
            //}
        }

        public Dictionary<Archive, ICollection<IFile>> FileSet { get; set; }
                     
        //public Catalog(Archive archive, ICollection<IFile> files)
        //{
        //    FileSet.Add(archive, files);
        //}
        
    }
}
