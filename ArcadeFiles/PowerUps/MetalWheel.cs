using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.ArcadeFiles.PowerUps
{
    class MetalWheel:PowerUp
    {
        public MetalWheel(string spriteSheet, int cols, int rows)
        {
            visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
            AddChild(visuals);
            AddHitBox();
        }


        public override void ApplyEffect(Unit target)
        {
            //Console.WriteLine("Metal wheel");
        }

       
    }
}
