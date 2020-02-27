using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
public class InventorySlot:GameObject
{
    string defaultSprite = "colors.png";

    AnimationSprite background;
    AnimationSprite visuals;

    public int _index= 0;
    public int ControllerId
    {
        get { return _index; }
        set
        {
            if (value == 0) {
                SetXY(-6000,-6000);
            }
            else{
                SetXY(-2000, -2000);
            }
            _index = value;
        }
    }

    public int xPos;
    public int yPos;
    public int pWidth;
    public int pHeight;

    public InventorySlot(int px,int py, int pwidth,int pheight) {
        xPos = px;
        yPos = py;
        pWidth = pwidth;
        pHeight = pheight;


        background = new AnimationSprite(defaultSprite, 1,1,-1,false);

        background.width = pwidth;
        background.height = pheight;
        AddChild(background);



        //Add viewPort
        var arcadeCamera = new ArcadeCamera(px, py, pwidth, pheight);
        arcadeCamera.SetXY(pwidth/2,pheight/2);
        this.AddChild(arcadeCamera);


        //visuals = new AnimationSprite("art/colors.png",1,1);
        //AddChild(visuals);
    }

    public void SetVisuals(PowerUp powerUp) {


        if (visuals != null)
        {
            visuals.LateRemove();
        }
        if (powerUp != null) { 
            if (powerUp is AcidBottle)
            {
                visuals = new AnimationSprite("art/popup_acid.png", 1, 1, -1, false);
            }
            Console.WriteLine(powerUp.GetType());

            if (visuals != null)
            {
                visuals.width = pWidth;
                visuals.height = pHeight;
                AddChild(visuals);
            }
        }

        
       
    }

}
