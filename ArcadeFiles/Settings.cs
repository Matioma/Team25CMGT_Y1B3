using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Settings
{

    private static string settingsFileName = "settings.txt";

    private static Settings _instance;
    public static Settings Instance{
        get{
            if (_instance == null)
                Initialize();
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
    public static void Initialize()
    {
        Instance = new Settings(settingsFileName);
    }

    Settings(string fileName) {
        string[] lines = System.IO.File.ReadAllLines(settingsFileName);
        ParseSettings(lines);
    }

    void ParseSettings(string[] data)
    {
        foreach (string line in data) {
            //Standartize the data
            string settingLine = line.ToLower();
            settingLine = settingLine.Replace(" ", String.Empty);
            settingLine = settingLine.Replace("\t", "");
            //Ingnore Extra data
            if (settingLine == "") {
                continue;
            }
            else {
                if (settingLine[0] == '/' || settingLine[1] == '/')
                    continue;
            }

            //Get the Settings Fields
            string[] settingsLine = settingLine.Split(new char[]{'=','/'});

            /*foreach (var s in settingsLine) {
                Console.WriteLine(s);
            }*/


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
                    //Console.WriteLine("Unknown Command");
            break;
            }
        }
    }
}