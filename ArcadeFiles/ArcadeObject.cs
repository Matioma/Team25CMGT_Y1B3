using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

public abstract class ArcadeObject : GameObject
{
    protected HitBox hitBox;
    public AnimationSprite visuals;
    public HitBox getHitBox() { return hitBox; }


    private PivotPointPosition _pivotPointPosition =PivotPointPosition.LEFT_TOP;


    private int animationTimer=0;


    public virtual void Update() {
    }

    /// <summary>
    /// Adds animation sprite to the object
    /// </summary>
    /// <param name="spriteSheet">file path</param>
    /// <param name="cols">number of columns</param>
    /// <param name="rows">number of rows</param>
    protected void CreateVisual(string spriteSheet, int cols, int rows) {
        visuals = new AnimationSprite(spriteSheet, cols, rows);
        AddChild(visuals);
    }


    /// <summary>
    /// Adds collider to the object
    /// </summary>
    protected void AddHitBox()
    {
        if (hitBox == null)
        {
            hitBox = new HitBox();
            AddChild(hitBox);
        }
        else {
            throw new Exception("tried to add aditional hitBox to ArcadeObject");
        }
    }
    protected void AddHitBox( int width, int height)
    {
        if (hitBox == null)
        {
            hitBox = new HitBox(width,height);
            AddChild(hitBox);
        }
        else
        {
            throw new Exception("tried to add aditional hitBox to ArcadeObject");
        }
    }


    //Sets the size of the animation sprite
    public void SetSpriteExtent(int width, int height)
    {
        visuals.width = width;
        visuals.height = height;
        SetHitBoxSize(width, height);
    }

    public void ScaleXY(float value) {
        visuals.width = (int)(visuals.width* value);
        visuals.height = (int)(visuals.height * value);
    }

    public void SetSpriteSheetFrame(int index)
    {
        visuals.SetFrame(index);
    }


    public void Animate(int millisPerFrame) {
        animationTimer -= Time.deltaTime;

        if (animationTimer <= 0) {
            animationTimer = millisPerFrame;
            visuals.NextFrame();
        }
    }
    public void Animate(int millisPerFrame, int frameStart, int frameCount, bool animBackwards)
    {
        animationTimer -= Time.deltaTime;
        if (animationTimer <= 0)
        {
            
            if (animBackwards)
            {
                if (visuals.currentFrame == 0)
                {
                    visuals.SetFrame(frameStart + frameCount);
                }
                visuals.SetFrame((visuals.currentFrame - 1 + frameStart) % frameCount + frameStart);
            }
            else {
                visuals.SetFrame((visuals.currentFrame + 1 + frameStart) % frameCount + frameStart);
            }
            animationTimer = millisPerFrame;
        }
    }



    public void SetHitBoxSize(int width, int height) {
        hitBox.setHitBoxSize(new Vector2(width, height));
    }




    /// <summary>
    /// Sets the pivot point based on preffered option
    /// </summary>
    /// <param name="pivotPointPosition"> Enum value</param>
    public void SetPivotPoint(PivotPointPosition pivotPointPosition)
    {
        if (hitBox == null || visuals == null)
        {
            throw new Exception("You have forgot to instantiate hitBox or Visuals in ArcadeObject");
        }
        //Setting the pivot points
        else
        {
            _pivotPointPosition = pivotPointPosition;
            switch (pivotPointPosition)
            {
                case PivotPointPosition.LEFT_TOP:
                    
                    hitBox.SetOrigin(0, 0);
                    visuals.SetOrigin(0, 0);
                    break;
                case PivotPointPosition.LEFT_CENTER:
                    hitBox.SetOrigin(0, visuals.height / 2);
                    visuals.SetOrigin(0, visuals.height / 2);
                    break;
                case PivotPointPosition.LEFT_BOTTOM:
                    hitBox.SetOrigin(0, visuals.height-1);
                    visuals.SetOrigin(0, visuals.height);
                    break;
                case PivotPointPosition.TOP:
                    hitBox.SetOrigin(visuals.width / 2, 0);
                    visuals.SetOrigin(visuals.width / 2, 0);
                    break;
                case PivotPointPosition.CENTER:
                    hitBox.SetOrigin(visuals.width / 2, visuals.height / 2);
                    visuals.SetOrigin(visuals.width / 2, visuals.height / 2);
                    break;
                case PivotPointPosition.BOTTOM:
                    hitBox.SetOrigin(visuals.width / 2, visuals.height);
                    //visuals.SetOrigin(visuals.width / 2, visuals.height);
                    visuals.SetXY(-visuals.width / 2, -visuals.height);
                    break;
                case PivotPointPosition.RIGHT_TOP:
                    //hitBox.SetOrigin(visuals.width, 0);
                    //visuals.SetOrigin(visuals.width, 0);
                    break;
                case PivotPointPosition.RIGHT_CENTER:
                    hitBox.SetOrigin(visuals.width, visuals.height / 2);
                    visuals.SetOrigin(visuals.width, visuals.height / 2);
                    break;
                case PivotPointPosition.RIGHT_BOTTOM:
                    hitBox.SetOrigin(visuals.width, visuals.height);
                    visuals.SetOrigin(visuals.width, visuals.height);
                    break;
            }
        }
    }


    virtual public void CollidedWith(GameObject other) { }


    /// <summary>
    /// Checks collision with a list of gameobjects
    /// </summary>
    /// <param name="gameObjects">list of object to check collisions with</param>
    protected virtual void DoCollisionCheck(List<ArcadeObject> gameObjects) {}
}

public enum PivotPointPosition
{
    LEFT_TOP,
    LEFT_CENTER,
    LEFT_BOTTOM,
    TOP,
    CENTER,
    BOTTOM,
    RIGHT_TOP,
    RIGHT_CENTER,
    RIGHT_BOTTOM,
}
