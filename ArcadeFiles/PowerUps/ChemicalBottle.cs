using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class ChemicalBottle:PowerUp
{
    public ChemicalBottle(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        AddHitBox();
    }


    public override void ApplyEffect(Unit target)
    {
        throw new NotImplementedException();
    }
}