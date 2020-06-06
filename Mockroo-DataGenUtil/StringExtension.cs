using System;
using System.Collections.Generic;

namespace Mockroo_DataGenUtil
{
    public static class StringExtension
    {
        public class SplitResponse
        {
            public string delimiter { get; set; }
            public string segment{ get; set; }

        }

        public static List<SplitResponse> SplitAdvaced(this String inputString, string[] delimiters, int IdMaxCount)
        {
            var tempString = inputString;
            List<string> delimiterHitTrace = new List<string>();
            List<string> segments = new List<string>();

            List<SplitResponse> splitresponse= new List<SplitResponse>();

            while (tempString.Length >0)
            {
                bool isFound = false;
                foreach (var delimiter in delimiters)
                {   
                    var pos = tempString.IndexOf(delimiter);
                    if (pos > 0)
                    {                        
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
                    splitresponse.Add(new SplitResponse
                    {
                        delimiter = string.Empty,
                        segment = tempString
                    });
                    tempString = string.Empty;
                }
            }
            return splitresponse;
        }
    }
}
