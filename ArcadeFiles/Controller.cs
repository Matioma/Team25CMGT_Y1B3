using System;

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
    }
    public Controller(Player ptarget)
    {
        ControlledTarget = ptarget;
        controllerId = ControllersNumber;
        ControllersNumber++;
    }

    public void Update()
    {
        switch (controllerId) {
            case 0:
                if (Input.GetKey(Key.D)) {
                    _target.MoveHorizontally(true);
                   
                }
                if (Input.GetKey(Key.A))
                {
                    _target.MoveHorizontally(false);
                    //_target.MoveLeft();
                }
                if (Input.GetKeyDown(Key.W)) {
                    _target.Jump();
                }
                if (Input.GetKeyDown(Key.S)) {
                    _target.UsePowerUp();

                }
                break;
            case 1:
                if (Input.GetKey(Key.RIGHT))
                {
                    _target.MoveHorizontally(true);
                }
                if (Input.GetKey(Key.LEFT))
                {
                    _target.MoveHorizontally(false);
                }
                if (Input.GetKeyDown(Key.UP))
                {
                    _target.Jump();
                }
                if (Input.GetKeyDown(Key.DOWN))
                {
                    _target.UsePowerUp();
                }
                break;
        }
    }

}