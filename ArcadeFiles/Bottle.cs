using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

class Bottle:Unit
{
    public Bottle(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        AddHitBox();

        dx = -10;
        dy = 5;
    }

    public void AddForce(Vector2 direction, float force) {
        //Vector2 directionNormal = 


    }

    public void Update() {
        base.Update();
      
        if (OnGround)
        {
            OnBottleBreak();
        }
    }

    void OnBottleBreak() {
        LateDestroy();
        SpawnAcidPuddle();
    }

    void SpawnAcidPuddle() {
        var slowEffect = new SlowEffect("Art/puddle.png", 2, 1);
        slowEffect.SetPivotPoint(PivotPointPosition.BOTTOM);

        slowEffect.SetXY(x, y - 20);
        slowEffect.PowerUpDuration = 1;
        slowEffect.SetScaleXY(0.5f);

        GameManager.Instance.ActiveLevel.AddChildAt(slowEffect, GameManager.Instance.ActiveLevel.GetChildren().Count - 10);
    }

}
