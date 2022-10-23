namespace SpaceBattle.Lib.Test;
public class RotateCommandTest
{
    [Fact]
    public void RotateCommandTestPositive()
    {
        // Arrange
        Mock<IRotatable> rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupProperty(i => i.Angle, new Angle(45, 1));
        rotatableMock.SetupGet<Angle>(i => i.AngleVelocity).Returns(new Angle(90, 1));
        ICommand rotatecommand = new RotateCommand(rotatableMock.Object);
        // Act
        rotatecommand.execute();
        // Assert
        Assert.Equal(new Angle(135, 1), rotatableMock.Object.Angle);
    }

    [Fact]
    public void RotateCommandTestCantGetVelocity()
    {
        // Arrange
        Mock<IRotatable> rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupProperty(i => i.Angle, new Angle(45, 1));
        rotatableMock.SetupGet<Angle>(i => i.AngleVelocity).Throws<ArgumentException>();
        ICommand rotatecommand = new RotateCommand(rotatableMock.Object);
        //Assert
        Assert.Throws<ArgumentException>(() => rotatecommand.execute());
    }

    [Fact]
    public void RotateCommandTestCantGetInstantaneousAngleVelocity()
    {
        // Arrange
        Mock<IRotatable> rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupProperty(i => i.Angle, new Angle(45, 1));
        rotatableMock.SetupGet<Angle>(i => i.AngleVelocity).Throws<Exception>();
        ICommand rotatecommand = new RotateCommand(rotatableMock.Object);
        // Assert
        Assert.Throws<Exception>(() => rotatecommand.execute());
    }

    [Fact]
    public void RotateCommandTesCantSetPosition()
    {
        // Arrange
        Mock<IRotatable> rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupProperty(i => i.Angle, new Angle(45, 1));
        rotatableMock.SetupSet<Angle>(i => i.Angle = It.IsAny<Angle>()).Throws<ArgumentException>();
        rotatableMock.SetupGet<Angle>(i => i.AngleVelocity).Returns(new Angle(90, 1));
        ICommand rotatecommand = new RotateCommand(rotatableMock.Object);
        // Assert
        Assert.Throws<ArgumentException>(() => rotatecommand.execute());
    }
}