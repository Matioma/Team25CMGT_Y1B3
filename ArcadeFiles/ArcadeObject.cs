﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

abstract class ArcadeObject : GameObject
{
    protected HitBox hitBox;
    protected AnimationSprite visuals;
    public HitBox getHitBox() { return hitBox; }


    private PivotPointPosition _pivotPointPosition =PivotPointPosition.LEFT_TOP;


    public virtual void Update() {
       
    }

    //Adds animation sprite to the object
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


    //Sets the size of the animation sprite
    public void SetSpriteExtent(int width, int height)
    {
        visuals.width = width;
        visuals.height = height;
    }
    public void SetSpriteSheetFrame(int index)
    {
        visuals.SetFrame(index);
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
                    visuals.SetOrigin(visuals.width / 2, visuals.height);
                    break;
                case PivotPointPosition.RIGHT_TOP:
                    hitBox.SetOrigin(visuals.width, 0);
                    visuals.SetOrigin(visuals.width, 0);
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
