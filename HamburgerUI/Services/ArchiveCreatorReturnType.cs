using HamburgerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services
{
    public class ArchiveCreatorReturnType
    {
        public bool Success { get; set; }
        public Archive archive { get; set; }
        public string ErrorMessage { get; set; }
    }
}
