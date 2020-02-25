using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class GameManager:GameObject
{
    Level activeLevel;
    public Level ActiveLevel{
        get { return activeLevel; }
    }

    static GameManager _instance = null;
    public static GameManager Instance {
        get {
            if (_instance == null) {
                throw new Exception("There is no instance of gameManger class make sure it has beed added in MyGame class");
            }

            return _instance;
        }
        set {
            if (_instance == null)
            {
                _instance = value;
            }
            else {
                Console.WriteLine("GameManger instance already exists");
            }

        }


    }
    public GameManager() {
        Settings.Initialize();


        activeLevel = new Level("MainMenu.tmx");

        AddChild(activeLevel);

        _instance = this;
    }
    public void OpenLevel(string path) {
        Controller.ControllersNumber = 0;
        activeLevel.LateDestroy();
        activeLevel = new Level(path);

        AddChild(activeLevel);
        
    }

}

