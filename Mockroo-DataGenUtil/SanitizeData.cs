using System;
using System.IO;
using System.Text;

namespace Mockroo_DataGenUtil
{
    public class SanitizeData
    {
        public string SrcFilePath { get; set; }
        public string DestFilePath { get; set; }
        public string[] IdStrings { get; set; }
        public int IdMaxCount { get; set; }
        public SanitizeData(string srcFilePath, string destFilePath,
            string[] idStrings, int idMaxCount)
        {
            SrcFilePath = srcFilePath;
            DestFilePath = destFilePath;
            IdStrings = idStrings;
            IdMaxCount = idMaxCount;
        }

        public void sanitize()
        {
            int idCounter = 1;

            File.Delete(DestFilePath);

            string[] separatorStrings = new string[IdStrings.Length * IdMaxCount];
            int IdCount = 0;
            foreach (string IdString in IdStrings)
            {
                for (int count = 1; count <= IdMaxCount; count++, IdCount++)
                {
                    separatorStrings[IdCount] = IdString + "\":" + count;
                }
            }

            using (var newFile = File.AppendText(DestFilePath))
            {
                foreach (var line in File.ReadLines(SrcFilePath))
                {
                    StringBuilder lineBuilder = new StringBuilder();
                    var splitresp = line.SplitAdvaced(separatorStrings, IdMaxCount);
                    // Build custom line - Start
                    foreach (var item in splitresp)
                    {
                        var strValue = string.Empty;
                        if (item.delimiter != string.Empty && item.delimiter.IndexOf(':') > 0)
                        {
                            strValue = item.delimiter.Split(":")[0] + ":" + idCounter.ToString();
                            idCounter++;
                        }
                        lineBuilder.Append(item.segment + strValue);                        
                    }
                    // Build custom line - End
                    newFile.WriteLine(lineBuilder);
                }
            }
            Console.WriteLine("File Generated Successfully!");
        }
    }
}
