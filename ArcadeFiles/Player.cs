using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Player:ArcadeObject
{

    Camera camera;
    public Player(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows);
        AddChild(visuals);
        AddHitBox();
        camera = new Camera(0, 0, Game.main.width / 2, Game.main.height);
        AddChild(camera);
    }



    void Update() {
        this.x += 10;
    }
}
