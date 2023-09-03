using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    [MetadataType(typeof(TasktMetaData))]
    public partial class Task
    {
        public List<string> SelectedEmployees { get; set; }
    }
    public class TasktMetaData
    {
        [Display(Name = "Project"), Required(ErrorMessage = "You must select project")]
        public Nullable<int> ProjectId { get; set; }
        [Required(ErrorMessage = "You must select project"), MaxLength(200)]
        public string Description { get; set; }
        [Display(Name = "Start Date of Task"), Required(ErrorMessage = "You must select start date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]

        public System.DateTime StartDate { get; set; }
        [Display(Name = "Due Date of Task"), Required(ErrorMessage = "You must end date")]
        public System.DateTime EndDate { get; set; }
        [Display(Name = "Who should do this?"), Required(ErrorMessage = "You must select employee")]
        public List<string> SelectedEmployees { get; set; }
    }
}