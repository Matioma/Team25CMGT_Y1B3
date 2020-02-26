using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class InventorySlot:GameObject
{
    public InventorySlot() {
        SetXY(-1000, -1000);
        //visuals = new AnimationSprite(FileName, cols, rows);
        //visuals.width = 10;
        //visuals.height = 10;

        //AddChild(visuals);

        var arcadeCamera = new ArcadeCamera(0, 0, 60, 60);
        arcadeCamera.SetXY(10, 10);
        this.AddChild(arcadeCamera);


    }
}
