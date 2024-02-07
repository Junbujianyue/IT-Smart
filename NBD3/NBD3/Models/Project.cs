using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(120, ErrorMessage = "Project name cannot exceed 120 characters.")]
        public string ProjectName { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000, ErrorMessage = "Description can be at most 2000 characters.")]
        [DataType(DataType.MultilineText)]
        public string ProjectDescription { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        [EnsureStartDateNotInPast(ErrorMessage = "Start date cannot be in the past.")]
        public DateOnly ProjectStartDate { get; set; }


        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateOnly? ProjectEndDate { get; set; }

        // Foreign Keys
        [Display(Name = "Client")]
        [Required(ErrorMessage = "Client selection is required.")]
        public int ClientId { get; set; }

        [Display(Name = "Client")]
        public Client Client { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location selection is required.")]
        public int LocationId { get; set; }

        [Display(Name = "Location")]
        public Location Location { get; set; }

        // Additional Validation
        [EnsureEndDateNotBeforeStartDate]
        public DateOnly? ProjectEndDateValue { get; set; }

        // Navigation property
        public ICollection<Location> Locations { get; set; } = new HashSet<Location>();
    }

    public class EnsureEndDateNotBeforeStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var project = (Project)validationContext.ObjectInstance;

            if (project.ProjectEndDate.HasValue && project.ProjectEndDate.Value < project.ProjectStartDate)
            {
                return new ValidationResult("End date cannot be before the start date.");
            }

            return ValidationResult.Success;
        }
    }
    public class EnsureStartDateNotInPastAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var date = (DateOnly)value;
            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            return date >= currentDate;
        }
    }
}
