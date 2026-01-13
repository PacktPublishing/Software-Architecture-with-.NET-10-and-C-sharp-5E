using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ch08.Pages
{
    public class EmailsModel : PageModel
    {
        private readonly ILogger<EmailsModel> _logger;

        public EmailsModel(ILogger<EmailsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
