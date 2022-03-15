using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebApplication1.Pages;
using WebApplication1.Pages.Results;
using Nest;
using System.IO;
using Elasticsearch.Net;
using Methodsbackend;

namespace Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupClass setup = new SetupClass();
            setup.SetupMethod("http://127.0.0.1:9200");
            Console.WriteLine("Hello World!");

        }

    }
}
