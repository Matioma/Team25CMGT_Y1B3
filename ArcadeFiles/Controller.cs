using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Controller:GameObject
{
    Player _target;

    Player ControlledTarget {
        get {
            if (_target != null) {
                return _target;
            }
            else {
                throw new Exception("Controller does not controll anything");
            }
        }
        set {
            _target = value;
        }
    }

    public Controller(Player ptarget) {
        ControlledTarget = ptarget;
    }

    public void Update()
    {
        Console.WriteLine("XD");
        //this.x += 10;
    }


}

