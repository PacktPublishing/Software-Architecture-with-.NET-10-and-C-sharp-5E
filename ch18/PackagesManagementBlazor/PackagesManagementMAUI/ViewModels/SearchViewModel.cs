using System.ComponentModel.DataAnnotations;

namespace PackagesManagementMAUI.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        public string? Location { get; set; }

    }
}
