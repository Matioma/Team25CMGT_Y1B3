using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class SlowEffect:PowerUp
{
    int _speedReduction = 0;
    public int SpeedReduction
    {
        get { return _speedReduction; }
        set { _speedReduction = value; }
    }

    
    public SlowEffect(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        AddHitBox();
    }

    public override void ApplyEffect(Unit target)
    {
        //throw new NotImplementedException();
        Console.WriteLine("SLOWED!");
        target.ActualMaxSpeed -= _speedReduction;
    }
}

