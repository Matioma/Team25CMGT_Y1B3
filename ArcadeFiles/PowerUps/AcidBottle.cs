using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class AcidBottle:PowerUp
{
    public AcidBottle(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        AddHitBox(visuals.width, visuals.height);
        hitBox.SetXY(visuals.width-45, 2*visuals.height-45);
    }

    public override void ApplyEffect(Unit target)
    {
    }



    //override 
    protected override void onPickUp()
    {
        this.LateRemove();
    }
    protected override void onPowerUpUse()
    {
       
        var position = TransformPoint(x, y);


        Bottle bottle = new Bottle("Art/acid.png",2,1);
        bottle.SetScaleXY(0.5f);

        bottle.SetPivotPoint(PivotPointPosition.BOTTOM);
        bottle.SetXY(owner.Owner.x - 150, owner.Owner.y - 10);

        GameManager.Instance.ActiveLevel.AddChildAt(bottle, GameManager.Instance.ActiveLevel.GetChildren().Count - 10);


        owner.PowerUpUsed(this);
        //this.LateDestroy();
    }
}

