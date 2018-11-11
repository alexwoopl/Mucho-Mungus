using Microsoft.Xna.Framework;
using Mucho_Mungus.Entities;
using Nez;
using Nez.Tiled;
using System.Collections.Generic;

namespace Mucho_Mungus.Scenes
{
    public class NextLevel : ExtendedScene
    {

        public override List<SceneChangeMapper> getTransitions()
        {
            if (transitions.Count == 0)
            {
                transitions.Add(new SceneChangeMapper(new Vector2(180, 464), new Vector2(300, 500), new Home()));
            }

            return transitions;
        }

        internal override string getMapName()
        {
            return "nextLevel";
        }
    }
}
