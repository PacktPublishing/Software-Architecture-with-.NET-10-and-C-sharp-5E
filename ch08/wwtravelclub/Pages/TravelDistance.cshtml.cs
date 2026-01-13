using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ch08.Services;

namespace ch08.Pages
{
    public class TravelDistanceModel : PageModel
    {
        private readonly ILogger<TravelDistanceModel> _logger;
        private readonly ITravelDistanceService _travelDistanceService;

        public TravelDistanceModel(ILogger<TravelDistanceModel> logger, ITravelDistanceService travelDistanceService)
        {
            _logger = logger;
            _travelDistanceService = travelDistanceService;
        }

        [BindProperty]
        public string City1 { get; set; }

        [BindProperty]
        public string City2 { get; set; }

        public double? Distance { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<string> AvailableCities { get; set; }

        public void OnGet()
        {
            AvailableCities = _travelDistanceService.GetAvailableCities();
        }

        public IActionResult OnPost()
        {
            AvailableCities = _travelDistanceService.GetAvailableCities();

            if (string.IsNullOrWhiteSpace(City1) || string.IsNullOrWhiteSpace(City2))
            {
                ErrorMessage = "Please select both cities";
                return Page();
            }

            try
            {
                Distance = _travelDistanceService.CalculateDistance(City1, City2);
            }
            catch (ArgumentException ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
