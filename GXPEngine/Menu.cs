using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;

public class Menu : GameObject
{
    bool _hasLoaded;

    public Menu()
    {
        //graphics.DrawString("PRESS 'JUMP' TO START", Brushes.White, 0, 0);
    }

    void Update()
    {

    }

    public void StartGame()
    {
        if (Input.GetKeyDown(Key.Z) && _hasLoaded == false || Input.GetKeyDown(Key.X) && _hasLoaded == false)
        {
            _hasLoaded = true;
            Level _level = new Level("levels/casino.tmx");
            AddChild(_level);
        }
    }
}
