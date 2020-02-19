using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Background:UIElement
{
    float scalingFactor=0.0f;
    public Background(string path, int cols, int rows) : base(path, cols, rows)
    {
        background.SetOrigin(background.width / 2, background.height / 2);
        background.SetXY(background.width / 2, background.height / 2);
    }

    void Update() {
        scalingFactor += Time.deltaTime;

        


        background.SetScaleXY(1 + Math.Abs((float)Math.Sin(scalingFactor / 1000 / 5))/3);
    }
}