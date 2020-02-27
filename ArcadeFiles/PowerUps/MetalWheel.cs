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

            AddHitBox(visuals.width, visuals.height);
            hitBox.SetXY(160, 280);
        }

        protected override void onPowerUpUse()
        {
            owner.PowerUpUsed(this);
            AudioManager.Instance.PlaySound("Audio/Using_powerup.mp3");
            this.LateDestroy();
        }

        public override void ApplyEffect(Unit target)
        {
           
        }

       
    }
}
