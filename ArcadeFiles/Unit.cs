using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

abstract class Unit : ArcadeObject
{
    protected int dx = 0;
    protected int dy = 0;

    protected int MaxSpeedY = 5;
    private const int  Gravity= 1;

    public int MaxSpeed { get; set; } = 0;
    protected int speedY = 0;


    protected bool onGround = false;
    public bool OnGround {
        get { return onGround; }
        set {
            onGround = value;
        }
    }

    protected int jumpForce = 5;
    public int JumpForce {
        get { return jumpForce; }
        set {
            if (value > 0)
            {
                jumpForce = -value;
            }
            else {
                jumpForce = value;
            }
        }

    }


    List<Tile> _tiles = new List<Tile>();


    public override void Update() {
        
        var collision = hitBox.MoveUntilCollision(0, speedY, VerticalTilesToConsider());
        if (collision != null)
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }

        hitBox.MoveUntilCollision(dx, 0f, HorizontalTilesToConsider());
        dx = 0;



        ///Update Unit position
        Vector2 worldPosition = hitBox.TransformPoint(hitBox.x, hitBox.y);
        SetXY(worldPosition.x, worldPosition.y);
        hitBox.SetXY(0, 0);
        ApplyGravity();
    }


    /// <summary>
    /// Moves player
    /// </summary>
    public void MoveRight(){
        dx = MaxSpeed;
    }
    public void MoveLeft(){
        dx = -MaxSpeed;
    }
    public void Jump() {
        if (OnGround)
        {
            speedY = JumpForce;
        }
        
    }

    void ApplyGravity() {
        if (speedY < MaxSpeedY)
        {
            speedY += Gravity;
        }
        else {
            speedY = MaxSpeedY;
        }
    }

    public void UsePowerUp(int controller) {
        Console.WriteLine(controller + "-used powerup");

    }



    private GameObject[] HorizontalTilesToConsider()
    {

        _tiles = new List<Tile>();
        foreach (var gameObject in parent.GetChildren())
        {
            if (gameObject is Tile)
            {
                _tiles.Add(gameObject as Tile);
            }

        }

        List<HitBox> hitBoxes = new List<HitBox>();
        foreach (var obj in _tiles)
        {


            //Tile below
            if ((obj.y - y) < 0)
            {
                //tile Left
                if ((obj.x - x) < 0)
                {
                    if ((Math.Abs(obj.y - y) > 1) && (Math.Abs(obj.x - x) <= Tile.tileWidth + hitBox.width / 2 + 1))
                    {
                        hitBoxes.Add(obj.getHitBox());
                    }
                }
                //tile right
                else
                {
                    if ((Math.Abs(obj.y - y) > 1) && (Math.Abs(obj.x - x) <= hitBox.width / 2 + 1))
                    {
                        hitBoxes.Add(obj.getHitBox());
                    }
                }
            }
            else
            {
                //left
                if ((obj.x - x) < 0)
                {
                    if ((Math.Abs(obj.y - y) > Tile.tileHeight + hitBox.height + 1) && (Math.Abs(obj.x - x) <= Tile.tileWidth + hitBox.width / 2 + 1))
                    {
                        hitBoxes.Add(obj.getHitBox());
                    }
                }
                else
                {
                    if ((Math.Abs(obj.y - y) > Tile.tileHeight + hitBox.height + 1) && (Math.Abs(obj.x - x) <= hitBox.width / 2 + 1))
                    {
                        hitBoxes.Add(obj.getHitBox());
                    }

                }
            }
        }

        return hitBoxes.ToArray();
    }
    private GameObject[] VerticalTilesToConsider()
    {
        _tiles = new List<Tile>();
        foreach (var gameObject in parent.GetChildren())
        {
            if (gameObject is Tile)
            {
                _tiles.Add(gameObject as Tile);
            }
        }

        List<HitBox> hitBoxes = new List<HitBox>();
        foreach (var obj in _tiles)
        {
            //if tile on the left
            if ((obj.x - x) < 0)
            {
                if ((Math.Abs(obj.x - x) < Tile.tileWidth + getHitBox().width / 2 - 10) && ((obj.y - y) <= 5))
                {
                    hitBoxes.Add(obj.getHitBox());
                }
            }
            else
            {
                if ((Math.Abs(obj.x - x) <= getHitBox().width / 2 - 10) && ((obj.y - y) <= 5))
                {
                    hitBoxes.Add(obj.getHitBox());
                }
            }

        }

        return hitBoxes.ToArray();
    }
}

