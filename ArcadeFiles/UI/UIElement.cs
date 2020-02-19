using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

abstract class UIElement : GameObject
{
    public AnimationSprite background;



    public UIElement(string fileName, int cols, int rows)
    {
        SetBackground(fileName, cols, rows);
    }



    public void SetBackground(string fileName, int cols, int rows)
    {
        if (background != null) {
            background.LateRemove();
        }
        background = new AnimationSprite(fileName, cols, rows);
        AddChild(background);
    }
}