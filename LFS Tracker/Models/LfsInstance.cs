using System.ComponentModel.DataAnnotations;

namespace LFS_Tracker.Models
{
    public class LfsInstance
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Instance Name")]
        public string InstanceName { get; set; }
        [Required]
        public float Version { get; set; }
        [Required]
        [Display(Name = "LFS Version")]
        public float LfsVersion { get; set; }

        [Display(Name = "Installed packages")]
        public List<Package> InstalledPackages { get; set; }

        public LfsInstance()
        {
            InstanceName = "";
        }

        public override string ToString() => InstanceName;
    }
}
