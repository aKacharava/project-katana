using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;

public class Player : AnimationSprite
{
    const int JUMP_HEIGHT = -18;
    const int PLAYER_SIZE_WIDTH = 64;
    const int PLAYER_SIZE_HEIGHT = 128;

    float _accelerationSpeed = +0.3f;
    float _speedX = 0;
    float _speedY = 0;

    bool _moveRight;
    bool _moveLeft;
    bool _dashing;
    bool _attacking;

    int _animationDrawBetweenFrames;
    int _step;

    Sprite _hitbox;
    public Player(float x, float y) : base("img/objects/player.png", 28, 1)
    {
        SetXY(x, y);
        width = PLAYER_SIZE_WIDTH;
        height = PLAYER_SIZE_HEIGHT;
        _animationDrawBetweenFrames = 6;
        _step = 0;

        _hitbox = new Sprite("img/objects/colors.png");
        _hitbox.alpha = 0.0f;
        AddChild(_hitbox);
        _hitbox.width = 133;
        _hitbox.height = 261;
    }

    ///float targetLevelX = 0.0f;

    void Update()
    {
        Idle();
        Attack();
        Dashing();
        Movement();
        Gravity();
        CameraFollowPlayer();
    }

    private void CameraFollowPlayer()
    {
        ///----Room to room
        //targetLevelX = -Mathf.Floor(x / 1280) * 1280; //eerst afronden, dan vermenigvuldigen
        //game.x = game.x * 0.9f + targetLevelX * 0.1f;

        ///----Classic
        if (x + game.x > 700)
        {
            game.x = 700 - x;
        }
        if (x + game.x < 300)
        {
            game.x = 300 - x;
        }
        if (game.x > 0)
        {
            game.x = 0;
        }

        game.y = -y;

        if (game.y <= game.y / 2)
        {
            game.y = game.y / 2 + 64;
        }
        if (game.y > 0)
        {
            game.y = 0;
        }
        
    }

    /// <summary>
    /// Takes care of Idle animation
    /// </summary>
    private void Idle()
    {
        if (_moveLeft == false && _moveRight == false)
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
    }

    private void Walking()
    {
        if (_moveLeft == true || _moveRight == true)
        {
            if (currentFrame == 16)
            {
                SetFrame(5);
            }

            _step += 1;

            if (_step > _animationDrawBetweenFrames)
            {
                NextFrame();
                _step = 0;
            }
        }
    }

    /// <summary>
    /// This takes care of the player movement
    /// </summary>
    private void Movement()
    {
        /// Player movement
        if (Input.GetKey(Key.LEFT) && _moveRight == false)
        {
            _moveLeft = true;
            Mirror(true, false);
            accelerate(_accelerationSpeed);
            Moving(-_speedX, 0);

            if (x <= 0)
            {
                x = 0;
            }
        }
        else if (Input.GetKey(Key.RIGHT) && _moveLeft == false)
        {
            _moveRight = true;
            Mirror(false, false);
            accelerate(_accelerationSpeed);
            Moving(_speedX, 0);
        }
        else
        {
            _moveRight = false;
            _moveLeft = false;
            _speedX = 0;
        }
    }

    /// <summary>
    /// Takes care of attacking
    /// </summary>
    private void Attack()
    {
        float tempX = x;

        if (Input.GetKeyDown(Key.X))
        {
            _attacking = true;
            if (_moveRight == true)
            {
                x += 10;
            }
            else if (_moveLeft == true)
            {
                x -= 10;
            }
        }
        else
        {
            _attacking = false;
            x = tempX;
        }
    }

    /// <summary>
    /// Takes care of dashing
    /// </summary>
    private void Dashing()
    {
        if (Input.GetKeyDown(Key.C))
        {
            if (_moveRight == true)
            {
                Moving(100, 0);
                _dashing = true;
                x += 50;
            }
            else if (_moveLeft == true)
            {
                Moving(-100, 0);
                _dashing = true;
                x -= 50;
            }
            else
            {
                _dashing = false;
            }
        }
    }

    /// <summary>
    /// Takes care of player gravity
    /// </summary>
    private void Gravity()
    {
        _speedY++;
        bool hasLanded = false;
        if (!Moving(0, _speedY))
        {
            if (_speedY > 0)
            {
                hasLanded = true;
            }
            _speedY = 0;
        }

        /// Checks if the player has landed and then can jump by pressing the up key
        if (hasLanded && Input.GetKeyDown(Key.Z))
        {
            _speedY = JUMP_HEIGHT;
            //SetFrame(18);
            //_animationDrawBetweenFrames = 0;
        }
    }

    /// <summary>
    /// This takes care of checking if the player is moving or not
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
            if (other == _hitbox)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }


        }
        if (!isSuccess)
        {
            x -= moveX; //simple resolve, there are better ways
            y -= moveY;
        }
        return isSuccess;
    }

    /// <summary>
    /// Takes care of acceleration of the player
    /// </summary>
    /// <param name="accSpd"> needs a value of acceleration speed</param>
    private void accelerate(float accSpd)
    {
        if (_speedX >= 7)
        {
            _speedX = 7;
        }
        else
        {
            _speedX += accSpd;
        }
    }

    /// <summary>
    /// Respawns player
    /// </summary>
    private void SpawnPlayer()
    {
        x = 100;
        y = 100;
    }

    /// <summary>
    /// Respawn an enemy 
    /// </summary>
    /// <param name="other"></param>
    private void Respawn(GameObject other)
    {
        Random random = new Random();
        other.x = random.Next(300, 1000);
        other.y = 600;
    }

    /// <summary>
    /// Takes care of screen shaking 
    /// </summary>
    private void ScreenShake()
    {
        if (_attacking == true || _dashing == true)
        {
            game.x = Utils.Random(-10, 10);
            game.y = Utils.Random(-10, 10);
        }
    }

    void OnCollision(GameObject other)
    {
        if (other is Enemy)
        {
            if (_attacking == true || _dashing == true)
            {
                Respawn(other);
            }
            else
            {
                SpawnPlayer();
            }
        }
    }
}
