﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Player : ArcadeObject
{
    //static int NumberOfPlayers = 0;

    Camera _cameraRef = null;
    Camera PlayerCamera {
        get {
            if (_cameraRef != null)
                return _cameraRef;
            else {
                Console.WriteLine("Player does not have a camera");
                return null;
            }
        }
        set {
            if (_cameraRef != null)
            {
                _cameraRef.LateDestroy(); // removes the previous camera to add a new one
                Console.Write("Warning! - replacing a camera from a player");
            }
            _cameraRef = value;
            AddChild(value);
        }
    }

    public Player(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows);
        AddChild(visuals);
        AddHitBox();
    }

    public void AddCamera(int x, int y, int width, int height) {
        PlayerCamera = new Camera(x, y, width, height);
    }
}
