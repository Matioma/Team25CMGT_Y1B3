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
        LaserParticle laser= new LaserParticle("Art/laser.png", 2,1);


        laser.SetScaleXY(1.0f);
        laser.SetPivotPoint(PivotPointPosition.BOTTOM);
        laser.SetXY(owner.Owner.x - 150, owner.Owner.y-60);

        GameManager.Instance.ActiveLevel.AddChildAt(laser, GameManager.Instance.ActiveLevel.GetChildren().Count - 10);


        /*Bottle bottle = new Bottle("Art/acid.png", 2, 1);
        bottle.SetScaleXY(0.5f);

        bottle.SetPivotPoint(PivotPointPosition.BOTTOM);
        bottle.SetXY(owner.Owner.x - 150, owner.Owner.y - 10);

        GameManager.Instance.ActiveLevel.AddChildAt(bottle, GameManager.Instance.ActiveLevel.GetChildren().Count - 10);

    */

        owner.PowerUpUsed(this);

        AudioManager.Instance.PlaySound("Audio/Using_powerup.mp3");

        //this.LateDestroy();
    }
}

