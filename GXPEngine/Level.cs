using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
using System.IO;

public class Level : GameObject
{
    
    public Level(string filename)
    {
        String pathName = Path.GetDirectoryName(filename);
        Map _leveldata = MapParser.ReadMap(filename);
        PlaceImageLayer(_leveldata, pathName);
        SpawnTiles(_leveldata);
        SpawnObjects(_leveldata);
    }

    /// <summary>
    /// Add an image layer
    /// </summary>
    /// <param name="leveldata"></param>
    /// <param name="pathName"></param>
    private void PlaceImageLayer(Map leveldata, string pathName)
    {
        if (leveldata.ImageLayers == null || leveldata.ImageLayers.Length == 0)
            return;

        ImageLayer _imageLayer = leveldata.ImageLayers[0];

        if (_imageLayer.Image == null)
            return;

        string imageFilename = Path.Combine(pathName, _imageLayer.Image.FileName);
        AddChild(new Sprite(imageFilename, true, false));
    }

    /// <summary>
    /// Spawns objects to their positions
    /// </summary>
    /// <param name="leveldata"> This needs a leveldata map to read out of to get the data of the objects </param>
    private void SpawnObjects(Map leveldata)
    {
        if (leveldata.ObjectGroups == null || leveldata.ObjectGroups.Length == 0)
            return;

        ObjectGroup _objectGroup = leveldata.ObjectGroups[0];

        if (_objectGroup.Objects == null || _objectGroup.Objects.Length == 0)
            return;

        foreach (TiledObject obj in _objectGroup.Objects)
        {
            switch (obj.Name)
            {
                case "Player":
                    Player _player = new Player(obj.X, obj.Y);
                    AddChild(_player);
                    break;
                case "Guard":
                    Enemy _enemy = new Enemy(obj.X, obj.Y);
                    AddChild(_enemy);
                    break;
            }
        }
    }

    /// <summary>
    /// Spawns tiles to their positions
    /// </summary>
    /// <param name="leveldata"> This needs a leveldata map to read out of to get the data of the tiles </param>
    private void SpawnTiles(Map leveldata)
    {
        if (leveldata.Layers == null || leveldata.Layers.Length == 0)
            return;

        Layer _mainLayer = leveldata.Layers[0];
        short[,] _tileNumbers = _mainLayer.GetTileArray();

        for (int row = 0; row < _mainLayer.Height; row++)
        {
            for (int col = 0; col < _mainLayer.Width; col++)
            {
                int _tileNumber = _tileNumbers[col, row];
                TileSet _tiles = leveldata.GetTileSet(_tileNumber);

                string _filenameTiles = _tiles.Image.FileName;
                _filenameTiles = _filenameTiles.Remove(0, 3);
                if (_tileNumber > 0)
                {
                    CollisionTile _tile = new CollisionTile(_filenameTiles, _tiles.Columns, _tiles.Rows);
                    _tile.SetFrame(_tileNumber - _tiles.FirstGId);
                    _tile.x = col * _tile.width;
                    _tile.y = row * _tile.height;
                    AddChild(_tile);
                }
            }
        }
    }
}