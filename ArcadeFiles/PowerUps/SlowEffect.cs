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
        target.ActualMaxSpeed -= _speedReduction;
    }

    protected override void onPickUp()
    {
        Console.WriteLine(this + "Picked UP SlowEffect ");
        //this.LateRemove();
    }
    protected override void onPowerUpUse()
    {
        Console.Write("Power Up used SlowEffect");
        //this.LateDestroy();
    }
}

