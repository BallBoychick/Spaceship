using CoreWCF;
using SpaceBattle.Lib;
using System.Collections;

namespace CoreWCFServer
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    internal class WebApi : IWebApi
    {
        public void HandleMessage(MessageContract message)
        {
            var threadID =  IoC.Resolve<string>("Thread ID by Game ID", message.GameID);
            IoC.Resolve<SpaceBattle.Lib.ICommand>("Send Message", threadID,IoC.Resolve<SpaceBattle.Lib.ICommand>("Command From Message", message) ).Execute();
        }
    }
}
