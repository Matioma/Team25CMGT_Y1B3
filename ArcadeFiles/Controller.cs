using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Controller:GameObject
{
    Player _target;
    int controllerId;


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

    public Controller(Player ptarget, int pControllerId) {
        ControlledTarget = ptarget;
        controllerId = pControllerId;
    }

    public void Update()
    {
        switch (controllerId) {
            case 0:
                if (Input.GetKey(Key.D)) {
                    _target.Move(5,0);
                }
                if (Input.GetKey(Key.A))
                {
                    _target.Move(-5, 0);
                }
                break;
            case 1:
                if (Input.GetKey(Key.RIGHT))
                {
                    _target.Move(5, 0);
                }
                if (Input.GetKey(Key.LEFT))
                {
                    _target.Move(-5, 0);
                }
                break;
        }
    }
}

