namespace SpaceBattle.Lib.Test;
using Moq;
using System.Collections.Concurrent;
using Xunit;
using Hwdtech.Ioc;
using Hwdtech;
using SpaceBattle.Lib;

public class SendMessageStrategyTests{
    public SendMessageStrategyTests(){
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Send Message", (object [] parameters) => new SendMessageStrategy().RunStrategy(parameters)).Execute();

    }
    [Fact]
    public void MessagePushInQueueSuccess(){
        var queueMessageThread = new BlockingCollection<IMessage>();
        var mockSender = new Mock<ISender>();
        var command = new Mock<SpaceBattle.Lib.ICommand>();
        var messageFromJson = new Mock<IMessage>();
        var threadID = "1";
        messageFromJson.SetupGet(message => message.GameID).Returns(threadID);
        mockSender.Setup(sender => sender.Send(It.IsAny<SpaceBattle.Lib.ICommand>())).Callback(() => queueMessageThread.Add(messageFromJson.Object)).Verifiable();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Thread.QueueMessages" + threadID, (object [] parameters) => mockSender.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CommandFromMessageStrategy", (object [] parameters) => command.Object).Execute();
        IoC.Resolve<bool>("Send Message", messageFromJson.Object);
        mockSender.Verify();
        Assert.True(queueMessageThread.Take().GameID == "1");
    }
}
