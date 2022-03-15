using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Pages.Methodsbackend;
using WebApplication1.Pages.Results;
using Nest;


namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ElasticClient _client;

        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string query { get; set; }
        public string options { get; set; }
        public List<string> value = QueryIndex.ValueOptions;
        public int Number { get; set; }
        public List<users> users = new List<users>();
        public bool TableOnline { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ElasticClient client)
        {
            _logger = logger;
            _client = client;
        }

        public void OnGet()
        {

        }
    }

}
