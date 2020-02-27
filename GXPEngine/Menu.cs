using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Menu : GameObject
{
    bool _hasLoaded;

    public Menu()
    {
        
    }

    void Update()
    {

    }

    public void StartGame()
    {
        if (Input.GetKeyDown(Key.Z) && _hasLoaded == false || Input.GetKeyDown(Key.X) && _hasLoaded == false)
        {
            _hasLoaded = true;
            Level _level = new Level("levels/placeholder-level.tmx");
            AddChild(_level);
        }
    }
}
