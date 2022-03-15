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

namespace WebApplication1.Pages.Methodsbackend
{
    public class QueryIndex
    {
        private readonly static IElasticClient elasticClient;
        static List<users> Employees = new List<users>();


        public static List<string> ValueOptions = new List<string>
        {
            "FirstName",
            "LastName",
            "Email",
            "Proffession",
            "default"
        };

        public static List<users> GetResults(String Query, string value, ElasticClient client)
        {
            var searchResponse = client.Search<users>(s => s
                    .From(0)
                    .Size(4000)
                    .Query(q => q.MatchAll()
                    )
                );
            List<users> Employees = searchResponse.Documents.ToList<users>();
            for (int i = 0; i <= (Employees.Count() - 1); i++)
            {
                var csvLine = Employees[i].Email;
                var CsvWords = csvLine.Split('@');
                csvLine = CsvWords[0];
                var FirstNameLastName = csvLine.Split('.');
                Employees[i].FirstName = FirstNameLastName[0];
                Employees[i].LastName = FirstNameLastName[1];

            }

            List<users> Results = new List<users>();
            switch (value)
            {
                case "FirstName":
                    {

                        var QueryList = Employees.Where(x => x.FirstName.Contains(Query)).ToList();
                        if (QueryList.Any())
                        {
                            Results = QueryList;
                        }

                        break;
                    }
                case "LastName":
                    {
                        var QueryList = Employees.Where(x => x.LastName.Contains(Query)).ToList();
                        if (QueryList.Any())
                        {
                            Results = QueryList;
                        }
                        break;
                    }
                case "Email":
                    {
                        var QueryList = Employees.Where(x => x.Email.Contains(Query)).ToList();
                        if (QueryList.Any())
                        {
                            Results = QueryList;
                        }
                        break;
                    }
                case "Proffession":
                    {
                        var QueryList = Employees.Where(x => x.Profession.Contains(Query)).ToList();
                        if (QueryList.Any())
                        {
                            Results = QueryList;
                        }
                        break;
                    }
                default:
                    {
                        var QueryList = Employees.Where(x => x.FirstName.Contains(Query)).ToList();
                        QueryList.AddRange(Employees.Where(x => x.LastName.Contains(Query)).ToList());
                        QueryList.AddRange(Employees.Where(x => x.Email.Contains(Query)).ToList());
                        QueryList.AddRange(Employees.Where(x => x.Profession.Contains(Query)).ToList());
                        if (QueryList.Any())
                        {
                            Results = QueryList;
                        }
                        break;
                    }
            }
            return Results;
        }
        public static async void UploadData(ElasticClient client)
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
