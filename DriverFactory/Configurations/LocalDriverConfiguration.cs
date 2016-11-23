namespace DriverFactory.Configurations
{
    /// <summary>
    /// A LocalDriverConfiguration class.
    /// For a local instance we need require the browser's name and resolution
    /// </summary>
    public class LocalDriverConfiguration
    {
        public string Browser { get; set; }
        public string Resolution { get; set; }

        public LocalDriverConfiguration(string browser, string resolution)
        {
            Browser = browser;
            Resolution = resolution;
        }
    }
}
