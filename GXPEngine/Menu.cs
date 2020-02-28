using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;

public class Menu : Sprite
{
    bool _hasLoaded;

    Sound _menuMusic;

    public Menu() : base("img/backgrounds/menu-screen.png", addCollider: false)
    {
        _menuMusic = new Sound("sounds/menu_music.mp3", true, false);
        _menuMusic.Play();
    }

    void Update()
    {

    }

    public bool HasGameStarted()
    {
        return _hasLoaded;
    }

    public Level GetLevel()
    {
        return _level;
    }

    Level _level;

    public void StartGame(string mapName)
    {
        if (Input.GetKeyDown(Key.Z) && _hasLoaded == false || Input.GetKeyDown(Key.X) && _hasLoaded == false)
        {
            _hasLoaded = true;
            _level = new Level(mapName);
            _menuMusic.Play(true);
            GetLevel();
            AddChild(_level);
        }
    }
}
