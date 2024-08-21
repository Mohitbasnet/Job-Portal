using System.ComponentModel.DataAnnotations;
namespace _2022E_WebApp.Models

{
    public class JobViewModel

    {
        public int Id { get; set; }
       
        [Required(ErrorMessage = "Title is required")]

        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }
    }
}
