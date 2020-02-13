using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class Level:GameObject
{
    int _tileWidth = 0;
    int _tileHeight = 0;
 
    short[,] tiledDataShort;

    Map levelData;

    List<Controller> controllersList = new List<Controller>();

    public Level()
    {
   
    }

    public Level(string tiledFile) {
        levelData = MapParser.ReadMap(tiledFile);
        _tileWidth = levelData.TileWidth;
        _tileHeight = levelData.TileHeight;

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
                    newTile.SetHitBoxSize(_tileWidth, _tileHeight);
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
        tile.SetXY(j * _tileWidth, i * _tileHeight);
        tile.SetSpriteExtent(_tileWidth, _tileHeight);
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
                    int cols=0, rows=0;
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
                    player.MaxSpeed = maxSpeed;
                    player.JumpForce = jumpForce;


                    player.SetPivotPoint(PivotPointPosition.BOTTOM);
                    AddChild(player);
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

                case "PowerUp":
                    spriteSheet = "colors.png";
                    cols = 1;
                    rows = 1;
                    string Name = "";

                    properties = obj.propertyList;
                    foreach (Property property in obj.propertyList.properties)
                    {
                        switch (property.Name)
                        {
                            case "Name":
                                Name = property.Value;
                                break;
                        }
                    }

                    PowerUp powerUp = new PowerUp(spriteSheet, cols, rows);
                    powerUp.SetXY(obj.X, obj.Y);
                    powerUp.name = Name;
                    powerUp.SetPivotPoint(PivotPointPosition.BOTTOM);
                    AddChild(powerUp);

                    break;
            }
        }
    }



}
