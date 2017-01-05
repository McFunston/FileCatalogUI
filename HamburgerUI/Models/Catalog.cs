using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;

namespace HamburgerUI.Models
{
    public sealed class Catalog
    {
        private static Catalog cat = new Catalog();

        private Catalog()
        {
            Archives = new ObservableCollection<Archive>();
            //Archive test = new Archive();
            //File TestFile = new File("tester",DateTimeOffset.Now,"c:nnnnn", 100, ".txt");
            //test.Name = "Test";
            //test.FileSet.Add(TestFile);                     
        }
        
        public void Add(Archive archive)
        {
            this.Archives.Add(archive);
        }

        public void Remove(Archive archiveToRemove)
        {
            this.Archives.Remove(archiveToRemove);
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

        public ObservableCollection<Archive> Archives { get; set; }
                     
        //public Catalog(Archive archive, ICollection<IFile> files)
        //{
        //    FileSet.Add(archive, files);
        //}
        
    }
}
