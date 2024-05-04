namespace ParkNet_Fabio.Pinheiro.App.Tests;

public class ParkTests
{
    private readonly ReadLayout _readLayout;

    public ParkTests()
    {
        _readLayout = new ReadLayout();
    }

    [Fact]
    public void Read_ParkingSpaces_ResultOk()
    {
        // Arrange
        var park = new Park { Id = 1, Name = "TestPark" };
        string layout = "MM     CCCC\r\nMM MMM MMMM\r\nCCCCCCCCCCCC\r\n\r\nMMMM MMM\r\nMMMM MMM\r\nMMMM MMM";

        var floors = _readLayout.ReadFloor(layout, park);

        // Act
        var parkingSpaces = _readLayout.ReadParkingSpace(floors);

        //Assert
        parkingSpaces.Count.Should().Be(48);
    }

    [Fact]
    public void Read_ParkingSpaces_ResultNull()
    {
        // Arrange
        var park = new Park { Id = 1, Name = "TestPark" };
        string layout = "";

        var floors = _readLayout.ReadFloor(layout, park);

        // Act
        var parkingSpaces = _readLayout.ReadParkingSpace(floors);

        //Assert
        parkingSpaces.Should().BeNullOrEmpty();
    }
}