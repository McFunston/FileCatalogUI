using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Models
{
    public interface IFile
    {
        string Name { get; set; }
        DateTimeOffset DateCreated { get; set; }
        string Path { get; set; }
        ulong Size { get; set; }
        string Extension { get; set; }
    }
}
