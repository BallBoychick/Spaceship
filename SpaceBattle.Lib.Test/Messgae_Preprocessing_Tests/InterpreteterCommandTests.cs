using System;
using Moq;
using Hwdtech;
using SpaceBattle.Lib;
namespace SpaceBattle.Lib.Test;
public class InterpreterCommandTests
{
    [Fact]
        public void TestExecute()
        {
            // Arrange
            var message = new Mock<IMessage>();
            message.Setup(m => m.GameID).Returns(123);
        }
}