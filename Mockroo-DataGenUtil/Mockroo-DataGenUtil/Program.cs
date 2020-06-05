using System;
using System.IO;
using System.Text;

namespace DataCorrectUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            #region current setup path
            ////Source and Dest Files
            //string path = @"\myhospitalapp\bin\Debug\netcoreapp3.1\";
            //string srcFileName = "MOCK_DATA.json";
            //string destFileName = "MOCK_PATIENTS_DATA.json";

            //string parentDir = Directory.GetParent(
            //Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

            //string srcFilePath = parentDir + path + srcFileName;
            //string destFilePath = parentDir + path + destFileName;
            ////
            #endregion

            string srcFilePath = @"..\..\..\MOCK_DATA.json";
            string destFilePath = @"..\..\..\MOCK_PATIENTS_DATA.json";

            string[] IdStrings = new[] {
                "ExamId",
                "id"
            };
            int IdMaxCount = 5;

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
