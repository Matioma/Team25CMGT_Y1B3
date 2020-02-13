using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.ArcadeFiles.PowerUps
{
    class MetalWheel:PowerUp
    {
        private float _metalWheelTime = 0;
        public float MetalWheelTime
        {
            get { return _metalWheelTime; }
            set { _metalWheelTime = value; }
        }
        public MetalWheel(string spriteSheet, int cols, int rows)
        {
            visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
            AddChild(visuals);
            AddHitBox();
        }
    }
}
