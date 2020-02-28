using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Enemy : AnimationSprite
{
    const int ENEMY_SIZE_WIDTH = 64;
    const int ENEMY_SIZE_HEIGHT = 128;

    float _speedX = 0;
    float _speedY = 0;

    int _animationDrawBetweenFrames;
    int _step;

    private Level _level;

    //GameObject _target;

    public Enemy(Level level, float x, float y) : base("img/objects/thug-idle.png", 5, 1)
    {
        _level = level;
        SetXY(x, y);
        width = ENEMY_SIZE_WIDTH;
        height = ENEMY_SIZE_HEIGHT;
        _animationDrawBetweenFrames = 6;
        _step = 0;
        _speedX = 5;
        Mirror(true, false);
    }

    //public void SetTarget(GameObject target)
    //{
    //    _target = target;
    //}

    void Update()
    {
        Gravity();
        Idle();
        MovementToPlayer();
    }

    /// <summary>
    /// Takes care of Idle animation
    /// </summary>
    private void Idle()
    {
        if (currentFrame == 4)
        {
            SetFrame(0);
        }

        _step += 1;

        if (_step > _animationDrawBetweenFrames)
        {
            NextFrame();
            _step = 0;
        }
    }

    int stepCount = 0;

    /// <summary>
    /// This takes care of the enemy movement
    /// </summary>
    private void MovementToPlayer()
    {
        Moving(-_speedX, 0.0f);
        stepCount++;

        if (stepCount > 40) 
        {
            stepCount = 0;
            _speedX = -_speedX;
            _mirrorX = !_mirrorX;
        }
        
    }

    /// <summary>
    /// Takes care of enemy gravity
    /// </summary>
    private void Gravity()
    {
        _speedY++;

        if (!Moving(0, _speedY))
        {
            _speedY = 0;
        }
    }

    /// <summary>
    /// This takes care of checking if the enemy is moving or not
    /// </summary>
    /// <param name="moveX"> needs an x movement parameter </param>
    /// <param name="moveY"> needs an y movement parameter </param>
    /// <returns> Returns a true or false if the player moves or not </returns>
    private bool Moving(float moveX, float moveY)
    {
        bool isSuccess = true;
        x += moveX;
        y += moveY;

        foreach (GameObject other in _level.GetOverlaps(this))
        {
            //if we went right, place ourself at the left of 'other'
            //if we went left, place ourself at the right of 'other'
            //if we went down .. etc etc
            isSuccess = false;
        }
        if (!isSuccess)
        {
            x -= moveX; //simple resolve, there are better ways
            y -= moveY;
        }
        return isSuccess;
    }

    void OnCollision(GameObject other)
    {

    }
}