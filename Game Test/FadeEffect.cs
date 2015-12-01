using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Game_Test
{
    public class FadeEffect
    {
        float FadeSpeed;
        bool fadingIn;
        float alpha;
        float alpha_min;

        public FadeEffect(float fadeSpeed)
        {
            this.FadeSpeed = fadeSpeed;
            this.alpha = 1.0f;
            this.fadingIn = false;
        }

        public FadeEffect(float fadeSpeed, float alpha_start)
        {
            this.FadeSpeed = fadeSpeed;
            this.alpha = alpha_start;
            this.fadingIn = false;
        }

        public FadeEffect(float fadeSpeed, float alpha_start, float alpha_min)
        {
            this.FadeSpeed = fadeSpeed;
            this.alpha = alpha_start;
            this.fadingIn = false;
            this.alpha_min = alpha_min;
        }

        public float Update(GameTime gameTime)
        {
            if (fadingIn == true)
            {

                alpha += FadeSpeed * (float)(gameTime.ElapsedGameTime.TotalSeconds);
                if (alpha >= 1.0f)
                    fadingIn = false;
                return alpha;
            }
            else
            {
                alpha -= FadeSpeed * (float)(gameTime.ElapsedGameTime.TotalSeconds);
                if (alpha <= alpha_min)
                    fadingIn = true;
                return alpha;
            }
        }
    }
}
