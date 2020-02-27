using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class BackgroundSprite : Sprite
    {
        float _speed = 0.0f;

        public BackgroundSprite(string filename, int layerIndex) : base(filename, true, false)
        {
            layerIndex += 1;
            _speed = 1.0f / layerIndex;
            game.OnAfterStep += LateUpdate;
        }

        public void LateUpdate()
        {
            x = -game.x * _speed;
        }
    }
}