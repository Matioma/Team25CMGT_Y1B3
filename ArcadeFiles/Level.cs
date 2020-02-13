using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.ArcadeFiles.PowerUps;
using GXPEngine.Core;
using TiledMapParser;

public class Level:GameObject
{
 
    short[,] tiledDataShort;

    Map levelData;

    List<Controller> controllersList = new List<Controller>();


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
        //tile.SetPivotPoint(PivotPointPosition.TOP);
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

        if (objectGroup.Objects == null || objectGroup.Objects.Length == 0)
        {
            return;
        }

        foreach (TiledObject obj in objectGroup.Objects)
        {
            switch (obj.Type) //
            {
                case "Player":
                    string spriteSheet = "colors.png";
                    int cols=1, rows=1;
                    int maxSpeed=0;
                    int jumpForce = 0;

                    var properties = obj.propertyList;
                    foreach (Property property in obj.propertyList.properties) {
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


                    player.SetPivotPoint(PivotPointPosition.BOTTOM);
                    AddChild(player);

                    playersList.Add(player);
                    Controller controller = new Controller(player);
                    AddChild(controller);


                    //Adding Cameras
                    if (controller.controllerId == 0)
                    {
                        player.AddCamera(0, 0, Game.main.width / 2, Game.main.height);
                    }
                    else {
                        player.AddCamera(Game.main.width / 2, 0, Game.main.width / 2, Game.main.height);
                    }

                    break;

                case "Pill":
                    spriteSheet = "colors.png";
                    cols = 1;
                    rows = 1;
                    string Name = "";
                    int speedBonus = 0;
                    float speedDuration = 0; 


                    properties = obj.propertyList;
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
                    pill.SpeedDuration = speedDuration;
                    pill.SetPivotPoint(PivotPointPosition.BOTTOM);
                    AddChild(pill);

                    break;
                case "MetalWheelPowerUp":
                    spriteSheet = "colors.png";
                    cols = 1;
                    rows = 1;
                    float metalWheelDuration = 0;

                    properties = obj.propertyList;
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
                    metalWheel.MetalWheelTime = metalWheelDuration;


                    metalWheel.SetPivotPoint(PivotPointPosition.BOTTOM);
                    AddChild(metalWheel);

                    break;
            }
        }
    }



}
