﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class ArcadeCamera : GameObject
{
    protected Window _targetWindow;


    public ArcadeCamera(int x, int y, int width, int height) {
    

        _targetWindow = new Window(x, y, width, height, this);
        game.OnAfterRender += _targetWindow.RenderWindow;

        SetScaleXY(1.0f, 1.0f);


    }



    public void Update() {
    }
    protected override void OnDestroy()
    {
        game.OnAfterRender -= _targetWindow.RenderWindow;
    }
}

