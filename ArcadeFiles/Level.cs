using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.ArcadeFiles.PowerUps;

using TiledMapParser;

public class Level:GameObject
{
 
    short[,] tiledDataShort;

    Map levelData;
    MainMenu mainMenu;


    //List<Controller> controllersList = new List<Controller>();


    public List<ArcadeObject> playersList = new List<ArcadeObject>();

    public Level()
    {
   
    }

    public Level(string tiledFile) {
        levelData = MapParser.ReadMap(tiledFile);
        
        Tile.tileHeight = levelData.TileWidth;
        Tile.tileWidth = levelData.TileHeight;
        
        SpawnTiles(levelData);
        SpawnObjects(levelData);

    }


    /// <summary>
    /// Parses Tiles to the level
    /// </summary>
    /// <param name="levelData"></param>
    public void SpawnTiles(Map levelData)
    {
        if (levelData.Layers == null || levelData.Layers.Length == 0)
        {
            return;
        }

        Layer mainLayer = levelData.Layers[0];
        tiledDataShort = mainLayer.GetTileArray();

        var tileSet = levelData.TileSets[0];
        

        for (int i = 0; i < mainLayer.Height; i++)
        {
            for (int j = 0; j < mainLayer.Width; j++)
            {
                int tileNumber = tiledDataShort[j, i];
                if (tileNumber > 0)
                {
                    int numberOfColumns = tileSet.Columns;
                    int numberOfRows = tileSet.TileCount / tileSet.Columns;
                    var newTile = CreateTile(i, j, tileNumber, tileSet.Image.FileName, numberOfColumns, numberOfRows);
                    newTile.SetHitBoxSize(Tile.tileWidth, Tile.tileHeight);
                }
            }
        }
    }

    /// <summary>
    /// Adds a tile to the Level
    /// </summary>
    /// <param name="i">array index row</param>
    /// <param name="j">array index column</param>
    /// <param name="tileNumber"> Tileset tile id</param>
    private Tile CreateTile(int i, int j, int tileNumber, string tiledFile , int columns, int rows) {
        var tile = new Tile(tiledFile, columns, rows);
        tile.SetSpriteSheetFrame(tileNumber - 1);
        tile.SetXY(j * Tile.tileWidth, i * Tile.tileHeight);
        tile.SetSpriteExtent(Tile.tileWidth, Tile.tileHeight);
        AddChild(tile);

        return tile;
    }


    public void SpawnObjects(Map levelData)
    {
        if (levelData.ObjectGroups == null || levelData.ObjectGroups.Length == 0)
        {
            return;
        }
        ObjectGroup objectGroup = levelData.ObjectGroups[0];

        foreach (ObjectGroup objGroup in levelData.ObjectGroups) {
            if (objGroup.Objects == null || objectGroup.Objects.Length == 0)
            {
                continue;
            }

            //Console.WriteLine(objGroup.Name);

            switch (objGroup.Name.ToLower())
            {
                case "menu":
                    mainMenu = new MainMenu();
                    AddChild(mainMenu);
                    foreach (TiledObject obj in objGroup.Objects)
                    {
                        GameObject gameObject;
                        switch (obj.Type) //
                        {
                            case "UIImage":
                                gameObject= ParseUIImage(obj);
                                mainMenu.AddChild(gameObject);
                                break;
                            case "UIButton":
                                //gameObject = ParseUIButton(obj);

                                mainMenu.AddButton(ParseUIButton(obj));
                                //mainMenu.AddChild(gameObject);
                                break;
                            default:
                                Console.WriteLine("Unknown Type for menu Objects");
                                break;
                        }
                    }
                    MenuLayer(objGroup);
                    break;
                default:
                    DefaultLayerCreation(objGroup);
                    break;
            }



        }


        if (objectGroup.Objects == null || objectGroup.Objects.Length == 0)
        {
            return;
        }

        
    }

    private void DefaultLayerCreation(ObjectGroup objGroup)
    {
        foreach (TiledObject obj in objGroup.Objects)
        {
            switch (obj.Type) //
            {
                case "Player":
                    ParsePlayerData(obj);
                    break;
                case "Pill":
                    ParsePillData(obj);

                    break;
                case "MetalWheelPowerUp":
                    ParseMetalWheelData(obj);
                    break;
                case "UIImage":
                    ParseUIImage(obj);
                    break;
                case "UIButton":
                    ParseUIButton(obj);
                    break;
                default:
                    break;
            }
        }
    }

    void MenuLayer(ObjectGroup objGroup) {
        foreach (TiledObject obj in objGroup.Objects)
        {
            switch (obj.Type) //
            {
                case "UIImage":
                    ParseUIImage(obj);
                    break;
                case "UIButton":
                    ParseUIButton(obj);
                    break;
                default:
                    Console.WriteLine("Unknown Type for menu Objects");
                    break;
            }
        }
    }



