using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Enemy : Sprite
{
    public const int ENEMY_SIZE = 64;

    float _speedX = 0;
    float _speedY = 0;

    public Enemy(float x, float y) : base("img/objects/triangle.png")
    {
        SetXY(x, y);
        width = ENEMY_SIZE;
        height = ENEMY_SIZE;
    }

    void Update()
    {
        Gravity();
    }

    /// <summary>
    /// This takes care of the enemy movement
    /// </summary>
    private void Movement()
    {
        ///TODO: Add enemy movement once he detects player
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

        foreach (GameObject other in GetCollisions())
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
