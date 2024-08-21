using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2022E_WebApp.Models
{
    public class PaginatedJobViewModel : PageModel 

    {
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages  => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public List<JobViewModel> Data { get; set; }
    }
}
