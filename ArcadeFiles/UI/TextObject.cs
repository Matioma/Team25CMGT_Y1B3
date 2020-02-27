using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;

using GXPEngine;
class TextObject : GameObject
{
    EasyDraw easyDraw;

    public TextObject(string message, int x , int y, int width, int height, int rotation) {
        easyDraw = new EasyDraw(width,height);
        this.x = x;
        this.y = y;
        AddChild(easyDraw);
        DrawText(message,rotation);
    }

    void DrawText(string text,int rotation) {
        var _foo = new PrivateFontCollection();
        _foo.AddFontFile("STENCIL.TTF");
        easyDraw.TextFont(new Font((FontFamily)_foo.Families[0], 60f));
        easyDraw.rotation= rotation;
        //easyDraw.TextSize(50);
        easyDraw.Fill(0);
        easyDraw.Text(text, 150, 150);
    }

}

