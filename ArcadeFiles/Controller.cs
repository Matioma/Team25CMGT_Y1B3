using System;

using GXPEngine;
class Controller:GameObject
{
    public static int ControllersNumber = 0;
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

    public Controller(int numberOfCameras)
    {
       controllerId = ControllersNumber;

        switch (numberOfCameras) {
            case 0:
                throw new Exception("Controller with no cameras Created");
                break;
            case 1:
                AddCamera(0, 0, Game.main.width, Game.main.height);
                ControllersNumber++;
                break;
            case 2:
                if (controllerId == 0)
                {
                    AddCamera(0, 0, Game.main.width / 2, Game.main.height);
                }
                else
                {
                    AddCamera(Game.main.width / 2, 0, Game.main.width / 2, Game.main.height);
                }
                break;
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
                          conrolledObject.RotateWheel(true);
                      }
                      if (Input.GetKey(Key.A))
                      {
                          conrolledObject.RotateWheel(false);
                      }
                      if (Input.GetKeyDown(Key.W))
                      {
                          conrolledObject.PressJumpButton();
                      }
                      if (Input.GetKeyDown(Key.S))
                      {
                          conrolledObject.PressPowerUpButton();

                      }
                      break;
                  case 1:
                      if (Input.GetKey(Key.RIGHT))
                      {
                          conrolledObject.RotateWheel(true);
                      }
                      if (Input.GetKey(Key.LEFT))
                      {
                          conrolledObject.RotateWheel(false);
                      }
                      if (Input.GetKeyDown(Key.UP))
                      {
                          conrolledObject.PressJumpButton();
                      }
                      if (Input.GetKeyDown(Key.DOWN))
                      {
                          conrolledObject.PressPowerUpButton();
                      }
                      break;
              }
          }
          else {
              if (Input.GetKey(Key.D) || Input.GetKey(Key.RIGHT))
              {
                  conrolledObject.RotateWheel(true);
              }
              if (Input.GetKey(Key.A) || Input.GetKey(Key.LEFT))
              {
                  conrolledObject.RotateWheel(false);
              }
              if (Input.GetKeyDown(Key.W) || Input.GetKeyDown(Key.UP))
              {
                  conrolledObject.PressJumpButton();
              }
              if (Input.GetKeyDown(Key.S) || Input.GetKeyDown(Key.DOWN))
              {
                  conrolledObject.PressPowerUpButton();
              }
          }
        }
        else
        {
            Console.WriteLine("Parent does not implements IControllable");
        }
    }

}