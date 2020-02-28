using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;

public class HUD : Canvas
{
    private Player _player;
    public HUD(Player player) : base(128, 64)
    {
        _player = player;
    }

    void Update()
    {
        graphics.Clear(Color.Empty);
        graphics.DrawString("Lives:  aksjdakjsdhaksd", SystemFonts.MenuFont, Brushes.White, 0, 0);
        //if (_player.GetLives() == 0)
        //{
        //    Level _level = new Level(null);
        //    if (_level == null)
        //    {
        //        _level.Destroy(); // Destroys level
        //        graphics.Clear(Color.Empty);
        //        graphics.DrawString("Game Over (Press R to reset)", SystemFonts.MenuFont, Brushes.White, 0, 0);
        //    }
        //}
    }
}