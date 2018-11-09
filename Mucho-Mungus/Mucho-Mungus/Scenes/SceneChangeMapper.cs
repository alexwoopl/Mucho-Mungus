using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Scenes
{
    public class SceneChangeMapper
    {
        public Vector2 min, max;
        public Scene sceneName;

        public SceneChangeMapper(Vector2 min, Vector2 max, Scene sceneName)
        {
            this.min = min;
            this.max = max;
            this.sceneName = sceneName;
        }
    }
}
