using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public interface IShootable
{
    public string ProjectileType
    {
        get;
        set;
    }
    public Vector Pos
    {
        get;
        set;
    }

    public Vector Velocity
    {
        get;
    }
}
