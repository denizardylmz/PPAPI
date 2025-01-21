using System.ComponentModel.DataAnnotations;

namespace HomeProjectAPI.API.Common.Settings
{
    public class AppSettings
    {
        [Required] public ApiSettings API { get; set; }
        [Required] public Swagger Swagger { get; set; }
        
        [Required] public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ApiSettings
    {
        [Required] public string Title { get; set; }

        public string Description { get; set; }

        public ApiContact Contact { get; set; }

        public string TermsOfServiceUrl { get; set; }

        public ApiLicense License { get; set; }
    }

    public class ApiContact
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Url { get; set; }
    }

    public class ApiLicense
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class Swagger
    {
        [Required] public bool Enabled { get; set; }
    }

    public class ConnectionStrings
    {
        public string EvdbConnection { get; set; }
    }
}