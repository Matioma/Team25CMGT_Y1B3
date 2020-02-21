using System;

using GXPEngine;
class Controller:GameObject
{
    static int ControllersNumber = 0;
    Player _target;
    public int controllerId;


    ArcadeCamera _cameraRef = null;
    ArcadeCamera ConrollerCamera
    {
        get
        {
            if (_cameraRef != null)
                return _cameraRef;
            else
            {
                Console.WriteLine("Player does not have a camera");
                return null;
            }
        }
        set
        {
            if (_cameraRef != null)
            {
                _cameraRef.LateDestroy(); // removes the previous camera to add a new one
                Console.Write("Warning! - replacing a camera from a player");
            }
            _cameraRef = value;
            AddChild(value);
        }
    }


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

    public Controller()
    {
       controllerId = ControllersNumber;


       if (controllerId == 0)
       {
           AddCamera(0, 0, Game.main.width / 2, Game.main.height);
       }
       else
       {
           AddCamera(Game.main.width / 2, 0, Game.main.width / 2, Game.main.height);
       }
       ControllersNumber++;
    }


    public void AddCamera(int x, int y, int width, int height)
    {
        ConrollerCamera = new ArcadeCamera(x, y, width, height);
    }
    public void Update()
    {
        if (parent is IControllable)
        {
            IControllable conrolledObject = parent as IControllable;
            if (parent is Player)
            {
                switch (controllerId)
                {
                    case 0:
                        if (Input.GetKey(Key.D))
                        {
                            conrolledObject.MoveHorizontally(true);
                        }
                        if (Input.GetKey(Key.A))
                        {
                            conrolledObject.MoveHorizontally(false);
                        }
                        if (Input.GetKeyDown(Key.W))
                        {
                            conrolledObject.Jump();
                        }
                        if (Input.GetKeyDown(Key.S))
                        {
                            conrolledObject.UsePowerUp();

                        }
                        break;
                    case 1:
                        if (Input.GetKey(Key.RIGHT))
                        {
                            conrolledObject.MoveHorizontally(true);
                        }
                        if (Input.GetKey(Key.LEFT))
                        {
                            conrolledObject.MoveHorizontally(false);
                        }
                        if (Input.GetKeyDown(Key.UP))
                        {
                            conrolledObject.Jump();
                        }
                        if (Input.GetKeyDown(Key.DOWN))
                        {
                            conrolledObject.UsePowerUp();
                        }
                        break;
                }
            }
        } else
        {
            Console.WriteLine("Parent does not implements IControllable");
        }
    }

}