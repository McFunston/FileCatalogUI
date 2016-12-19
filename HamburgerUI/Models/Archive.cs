using System.Collections.Generic;
using System.IO;

namespace HamburgerUI.Models
{
    public enum DriveType
    {
        CDRom,
        Local,
        Network,
        Floppy,
        USB,
        Camera
    }
    /// <summary>
    /// This is the Key type for the sorted list pairs of the archive set.
    /// </summary>
    public class Archive
    {
        public int Id { get; set; }
        public string Name { get; set; } //The name of the archive set (eg. "DVD #5")
        public DriveType DriveType { get; set; } //Captured so that we can tell the user the type of the archive media (USB, vs DVD, etc)
        public string Label { get; set; }
        public ICollection<File> Files { get; set; }

        public Archive(string name, DriveType driveType, string label)
        {
            Name = name;
            DriveType = driveType;
            Label = label;
        }
    }
}
