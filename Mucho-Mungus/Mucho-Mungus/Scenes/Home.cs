using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Mucho_Mungus.Content.Characters;
using Nez;
using Nez.Tiled;

namespace Mucho_Mungus.Scenes
{
    public class Home : ExtendedScene
    {
        
        public override void initialize()
        {

            clearColor = Color.DarkOliveGreen;
            addRenderer(new DefaultRenderer());

            var map = this.content.Load<TiledMap>("basiclevel");
            var mapEntity = this.createEntity("map");
            var mapComponent = mapEntity.addComponent(new TiledMapComponent(map, "blockers"));
            
            this.camera.zoomIn(2);
            

        }

        

        public override List<SceneChangeMapper> getTransitions()
        {
            if (transitions.Count == 0)
            {
                transitions.Add(new SceneChangeMapper(new Vector2(180, 464), new Vector2(300, 500), new NextLevel()));
            }

            return transitions;
        }
    }
}
