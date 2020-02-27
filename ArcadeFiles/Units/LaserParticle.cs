using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class LaserParticle:Unit
{
    public LaserParticle(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        //AddHitBox();
        AddHitBox(visuals.width, visuals.height);
        hitBox.SetXY(0f, -5.0f);
        //AddHitBox(visuals.width, visuals.height);

        dx = -10;
    }

   

    public override void Update()
    {


        
        base.Update();
        dy = 0;

        foreach (var obj in getHitBox().GetCollisions()) {
            if (obj is Player)
            {
                Console.WriteLine("GG");
            }
            else if (obj.parent is Tile) {
                Console.WriteLine("Colided with tile");
                LateDestroy();
            }




        }

        //getHitBox().HitTest


        //if (OnGround)
        //{
          //  OnBottleBreak();
          //  dx *= 0.96f;
        //}
    }

    void OnBottleBreak()
    {
        LateDestroy();
        SpawnAcidPuddle();
    }

    void SpawnAcidPuddle()
    {
        var slowEffect = new SlowEffect("Art/puddle.png", 2, 1);


        AudioManager.Instance.PlaySound("Audio/Glass.mp3");

        slowEffect.SetPivotPoint(PivotPointPosition.BOTTOM);

        slowEffect.SetXY(x, y - 20);
        slowEffect.PowerUpDuration = 1;
        slowEffect.SetScaleXY(0.5f);

        GameManager.Instance.ActiveLevel.AddChildAt(slowEffect, GameManager.Instance.ActiveLevel.GetChildren().Count - 10);
    }



    /*void OnCollision(GameObject obj)
    {
        if (obj is Player)
        {
            Console.WriteLine("GG");
        }
        else {

            obj.LateDestroy();
        }

    }*/
}
