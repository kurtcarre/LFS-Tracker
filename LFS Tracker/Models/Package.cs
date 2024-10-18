using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS_Tracker.Models
{
    public class Package
    {
        [Required]
        [Key]
        [Display(Name = "Package ID")]
        public int PackageId { get; set; }
        [Required]
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }
        [Required]
        public float Version { get; set; }
        [Required]
        [Display(Name = "LFS Version")]
        public float LfsVersion { get; set; }

        [Display(Name = "Core LFS Package?")]
        public bool IsCoreLfsPackage { get; set; }

        [Display(Name = "Instances with this package installed")]
        public List<LfsInstance> InstalledInstances { get; set; }

        public Package()
        {
            PackageName = "";
        }

        public override string ToString() => PackageName;
    }
}
