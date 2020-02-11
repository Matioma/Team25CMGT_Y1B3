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
        
        SpawnTiles(levelData);

        var player =CreatePlayer(50, 150, 0);
        player.AddCamera(0, 0, Game.main.width / 2, Game.main.height);

        player=CreatePlayer(150, 200, 1);
        player.AddCamera(Game.main.width / 2, 0, Game.main.width / 2, Game.main.height);
        
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

        for (int i = 0; i < mainLayer.Height; i++)
        {
            for (int j = 0; j < mainLayer.Width; j++)
            {
                int tileNumber = tiledDataShort[j, i];
                if (tileNumber > 0)
                {
                    AddTileToLevel(i, j, tileNumber);
                    
                }
            }
        }
    }

    /// <summary>
    /// Creates a Player object and gives it a controller
    /// </summary>
    /// <param name="x">position x</param>
    /// <param name="y">position y</param>
    /// <param name="controlerIndex"> Owning controller Id</param>
    /// <returns></returns>
    private Player CreatePlayer(int x ,int y, int controlerIndex) {
        Player player1 = new Player("colors.png", 1, 1);
        player1.SetXY(x, y);

        player1.SetPivotPoint(PivotPointPosition.BOTTOM);
        AddChild(player1);

        Controller controller1 = new Controller(player1, controlerIndex);
        AddChild(controller1);

        return player1;
    }


    /// <summary>
    /// Adds a tile to the Level
    /// </summary>
    /// <param name="i">array index row</param>
    /// <param name="j">array index column</param>
    /// <param name="tileNumber"> Tileset tile id</param>
    public void AddTileToLevel(int i, int j, int tileNumber) {
        var tile = new Tile("tiles.png", 5, 1);
        tile.SetSpriteSheetIndex(tileNumber - 1);
        tile.SetXY(j * _tileWidth, i * _tileHeight);
        tile.SetSpriteExtent(_tileWidth, _tileHeight);
        AddChild(tile);
    }





    /*public void SpawnObjects(Map levelData)
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
            switch (obj.Name)
            {
                case "Rope":
                    Rope rope = new Rope();
                    rope.x = obj.X;
                    rope.y = obj.Y;
                    AddChild(rope);
                    break;
                case "BoxEnemy":
                    BoxEnemy boxEnemy = new BoxEnemy();
                    boxEnemy.x = obj.X;
                    boxEnemy.y = obj.Y;
                    AddChild(boxEnemy);
                    break;
                case "Player":
                    Player player = new Player(obj.X, obj.Y);
                    //player.x = ;
                    //player.y = obj.Y;
                    LevelManager.Instance.PlayerInstace = player;
                    break;
                case "AlligatorPuddle":
                    Console.WriteLine("SPAWNED");
                    AliggatorPuddle puddle = new AliggatorPuddle(tileSize * 4, obj.X, obj.Y);
                    AddChild(puddle);
                    break;
            }
        }
    }*/


    /*void AddTile(int id, int x, int y)
    {
        GameObject gameObject = null;
        switch (id)
        {
            case 0:
                break;
            case 1:
                var ground = new Ground();
                ground.SetHitBoxSize(tileSize, tileSize);
                gameObject = ground;

                break;
            case 2:
                gameObject = new Stairs();
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                throw new Exception("Unknow tile" + id + ";");
        }

        if (gameObject != null)
        {
            gameObject.SetXY(x, y);
            AddChild(gameObject);
        }*/
}
