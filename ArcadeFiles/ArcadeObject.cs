using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

abstract class ArcadeObject : GameObject
{
    protected HitBox hitBox;
    protected AnimationSprite visuals;

    private PivotPointPosition _pivotPointPosition =PivotPointPosition.LEFT_TOP;


    public virtual void Update() {
       
        Vector2 worldPosition = hitBox.TransformPoint(hitBox.x, hitBox.y);
        SetXY(worldPosition.x, worldPosition.y);
        hitBox.SetXY(0, 0);
        //SetSpritePivotPoint(hitBox, _pivotPointPosition);
        /*if (hitBox.parent is Player)
        {
            Console.WriteLine(x + ":" + y);
            hitBox.SetXY(-visuals.width / 2, -visuals.height);
        }*/
          
        //SetPivotPoint(_pivotPointPosition);
    }


    //Adds animation sprite to the object
    protected void CreateVisual(string spriteSheet, int cols, int rows) {
        visuals = new AnimationSprite(spriteSheet, cols, rows);
        AddChild(visuals);
    }

    //Adds collider to the object
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
    public void SetSpriteSheetIndex(int index)
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
                    hitBox.SetXY(0, 0);
                    visuals.SetXY(0, 0);
                    break;
                case PivotPointPosition.LEFT_CENTER:
                    hitBox.SetXY(0, -visuals.height / 2);
                    visuals.SetXY(0, -visuals.height / 2);
                    break;
                case PivotPointPosition.LEFT_BOTTOM:
                    hitBox.SetXY(0, -visuals.height);
                    visuals.SetXY(0, -visuals.height);
                    break;
                case PivotPointPosition.TOP:
                    hitBox.SetXY(-visuals.width / 2, 0);
                    visuals.SetXY(-visuals.width / 2, 0);
                    break;
                case PivotPointPosition.CENTER:
                    hitBox.SetXY(-visuals.width / 2, -visuals.height / 2);
                    visuals.SetXY(-visuals.width / 2, -visuals.height / 2);
                    break;
                case PivotPointPosition.BOTTOM:
                    hitBox.SetXY(-visuals.width / 2, -visuals.height);
                    visuals.SetXY(-visuals.width / 2, -visuals.height);
                    break;
                case PivotPointPosition.RIGHT_TOP:
                    hitBox.SetXY(-visuals.width, 0);
                    visuals.SetXY(-visuals.width, 0);
                    break;
                case PivotPointPosition.RIGHT_CENTER:
                    hitBox.SetXY(-visuals.width, -visuals.height / 2);
                    visuals.SetXY(-visuals.width, -visuals.height / 2);
                    break;
                case PivotPointPosition.RIGHT_BOTTOM:
                    hitBox.SetXY(-visuals.width, -visuals.height);
                    visuals.SetXY(-visuals.width, -visuals.height);
                    break;
            }
        }
    }

    public void SetSpritePivotPoint(Sprite sprite, PivotPointPosition pivotPointPosition)
    {
        if (sprite == null)
        {
            throw new Exception("Imposible to set the pivot point to a null Sprite");
        }
        //Setting the pivot points
        else
        {
            //_pivotPointPosition = pivotPointPosition;
            switch (pivotPointPosition)
            {
                case PivotPointPosition.LEFT_TOP:
                    sprite.SetXY(0, 0);
                    break;
                case PivotPointPosition.LEFT_CENTER:
                    sprite.SetXY(0, -visuals.height / 2);
                    break;
                case PivotPointPosition.LEFT_BOTTOM:
                    sprite.SetXY(0, -visuals.height);
                    break;
                case PivotPointPosition.TOP:
                    sprite.SetXY(-visuals.width / 2, 0);
                    break;
                case PivotPointPosition.CENTER:
                    sprite.SetXY(-visuals.width / 2, -visuals.height / 2);
                    break;
                case PivotPointPosition.BOTTOM:
                    sprite.SetXY(-visuals.width/2, -visuals.height);
                    break;
                case PivotPointPosition.RIGHT_TOP:
                    sprite.SetXY(-visuals.width, 0);
                    break;
                case PivotPointPosition.RIGHT_CENTER:
                    sprite.SetXY(-visuals.width, -visuals.height / 2);
                    break;
                case PivotPointPosition.RIGHT_BOTTOM:
                    sprite.SetXY(-visuals.width, -visuals.height);
                    break;
            }
        }
    }
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
