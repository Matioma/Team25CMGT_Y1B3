using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Separator:GameObject
{
    public static string FileName = "";
    public static int SepWidth = 0;
    public static int SepHeight =  0;
    public static int cols = 1;
    public static int rows = 1;

    AnimationSprite visuals;

    public Separator() {
        SetXY(-1000, -1000);
        visuals = new AnimationSprite(FileName, cols, rows);
        visuals.width = SepWidth;
        visuals.height = SepHeight;
            
        AddChild(visuals);

        var arcadeCamera = new ArcadeCamera(0, Game.main.height/2-SepHeight/2, SepWidth, SepHeight);
        arcadeCamera.SetXY(SepWidth/2, SepHeight/2);
        this.AddChild(arcadeCamera);
      
    }


}
