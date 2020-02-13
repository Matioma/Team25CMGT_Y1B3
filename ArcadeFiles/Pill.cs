using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class Pill:PowerUp
{
    private int _speedBonus = 0;
    public int SpeedBonus {
        get { return _speedBonus; }
        set { _speedBonus = value; }
    }

    private float _speedDuration = 0;
    public float SpeedDuration
    {
        get { return _speedDuration; }
        set { _speedDuration = value; }
    }

    public Pill(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        AddHitBox();
    }
}