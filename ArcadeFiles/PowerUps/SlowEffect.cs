using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class SlowEffect:PowerUp
{
    static int _speedReduction = 0;
    public static int SpeedReduction
    {
        get { return _speedReduction; }
        set { _speedReduction = value; }
    }

    
    public SlowEffect(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        AddHitBox(visuals.width, visuals.height);
        hitBox.SetXY(2*visuals.width, 2*visuals.height);
    }

    public override void ApplyEffect(Unit target)
    {
        target.ActualMaxSpeed -= _speedReduction;
    }

    protected override void onPickUp()
    {
    }
    protected override void onPowerUpUse()
    {
        Console.Write("Power Up used SlowEffect");
        //this.LateDestroy();
    }
}

