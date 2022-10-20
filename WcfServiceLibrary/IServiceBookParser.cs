using System.Collections.Generic;
using System.ServiceModel;

namespace WCFService
{
    [ServiceContract]
    public interface IServiceBookParser
    {
        [OperationContract]
        Dictionary<string, long> CountUniqueWords(string xml);
    }
}
