using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;                                // GXPEngine contains the engine

public class MyGame : Game
{
    Level _level;
    public MyGame() : base(1920, 1080, false, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        targetFps = 60;
        ResetLevel();
    }

    void Update()
    {
        /// Press the 'R' key to reset the level
        if (Input.GetKey(Key.R))
        {
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        if (_level != null)
        {
            _level.Destroy(); // Destroys level
            _level = null;
            game.x = 0;
        }

        _level = new Level("levels/placeholder-level.tmx");
        AddChild(_level);
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}