using CoreWCF;
using SpaceBattle.Lib;

namespace CoreWCFServer
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    internal class WebApi : IWebApi
    {
        public void HandleMessage(MessageContract message)
        {
            IoC.Resolve<SpaceBattle.Lib.ICommand>("Send Message", message).Execute();
        }
    }
}
