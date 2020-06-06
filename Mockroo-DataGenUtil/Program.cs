using Mockroo_DataGenUtil;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace DataCorrectUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            //sanitizeData_v1();
            sanitizeData_v2();
        }

        public static void sanitizeData_v2()
        {
            string srcFilePath = ConfigurationManager.AppSettings["SourcePath"];
            string destFilePath = ConfigurationManager.AppSettings["DestinationPath"];
            string[] IdStrings = ConfigurationManager.AppSettings["IdStrings"].Split(',');
            int IdMaxCount = Int32.Parse(ConfigurationManager.AppSettings["RepeatCount"]);

            int idCounter = 1;

            File.Delete(destFilePath);

            string[] separatorStrings = new string[IdStrings.Length * IdMaxCount];
            int IdCount = 0;
            foreach (string IdString in IdStrings)
            {
                for (int count = 1; count <= IdMaxCount; count++, IdCount++)
                {
                    separatorStrings[IdCount] = IdString + "\":" + count;
                }
            }

            using (var newFile = File.AppendText(destFilePath))
            {
                foreach (var line in File.ReadLines(srcFilePath))
                {
                    StringBuilder lineBuilder = new StringBuilder();
                    var splitresp = line.SplitAdvaced(separatorStrings, IdMaxCount);
                    // Build custom line - Start
                    foreach(var item in splitresp)
                    {
                        var strValue = string.Empty;                        
                        if (item.delimiter!= string.Empty && item.delimiter.IndexOf(':') > 0)
                        {
                            strValue = item.delimiter.Split(":")[0] + ":" + idCounter.ToString();                            
                        } 
                        lineBuilder.Append(item.segment + strValue);
                        idCounter++;
                    }
                    // Build custom line - End
                    newFile.WriteLine(lineBuilder);
                }
            }

            Console.WriteLine("File Generated Successfully!");

        }

        public static void sanitizeData_v1()
        {
            string srcFilePath = ConfigurationManager.AppSettings["SourcePath"];
            string destFilePath = ConfigurationManager.AppSettings["DestinationPath"];
            string[] IdStrings = ConfigurationManager.AppSettings["IdStrings"].Split(',');                
            int IdMaxCount = Int32.Parse(ConfigurationManager.AppSettings["RepeatCount"]);

            int idCounter = 1;
            File.Delete(destFilePath);

            //string[] separatorStrings = new string[] {
            //            "ExamId\":1",
            //            "ExamId\":2",
            //            "ExamId\":3",
            //            "ExamId\":4",
            //            "ExamId\":5",
            //        };

            string[] separatorStrings = new string[IdStrings.Length * IdMaxCount];
            int IdCount = 0;
            foreach (string IdString in IdStrings)
            {
                for (int count = 1; count <= IdMaxCount; count++, IdCount++)
                {
                    separatorStrings[IdCount] = IdString + "\":" + count;
                }
            }

            using (var newFile = File.AppendText(destFilePath))
            {
                foreach (var line in File.ReadLines(srcFilePath))
                {
                    StringBuilder lineBuilder = new StringBuilder();

                    var segments = line.Split(separatorStrings, StringSplitOptions.None);
                    var advsegments = line.SplitAdvaced(separatorStrings, IdMaxCount);
                    lineBuilder.Append(segments[0]);

                    for (int segCount = 1; segCount < segments.Length; segCount++, idCounter++)
                    {
                        lineBuilder.Append("ExamId\":" + idCounter.ToString() + segments[segCount]);
                    }
                    newFile.WriteLine(lineBuilder);
                }
            }

            Console.WriteLine("File Generated Successfully!");
        }
    }
}
