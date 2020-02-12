using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Controller:GameObject
{
    static int ControllersNumber = 0;
    Player _target;
    public int controllerId;

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
        controllerId = ControllersNumber;
        ControllersNumber++;
        //controllerId = pControllerId;
    }
    public Controller(Player ptarget)
    {
        ControlledTarget = ptarget;
        controllerId = ControllersNumber;
        ControllersNumber++;
        //controllerId = pControllerId;
    }

    public void Update()
    {
        switch (controllerId) {
            case 0:
                if (Input.GetKey(Key.D)) {

                    //_target.
                    _target.MoveRight();
                   
                }
                if (Input.GetKey(Key.A))
                {
                    _target.MoveLeft();
                }
                if (Input.GetKeyDown(Key.W)) {
                    _target.Jump();
                }
                if (Input.GetKeyDown(Key.S)) {
                    _target.UsePowerUp(controllerId);

                }
                break;
            case 1:
                if (Input.GetKey(Key.RIGHT))
                {
                    _target.MoveRight();
                }
                if (Input.GetKey(Key.LEFT))
                {
                    _target.MoveLeft();
                }
                if (Input.GetKeyDown(Key.UP))
                {
                    _target.Jump();
                }
                if (Input.GetKeyDown(Key.DOWN))
                {
                    _target.UsePowerUp(controllerId);
                }
                break;
        }
    }

}