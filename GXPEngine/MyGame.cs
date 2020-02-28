using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using System.Reflection;
using GXPEngine;                                // GXPEngine contains the engine

public class MyGame : Game
{
    Menu _menu;
    Level _level;
    Sound _level1Music;

    int _levelSwitch;

    string _firstLevel = "levels/casino.tmx";
    string _secondLevel = "levels/market.tmx";
    string _thirdLevel1 = "levels/japan.tmx";
    string _thirdLevel2 = "levels/japan_2.tmx";
    public MyGame() : base(1280, 720, false, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        targetFps = 60;
        _levelSwitch = 0;
        _level1Music = new Sound("sounds/casino_music.mp3", true, false);
        _menu = new Menu();
        AddChild(_menu);
        //ResetLevel();
    }

    void Update()
    {
        if (_menu != null)
        {
            _menu.StartGame(_firstLevel);

            if (_menu.HasGameStarted() == true)
            {
                _level = _menu.GetLevel();
                _menu = null;
                _level1Music.Play(false);
            }
        }

        SwitchLevel();

        /// Press the 'R' key to reset the level
        /// 
        //if (Input.GetKey(Key.R))
        //{
        //    ResetLevel();
        //}
    }

    int kills;

    public void SwitchLevel()
    {
        if (_level == null)
            return;

        Type type = (typeof(Player));
        MethodInfo mInfo = type.GetMethod("GetKilledEnemies", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        int count = _level.GetAmountEnemy();
        //kills = mInfo.Invoke;

        if (count == kills)
        {
            _levelSwitch++;

            switch (_levelSwitch)
            {
                case 1:
                    _level = null;
                    _level = new Level(_secondLevel);
                    AddChild(_level);
                    break;
                case 2:
                    _level = null;
                    _level = new Level(_thirdLevel1);
                    AddChild(_level);
                    break;
                case 3:
                    _level = null;
                    _level = new Level(_thirdLevel2);
                    AddChild(_level);
                    break;
            }
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

        _level = new Level("levels/casino.tmx");
        AddChild(_level);
    }


    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}