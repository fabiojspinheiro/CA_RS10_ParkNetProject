namespace ParkNet_Fabio.Pinheiro.App.Services;

public class ReadLayout
{
    private string _alpha = "ABCDEFGHIJKLMNOPQRSTUVQXYZ";

    public List<Floor> ReadFloor(string layout, Park park)
    {
        List<Floor> floors = new List<Floor>();

        var floorLayout = layout.Split("\r\n\r\n"); // Split by floors
        foreach (var f in floorLayout)
        {
            Floor floor = new Floor { ParkId = park.Id };

            var rowOfFloors = f.Split("\r\n");      // Split by row
            foreach (var row in rowOfFloors)
            {
                floor.RowOfSpaces.Add(row);
            }

            floors.Add(floor);
        }

        return floors;
    }

    public List<ParkingSpace> ReadParkingSpace(List<Floor> floors)
    {
        List<ParkingSpace> space = new List<ParkingSpace>();

        int countRows = 0;
        foreach (var floor in floors)       // Floors
        {
            foreach (var s in floor.RowOfSpaces)        // Lines
            {
                for (int i = 0; i < s.Length; i++)      // Parking Spaces
                {
                    if (s[i] != ' ')
                    {
                        if (s[i].ToString().ToUpper() == "C")
                            space.Add(new ParkingSpace { FloorId = floor.Id, Name = $"{_alpha[countRows]}{i + 1}", TypeId = 1 });
                        else if (s[i].ToString().ToUpper() == "M")
                            space.Add(new ParkingSpace { FloorId = floor.Id, Name = $"{_alpha[countRows]}{i + 1}", TypeId = 2 });
                    }
                }
                countRows++;
            }
        }
        return space;
    }
}
