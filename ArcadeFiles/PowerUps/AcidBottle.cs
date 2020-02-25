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

    protected override void onPickUp()
    {
        Console.WriteLine(this + "Picked UP");
        //SlowEffect temp = new SlowEffect("Art/acid_puddle_test.png", 2, 1);
        
      

        this.LateRemove();
    }
    protected override void onPowerUpUse()
    {
        Console.WriteLine(this+ "Power Up used");

        var position = TransformPoint(x, y);


        Bottle bottle = new Bottle("Art/acid.png",2,1);
        bottle.SetScaleXY(0.5f);

        bottle.SetPivotPoint(PivotPointPosition.BOTTOM);
        bottle.SetXY(owner.Owner.x - 150, owner.Owner.y - 10);

        GameManager.Instance.ActiveLevel.AddChildAt(bottle, GameManager.Instance.ActiveLevel.GetChildren().Count - 10);

        /*var slowEffect = new SlowEffect("Art/puddle.png", 2, 1);
        slowEffect.SetPivotPoint(PivotPointPosition.BOTTOM);
        
        slowEffect.SetXY(owner.Owner.x-150, owner.Owner.y+30);
        //slowEffect.SpeedReduction = 3;
        slowEffect.PowerUpDuration = 1;
        slowEffect.SetScaleXY(0.5f);
        */

        //GameManager.Instance.ActiveLevel.AddChildAt(slowEffect,GameManager.Instance.ActiveLevel.GetChildren().Count - 10);

        this.LateDestroy();
    }
}

