using System.Collections.Generic;
using System.Runtime.Serialization;
using CoreWCF.OpenApi.Attributes;
using SpaceBattle.Lib;

namespace CoreWCFServer
{
    [DataContract(Name = "MessageContract", Namespace = "http://spacebattle.com")]
    internal class MessageContract : IMessage
    {
        [DataMember(Name = "Type", Order = 1)]
        [OpenApiProperty(Description = "Type description.")]
        public string Type { get; set; }

        [DataMember(Name = "GameID", Order = 2)]
        [OpenApiProperty(Description = "GameID description.")]
        public string GameID { get; set; }

        [DataMember(Name = "GameItemID", Order = 3)]
        [OpenApiProperty(Description = "GameItemID description.")]
        public string GameItemID { get; set; }


        [DataMember(Name = "Properties", Order = 4)]
        [OpenApiProperty(Description = "Properties description.")]
        public IDictionary<string, object> Properties { get; set; }
    }
}