    void ParsePlayerData(TiledObject obj) {
        string spriteSheet = "colors.png";
        int cols = 1, rows = 1;
        int maxSpeed = 0;
        int jumpForce = 0;

        var properties = obj.propertyList;
        foreach (Property property in obj.propertyList.properties)
        {
            switch (property.Name)
            {
                case "MaxSpeed":
                    maxSpeed = int.Parse(property.Value);
                    break;
                case "SpriteSheet":
                    spriteSheet = property.Value;
                    break;
                case "SpriteSheetColumns":
                    cols = int.Parse(property.Value);
                    break;
                case "SpriteSheetRows":
                    rows = int.Parse(property.Value);
                    break;
                case "JumpForce":
                    jumpForce = int.Parse(property.Value);

                    break;
            }
        }

        Player player = new Player(spriteSheet, cols, rows);
        player.SetXY(obj.X, obj.Y);
        player.DefaultMaxSpeed = maxSpeed;
        player.JumpForce = jumpForce;


        player.SetSpriteExtent((int)obj.Width, (int)obj.Height);

        player.SetPivotPoint(PivotPointPosition.BOTTOM);
        AddChild(player);

        playersList.Add(player);
    }

    void ParsePillData(TiledObject obj) {
        string spriteSheet = "colors.png";
        int cols = 1;
        int rows = 1;
        string Name = "";
        int speedBonus = 0;
        float speedDuration = 0;


        var properties = obj.propertyList;
        foreach (Property property in obj.propertyList.properties)
        {
            switch (property.Name)
            {
                case "SpriteSheet":
                    spriteSheet = property.Value;
                    break;
                case "SpriteSheetColumns":
                    cols = int.Parse(property.Value);
                    break;
                case "SpriteSheetRows":
                    rows = int.Parse(property.Value);
                    break;
                case "SpeedBonus":
                    speedBonus = int.Parse(property.Value);
                    break;
                case "SpeedDuration":
                    speedDuration = float.Parse(property.Value);
                    break;
            }
        }

        Pill pill = new Pill(spriteSheet, cols, rows);
        pill.SetXY(obj.X, obj.Y);
        pill.name = Name;
        pill.SpeedBonus = speedBonus;
        pill.PowerUpDuration = speedDuration;
        pill.SetPivotPoint(PivotPointPosition.BOTTOM);
        AddChild(pill);


    }

    void ParseMetalWheelData(TiledObject obj) {
        var spriteSheet = "colors.png";
        var cols = 1;
        var rows = 1;
        float metalWheelDuration = 0;

        var properties = obj.propertyList;
        foreach (Property property in obj.propertyList.properties)
        {
            switch (property.Name)
            {
                case "SpriteSheet":
                    spriteSheet = property.Value;
                    break;
                case "SpriteSheetColumns":
                    cols = int.Parse(property.Value);
                    break;
                case "SpriteSheetRows":
                    rows = int.Parse(property.Value);
                    break;
                case "MetalWheelDuration":
                    metalWheelDuration = float.Parse(property.Value);
                    break;
            }
        }
        MetalWheel metalWheel = new MetalWheel(spriteSheet, cols, rows);
        metalWheel.SetXY(obj.X, obj.Y);
        metalWheel.PowerUpDuration = metalWheelDuration;


        metalWheel.SetPivotPoint(PivotPointPosition.BOTTOM);
        AddChild(metalWheel);
    }

    UIElement ParseUIImage(TiledObject obj) {
        var spriteSheet = "colors.png";
        var cols = 1;
        var rows = 1;

        var properties = obj.propertyList;
        foreach (Property property in obj.propertyList.properties)
        {
            switch (property.Name)
            {
                case "BackgroundImage":
                    spriteSheet = property.Value;
                    break;
                case "SpriteSheetColumns":
                    cols = int.Parse(property.Value);
                    break;
                case "SpriteSheetRows":
                    rows = int.Parse(property.Value);
                    break;
            }
        }
        UIElement uiElement = new UIImage(spriteSheet, cols, rows);
        uiElement.SetXY(obj.X - Game.main.width/2, obj.Y - Game.main.height / 2);
        uiElement.background.width = (int)obj.Width;
        uiElement.background.height = (int)obj.Height;


        return uiElement;
    }

    UIButton ParseUIButton(TiledObject obj)
    {
        var spriteSheet = "colors.png";
        var cols = 1;
        var rows = 1;
        string targetLevel = "";

        var properties = obj.propertyList;
        foreach (Property property in obj.propertyList.properties)
        {
            switch (property.Name)
            {
                case "BackgroundImage":
                    spriteSheet = property.Value;
                    break;
                case "SpriteSheetColumns":
                    cols = int.Parse(property.Value);
                    break;
                case "SpriteSheetRows":
                    rows = int.Parse(property.Value);
                    break;
                case "TargetLevel":
                    targetLevel = property.Value;
                    break;
            }
        }
        UIButton uiElement = new UIButton(spriteSheet, cols, rows);
        uiElement.SetXY(obj.X - Game.main.width/2, obj.Y - Game.main.height / 2);
        uiElement.background.width = (int)obj.Width;
        uiElement.background.height = (int)obj.Height;
        uiElement.TargetLevel = targetLevel;


        return uiElement;
    }

}
