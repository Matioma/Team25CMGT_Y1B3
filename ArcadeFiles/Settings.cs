using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Settings
{

    private const string settingsFileName = "settings.txt";
    private static Settings _instance;
    public static Settings Instance{
        get{
            if (_instance == null) {
                Console.WriteLine("Settings have not been Initilized");
                Initialize();
            }
            return _instance;
            
        }
        set {
            if (_instance != null)
            {
                Console.WriteLine("Tried To create extra Settings object ");
            }
            else {
                _instance = value;
            }
        }
    }


    /// <summary>
    /// Initilizes
    /// </summary>
    public static void Initialize()
    {
        Instance = new Settings(settingsFileName);
    }

    Settings(string fileName) {
        string[] lines = System.IO.File.ReadAllLines(settingsFileName);
        ParseSettings(lines);
    }
    
    
    
    /// <summary>
    /// Parses all the lines from the Settings file to relevant Class fields
    /// </summary>
    /// <param name="data">All lines from the settings files as and string array</param>
    void ParseSettings(string[] data)
    {
        foreach (string line in data) {
            //Standartize the data
            string settingLine = line.ToLower();
            settingLine = settingLine.Replace(" ", String.Empty);
            settingLine = settingLine.Replace("\t", "");
            //Ingnore empty lines and 
            if (settingLine == "" || settingLine[0] == '/' || settingLine[1] == '/') {
                continue;
            }
         

        //Get the Settings Fields
        string[] settingsLine = settingLine.Split(new char[]{'=','/'});

        //Parse fields to settings
        switch (settingsLine[0]) {
            case "debugmode":
                Console.WriteLine(settingsLine[1]+"ok");

                if (settingsLine[1] == "on")
                {
                    HitBox.CollisionDebugging = true;
                }
                else {
                    HitBox.CollisionDebugging = false;
                        
                }
                break;
            case "collideralpha":
                float value = float.Parse(settingsLine[1]);
                HitBox.ColliderAlpha = value;
                break;
            default:
                break;
            }
        }
    }
}