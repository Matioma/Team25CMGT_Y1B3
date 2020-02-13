using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine
using GXPEngine.Core;
using TiledMapParser;
public class MyGame : Game
{
    public MyGame() : base(1720, 1080, false)		// Create a window that's 800x600 and NOT fullscreen
    {
        Settings.Initialize(); // Loads And Parses settings file


        //var levelData = MapParser.ReadMap("TestLevel.tmx");
        Level level = new Level("TestLevel2.tmx");

        
        //ArcadeObject obj= new ArcadeObject(new Vector2(150,150), new Vector2(32,32));

        AddChild(level);
    }

    void Update()
    {
        
    }

    static void Main()							// Main() is the first method that's called when the program is run
    {
        new MyGame().Start();					// Create a "MyGame" and start it
    }

    void LoadSettings() {

    }
}