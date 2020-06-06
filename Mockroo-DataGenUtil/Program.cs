using Mockroo_DataGenUtil;
using System;
using System.Configuration;

namespace DataCorrectUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            string srcFilePath = ConfigurationManager.AppSettings["SourcePath"];
            string destFilePath = ConfigurationManager.AppSettings["DestinationPath"];
            string[] IdStrings = ConfigurationManager.AppSettings["IdStrings"].Split(',');
            int IdMaxCount = Int32.Parse(ConfigurationManager.AppSettings["RepeatCount"]);

            SanitizeData data = new SanitizeData(
                srcFilePath,
                destFilePath,
                IdStrings,
                IdMaxCount
                );

            data.sanitize();
        }

        
    }
}
