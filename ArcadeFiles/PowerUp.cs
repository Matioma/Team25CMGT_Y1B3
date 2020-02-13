using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class PowerUp: ArcadeObject
{
    public string name;
    public PowerUp(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        AddHitBox();
    }


    void Update() {

        Console.WriteLine(name);
    }

    /*public override void CollidedWith(GameObject other)
    {
        if(other is Player)
            Console.WriteLine(name);
        //base.CollidedWith(other);
    }*/



}
