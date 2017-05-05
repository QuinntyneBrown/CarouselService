using System.Configuration;

namespace CarouselService.Features.Search
{

    public interface ISearchConfiguration
    {
        string BaseUri { get; set; }
    }

    public class SearchConfiguration: ConfigurationSection, ISearchConfiguration
    {

        [ConfigurationProperty("baseUri")]
        public string BaseUri
        {
            get { return (string)this["baseUri"]; }
            set { this["baseUri"] = value; }
        }

        public static ISearchConfiguration Config
        {
            get { return ConfigurationManager.GetSection("searchConfiguration") as ISearchConfiguration; }
        }
    }
}
