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

namespace Methodsbackend
{
    public class SetupClass
    {
        static List<users> Employees = new List<users>();
        public void SetupMethod(String URi)
        {
            var uris = new[]
    {
                new Uri(URi),
            };

            var connectionPool = new SniffingConnectionPool(uris);
            var settings = new ConnectionSettings(connectionPool).DefaultIndex("users").DisableDirectStreaming();

            var client = new ElasticClient(settings);
            UploadData(client);
        }
        public void UploadData(ElasticClient client)
        {
            if (Employees.Count <= 0)
            {
                StreamReader streamReader = new StreamReader(".\\users.csv");

                while (!streamReader.EndOfStream)
                {
                    var csvLine = streamReader.ReadLine();
                    var CsvWords = csvLine.Split(',');
                    if (CsvWords[0] != "id")
                    {
                        users users = new users(CsvWords[1], CsvWords[2], CsvWords[3], CsvWords[5], int.Parse(CsvWords[4]), int.Parse(CsvWords[0]));
                        client.IndexDocument(users);
                    }

                }
            }
        }
    }
}

