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
    public void setSpriteExtent(int width, int height)
    {
        visuals.width = width;
        visuals.height = height;
    }

    public void setSpriteSheetIndex(int index)
    {
        visuals.SetFrame(index);
    }

    public void setHitBoxSize(int width, int height) {
        hitBox.setHitBoxSize(new Vector2(width, height));
    }

    void SetPivotPoint()
    {
        if (hitBox == null || visuals == null)
        {
            throw new Exception("You have forgot to instantiate hitBox or Visuals in ArcadeObject");
        }
        else
        {
            hitBox.SetXY(-visuals.width / 2,-visuals.height/2);
            visuals.SetXY(-visuals.width / 2,-visuals.height/2);
        }
    }
}
