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

    

    public Pill(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);

        AddHitBox(visuals.width, visuals.height);
        hitBox.SetXY(visuals.width+10,visuals.height);

        
    }

    protected override void onPickUp()
    {
        base.onPickUp();
        //AudioManager.Instance.PlaySound("Pickingup_powerup.wav");
    }

    protected override void onPowerUpUse()
    {
        owner.PowerUpUsed(this);


        AudioManager.Instance.PlaySound("Audio/Using_powerup.mp3");
        this.LateDestroy();
    }
    public override void ApplyEffect(Unit target)
    {
        target.ActualMaxSpeed += SpeedBonus;
    }
}