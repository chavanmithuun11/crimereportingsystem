using System;
using System.ComponentModel.DataAnnotations;

namespace CrimeReportingSystem.Models
{
    public class CrimeReport
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Crime Type")]
        public string CrimeType { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Incident Date")]
        [DataType(DataType.Date)]
        public DateTime IncidentDate { get; set; }

        public string Status { get; set; } = "Pending";

        [Display(Name = "Reported On")]
        public DateTime CreatedAt { get; set; }
    }
}
