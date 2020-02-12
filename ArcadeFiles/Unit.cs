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



    public override void Update() {
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
}

