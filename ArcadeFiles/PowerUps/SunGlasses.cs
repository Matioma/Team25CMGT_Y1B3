using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class SunGlasses: PowerUp
{
    public SunGlasses(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        AddHitBox(visuals.width, visuals.height);
        hitBox.SetXY(visuals.width+70,visuals.height);
        PowerUpDuration = 2;
    }

    public override void ApplyEffect(Unit target)
    {
    }



    //override 
    protected override void onPickUp()
    {
        AudioManager.Instance.PlaySound("Audio/Pickingup_powerup.wav");
        this.LateRemove();
    }
    protected override void onPowerUpUse()
    {
        LaserParticle laser= new LaserParticle("Art/laser.png", 2,1,owner.Owner.isLookingRight, owner.Owner);

        laser.SetScaleXY(1.0f);
        if (owner.Owner.isLookingRight)
        {
            laser.SetXY(owner.Owner.x , owner.Owner.y );
        }
        else {
            laser.SetXY(owner.Owner.x, owner.Owner.y +30);

        }
        laser.SetXY(owner.Owner.x , owner.Owner.y-30);

        GameManager.Instance.ActiveLevel.AddChildAt(laser, GameManager.Instance.ActiveLevel.GetChildren().Count - 10);


        owner.PowerUpUsed(this);

        AudioManager.Instance.PlaySound("Audio/Using_powerup.mp3");

    }
}

