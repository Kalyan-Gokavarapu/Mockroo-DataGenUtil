using System;
using System.Collections.Generic;
using System.Text;

namespace Mockroo_DataGenUtil
{
    public static class StringExtension
    {
        //public class StringExtensionResponse
        //{
        //    public List<string> delimiterHitTrace { get; set; }
        //    public List<string> segments { get; set; }

        //}

        public class SplitResponse
        {
            public string delimiter { get; set; }
            public string segment{ get; set; }

        }
        //public static Dictionary<string, string> SplitAdvaced(this String inputString, string[] delimiters, int IdMaxCount)
        public static List<SplitResponse> SplitAdvaced(this String inputString, string[] delimiters, int IdMaxCount)
        {
            var tempString = inputString;
            List<string> delimiterHitTrace = new List<string>();
            List<string> segments = new List<string>();

            List<SplitResponse> splitresponse= new List<SplitResponse>();

            ////Dictionary<string, string> respDict = new Dictionary<string, string>();

            while (tempString.Length >0)
            {
                bool isFound = false;
                foreach (var delimiter in delimiters)
                {   
                    var pos = tempString.IndexOf(delimiter);
                    if (pos > 0)
                    {
                        ////respDict.Add(delimiter, tempString.Substring(0, pos));
                        //delimiterHitTrace.Add(delimiter);
                        //segments.Add(tempString.Substring(0, pos));                        
                        splitresponse.Add(new SplitResponse{
                            delimiter = delimiter,
                            segment= tempString.Substring(0, pos)
                        });
                        tempString = tempString.Substring(pos + delimiter.Length);
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    //delimiterHitTrace.Add(string.Empty);
                    //segments.Add(tempString);
                    splitresponse.Add(new SplitResponse
                    {
                        delimiter = string.Empty,
                        segment = tempString
                    });
                    tempString = string.Empty;
                }
            }

            //var response = new StringExtensionResponse
            //{
            //    delimiterHitTrace = delimiterHitTrace,
            //    segments = segments
            //};
            return splitresponse;
        }
    }
}
