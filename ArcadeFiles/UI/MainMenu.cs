using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using GXPEngine;
class MainMenu:GameObject,IControllable
{
    public MainMenu() {
        SetXY(Game.main.width/2, Game.main.height/2);
        AddChild(new Controller(1));
    }

    public void PressJumpButton()
    {
        Console.WriteLine("JUmpButton");

        //throw new NotImplementedException();
    }

    public void PressPowerUpButton()
    {
        Console.WriteLine("PowerUp");
        //throw new NotImplementedException();
    }

    public void RotateWheel(bool pRight)
    {
        Console.WriteLine("Wheel rotated");
        //throw new NotImplementedException();
    }
}
