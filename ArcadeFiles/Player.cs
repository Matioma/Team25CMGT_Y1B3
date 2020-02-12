using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

class Player : Unit
{
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

    List<Tile> tiles = new List<Tile>();


    public Player(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows,-1,false,false);
        AddChild(visuals);
        AddHitBox();


        
    }


    public override void Update() {
        base.Update();

        var collision = hitBox.MoveUntilCollision(0, speedY, VerticalTilesToConsider());
        if (collision != null)
        {
            //Console.WriteLine(collision.normal);
            //Vector2 normal = collision.normal;
            //if (normal.x == 0 && normal.y == -1)
            //{
                OnGround = true;
            //}
            //else
            //{
              //  OnGround = false;
            //}

        }
        else {
            OnGround = false;
        }

        

        //Console.WriteLine(tilesBelow.Count);
        hitBox.MoveUntilCollision(dx, 0f, HorizontalTilesToConsider());
        dx = 0;
    }

    public void AddCamera(int x, int y, int width, int height) {
        PlayerCamera = new Camera(x, y, width, height);
    }

    private GameObject[] HorizontalTilesToConsider() {

        tiles = new List<Tile>();
        foreach (var gameObject in parent.GetChildren()) {
            if (gameObject is Tile) {
                tiles.Add(gameObject as Tile);
            }

        }

        List<HitBox> hitBoxes = new List<HitBox>();
        foreach (var obj in tiles) {


            //Tile below
            if ((obj.y - y) < 0)
            {
                //tile Left
                if ((obj.x - x) < 0)
                {
                    if ((Math.Abs(obj.y - y) > 1) && (Math.Abs(obj.x - x) <= Tile.tileWidth+ hitBox.width / 2 + 1))
                    {
                        hitBoxes.Add(obj.getHitBox());
                    }
                }
                //tile right
                else {
                    if ((Math.Abs(obj.y - y) > 1) && (Math.Abs(obj.x - x) <= hitBox.width / 2 + 1))
                    {
                        hitBoxes.Add(obj.getHitBox());
                    }
                }
            }
            else
            {
                if ((Math.Abs(obj.y - y) <= getHitBox().height / 2 - 10) && ((obj.x - x) <= 5))
                {
                    hitBoxes.Add(obj.getHitBox());
                }
            }

            /*if (Math.Abs(obj.y - y)>= Tile.tileHeight) {
                hitBoxes.Add(obj.getHitBox());
            }*/
        }

        return hitBoxes.ToArray() ;
    }

    private GameObject[] VerticalTilesToConsider()
    {
        tiles = new List<Tile>();
        foreach (var gameObject in parent.GetChildren())
        {
            if (gameObject is Tile)
            {
                tiles.Add(gameObject as Tile);
            }
        }

        List<HitBox> hitBoxes = new List<HitBox>();
        foreach (var obj in tiles)
        {
            //if tile on the left
            if ((obj.x - x) < 0)
            {
                if ((Math.Abs(obj.x - x) < Tile.tileWidth +getHitBox().width/2 - 10) && ((obj.y - y) <= 5))
                {
                    hitBoxes.Add(obj.getHitBox());
                }
            }
            else {
                if ((Math.Abs(obj.x - x) <= getHitBox().width / 2-10) && ((obj.y - y) <= 5))
                {
                    hitBoxes.Add(obj.getHitBox());
                }
            }

        }

        return hitBoxes.ToArray();
    }
}
