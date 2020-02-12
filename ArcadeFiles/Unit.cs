using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

abstract class Unit : ArcadeObject
{
    protected int speedX = 0;
    protected int MaxSpeedY = 4;

    public int MaxSpeed {
        get{ return speedX; }
        set { speedX = value; }
    }
    protected int speedY = 0;


    protected int _jumpForce = 0;
    public int JumpForce {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }


    protected bool onGround = false;

   


    public virtual void Update()
    {
        Vector2 worldPosition = hitBox.TransformPoint(hitBox.x, hitBox.y);
        if (onGround) {
            SetXY(worldPosition.x, worldPosition.y - 1);
        }else { 
            SetXY(worldPosition.x, worldPosition.y);
            //Console.WriteLine("On Ground");
        }
        hitBox.SetXY(0, 0);
        ApplyGravity();
    }

    public void MoveRight()
    {
        hitBox.MoveUntilCollision(speedX, 0f);
        //Console.WriteLine(speedX);
    }
    public void MoveLeft()
    {
        hitBox.MoveUntilCollision(-speedX, 0f);
    }

    public void Jump() {
        if (onGround)
        {
            speedY = JumpForce;

            Console.WriteLine(speedY);
            Console.WriteLine("JUMP");
        }
        
    }

    void ApplyGravity() {
        if (speedY < MaxSpeedY)
        {
            speedY += 2;
        }
        else {
            //speedY = MaxSpeedY;
        }
       //speedY += 1;
    }

    public void UsePowerUp(int controller) {
        Console.WriteLine(controller + "-used powerup");
    }
}

