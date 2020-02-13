using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
public abstract class PowerUp: ArcadeObject
{
    public string message = "TestMessage";

    public virtual void Update() {
        var level = parent as Level;
        DoCollisionCheck(level.playersList);
    }

    protected override void DoCollisionCheck(List<ArcadeObject> arcadeObjects)
    {
        foreach (var arcadeObject in arcadeObjects) {
            if (getHitBox().HitTest(arcadeObject.getHitBox())) {
                var player =arcadeObject as Player;
                if (player != null) {
                    player.PickPowerUP(this);
                    this.LateRemove();
                }
            }
        }
    }
}
