namespace SpaceBattle.Lib;
public class Angle
{
    public int numa;
    public int dena;
    public Angle(int angle1, int angle2)
    {
        numa = angle1;
        dena = angle2;
        if (dena == 0)
        {
            throw new System.DivideByZeroException();
        }
    }
    public static int GCD(int x, int y)
    {
        return y == 0 ? x : GCD(y, x % y);
    }
    public override string ToString()
    {
        return numa.ToString() + "/" + dena.ToString();
    }
    public static Angle operator +(Angle angle1, Angle angle2)
    {
        int y3 = GCD(angle1.numa * angle2.dena + angle2.numa * angle1.dena, angle1.dena * angle2.dena);
        return new Angle((angle1.numa * angle2.dena + angle2.numa * angle1.dena) / y3, angle1.dena * angle2.dena / y3);

    }
    public static bool operator ==(Angle angle1, Angle angle2)
    {
        return (decimal)angle1.numa * angle2.dena == (decimal)angle2.numa * angle1.dena;
    }
    public static bool operator !=(Angle angle1, Angle angle2)
    {
        return (!(angle1 == angle2));
    }
    public override bool Equals(object? obj)
    {
        return obj is Angle angle &&
         numa * angle.dena == angle.numa * dena;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(numa, dena);
    }
}