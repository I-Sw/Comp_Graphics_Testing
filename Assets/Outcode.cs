using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outcode
{
    internal bool up, down, left, right;

    public Outcode()
    {
        up = false;
        down = false;
        left = false;
        right = false;
    }

    public Outcode(Vector2 point)
    {
        up = (point.y > 1);
        down = (point.y < -1);
        left = (point.x < -1);
        right = (point.x > 1);
    }

    public Outcode(bool up, bool down, bool left, bool right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public string ToString() //Output the outcode as 0000, 1001, etc...
    {
        string output = "";
        if (up) output += "1"; else output += "0";
        if (down) output += "1"; else output += "0";
        if (left) output += "1"; else output += "0";
        if (right) output += "1"; else output += "0";
        return output;
    }

    public static Outcode operator +(Outcode a, Outcode b) //Logical OR
    {
        return new Outcode((a.up || b.up), (a.down || b.down), (a.left || b.left), (a.right || b.right));
    }

    public static Outcode operator *(Outcode a, Outcode b) //Logical AND
    {
        return new Outcode((a.up && b.up), (a.down && b.down), (a.left && b.left), (a.right && b.right));
    }

    public static bool operator ==(Outcode a, Outcode b) //Are outcodes equal
    {
        return ((a.up == b.up) && (a.down == b.down) && (a.left == b.left) && (a.right == b.right));
    }

    public static bool operator !=(Outcode a, Outcode b) //Are outcodes not equal
    {
        return !(a == b);
    }
}
