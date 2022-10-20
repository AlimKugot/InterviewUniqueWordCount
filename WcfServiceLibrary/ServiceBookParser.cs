using System;
using System.Collections.Generic;
using InterviewUniqueWordCount;

namespace WCFService
{
    public class ServiceBookParser : IServiceBookParser
    {
        public Dictionary<string, long> CountUniqueWords(string xml)
        {
            var words =  FbUtility.ParseXmlString(xml);
            return FbUtility.CountUniqueWords(words);
        }
    }
}
