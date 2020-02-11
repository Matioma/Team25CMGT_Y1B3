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


        Controller controller1;

        Player player1 = new Player("colors.png", 1, 1);
        player1.SetXY(50, 50);
        player1.AddCamera(0,0, Game.main.width/2, Game.main.height);
        AddChild(player1);

        controller1 = new Controller(player1,0);
        AddChild(controller1);

        player1 = new Player("colors.png", 1, 1);
        player1.SetXY(150, 150);
        player1.AddCamera(Game.main.width / 2, 0, Game.main.width / 2, Game.main.height);
        AddChild(player1);

        controller1 = new Controller(player1, 1);
        AddChild(controller1);
    }

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

    public void AddTileToLevel(int i, int j, int tileNumber) {
        var tile = new Tile("tiles.png", 5, 1);
        tile.setSpriteSheetIndex(tileNumber - 1);
        tile.SetXY(j * _tileWidth, i * _tileHeight);
        tile.setSpriteExtent(_tileWidth, _tileHeight);
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
