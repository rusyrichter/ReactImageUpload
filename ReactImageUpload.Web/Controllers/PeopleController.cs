using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactImageUpload.Web.Models;
using System;
using CsvHelper;
using System.Globalization;
using System.Text;
using Faker;
using ReactImageUpload.Data;

namespace ReactImageUpload.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private string _connectionString;

        public PeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("buildPeopleCsv/{amount}")]
        public IActionResult BuildPeopleCsv(int amount)
        {
            var people = GeneratePeople(amount);          
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            string csvstring = builder.ToString();
            return File(Encoding.UTF8.GetBytes(csvstring), "text/csv", "people.csv");
        }
        [HttpPost]
        [Route("upload")]
        public void Upload(UploadViewModel vm)
        {
            string base64 = vm.Base64.Substring(vm.Base64.IndexOf(",") + 1);
            byte[] imageBytes = Convert.FromBase64String(base64);
            System.IO.File.WriteAllBytes($"uploads/{vm.Name}", imageBytes);
            var people = GetCsvFromBytes(imageBytes);
            var repo = new ImageRepository(_connectionString);
            repo.AddPeople(people);
        }

        [HttpGet]
        [Route("getdata")]
        public List<Person> BuildPeopleCsv()
        {
            var repo = new ImageRepository(_connectionString);
            return repo.GetPeople();
        }
        [HttpPost]
        [Route("deleteall")] 
        public void DeleteAll()
        {
            var repo = new ImageRepository(_connectionString);
            repo.DeleteAll();
        }

        private static List<Person> GeneratePeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ => new Person
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Age = Faker.RandomNumber.Next(20, 100),
                Email = Faker.Internet.Email(),
                Address = Faker.Address.StreetAddress()
            }).ToList();
          
        }
        private static List<Person> GetCsvFromBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }




    }
}
