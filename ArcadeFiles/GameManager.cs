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
    AudioManager audioManager;
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
        audioManager = new AudioManager();
        //audioManager.AddBackgroundSound("Using_powerup.mp3");
       


        activeLevel = new Level("MainMenu.tmx");

        AddChild(activeLevel);

        _instance = this;
    }

    void Update()
    {

    }
    public void OpenLevel(string path) {
        Controller.ControllersNumber = 0;
        activeLevel.LateDestroy();
        activeLevel = new Level(path);

        AddChild(activeLevel);
        
    }

}

