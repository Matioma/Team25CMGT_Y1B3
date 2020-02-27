using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;


public enum UnitState
{
    IDLE,
    RUN,
    IN_AIR,
    JUMPED,
    STUNNED
}
public abstract class Unit : ArcadeObject,IControllable
{
    public float dx = 0;
    public float dy = 0;

    public bool isLookingRight =true;


    protected int MaxSpeedY = 5;
    private const int  Gravity= 1;

    private int defaultMaxSpeed = 0;
    public int DefaultMaxSpeed {
        get {
            return defaultMaxSpeed;
        }
        set {
            defaultMaxSpeed = value;
            ActualMaxSpeed = defaultMaxSpeed;
        }
    }
    public int ActualMaxSpeed { get; set;}
    protected int speedY = 0;


    private bool onGround = false;
    public bool OnGround {
        get { return onGround; }
        set {
            onGround = value;
            if (OnGround == false)
            {
                if (stateMachine.CurrentState != UnitState.STUNNED) {
                    stateMachine.CurrentState = UnitState.IN_AIR;
                }
                
                //playerState = UnitState.IN_AIR;
            }
            else {

            }
            
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

    public StateMachine stateMachine;

    //List<Tile> _tiles = new List<Tile>();

    public static List<Tile> _tiles = new List<Tile>();

    public Unit() {
        stateMachine = new StateMachine(this);
    }


    public override void Update() {
        base.Update();

        var collision = hitBox.MoveUntilCollision(0, speedY, VerticalTilesToConsider());
        if (collision != null)
        {
            if (collision.other.y < y)
                OnGround = true;
            else
                OnGround = false;
        }
        else
        {
            OnGround = false;
        }

        hitBox.MoveUntilCollision(dx, 0f, HorizontalTilesToConsider());
        



        ///Update Unit position
        Vector2 worldPosition = hitBox.TransformPoint(hitBox.x, hitBox.y);
        SetXY(worldPosition.x, worldPosition.y);
        hitBox.SetXY(0, 0);
        //ApplyGravity();


    }


    /// <summary>
    /// Moves player
    /// </summary>
    public virtual void RotateWheel( bool pRight){
        if (stateMachine.CurrentState != UnitState.STUNNED)
        {

            stateMachine.CurrentState = UnitState.RUN;
            if (pRight)
            {
                dx = ActualMaxSpeed;
                isLookingRight = true;
            }
            else
            {
                dx = -ActualMaxSpeed;
                isLookingRight = false;
            }
        }
    }

    public virtual void PressJumpButton() {
        if (stateMachine.CurrentState != UnitState.STUNNED) {
            if (OnGround)
            {
                speedY = JumpForce;
                AudioManager.Instance.PlaySound("Audio/Jumping.wav");
            }
        }

       
        
    }

    public void ApplyGravity() {
        if (speedY < MaxSpeedY)
        {
            speedY += Gravity;
        }
        else {
            speedY = MaxSpeedY;
        }
    }

    virtual public void PressPowerUpButton() {
      
        Console.WriteLine("used powerup");
    }



    private GameObject[] HorizontalTilesToConsider()
    {

        List<HitBox> hitBoxes = new List<HitBox>();
        foreach (var obj in _tiles)
        {
            //Tile below
            if ((obj.y - y) < 0)
            {
                //tile Left
                if ((obj.x - x) < 0)
                {
                    if ((Math.Abs(obj.y - y) > MaxSpeedY) && (Math.Abs(obj.x - x) <= Tile.tileWidth + hitBox.width / 2 + ActualMaxSpeed))
                    {
                        hitBoxes.Add(obj.getHitBox());
                    }
                }
                //tile right
                else
                {
                    if ((Math.Abs(obj.y - y) > MaxSpeedY) && (Math.Abs(obj.x - x) <= hitBox.width / 2 + ActualMaxSpeed))
                    {
                        hitBoxes.Add(obj.getHitBox());
                    }
                }
            }
            else if((obj.y - y)<0)
            {
                //left
                if ((obj.x - x) < 0)
                {
                    if ((Math.Abs(obj.y - y) > Tile.tileHeight + hitBox.height + JumpForce) && (Math.Abs(obj.x - x) <= Tile.tileWidth + hitBox.width / 2 + ActualMaxSpeed))
                    {
                        hitBoxes.Add(obj.getHitBox());
                    }
                }
                else
                {
                    if ((Math.Abs(obj.y - y) > Tile.tileHeight + hitBox.height + JumpForce) && (Math.Abs(obj.x - x) <= hitBox.width / 2 + ActualMaxSpeed))
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
        List<HitBox> hitBoxes = new List<HitBox>();
        foreach (var obj in _tiles)
        {
            //if tile on the left
            if ((obj.x - x) < 0)
            {
                if ((Math.Abs(obj.x - x) < Tile.tileWidth + getHitBox().width / 2 - 10) && ((obj.y - y) <= dy+1))
                {
                    hitBoxes.Add(obj.getHitBox());
                }
            }
            else
            {
                if ((Math.Abs(obj.x - x) <= getHitBox().width / 2 - 10) && ((obj.y - y) <= dy+1))
                {
                    hitBoxes.Add(obj.getHitBox());
                }
            }

        }

        return hitBoxes.ToArray();
    }
}

