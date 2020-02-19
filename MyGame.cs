using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine
using GXPEngine.Core;
using TiledMapParser;
public class MyGame : Game
{
   

    GameManager gameManager;
    public MyGame() : base(1920, 1080, false)		// Create a window that's 800x600 and NOT fullscreen
    {
        //Settings.Initialize(); // Loads And Parses settings file

        gameManager = new GameManager();
        
       


        AddChild(gameManager);
    }


    static void Main()							// Main() is the first method that's called when the program is run
    {
        new MyGame().Start();					// Create a "MyGame" and start it
    }

}