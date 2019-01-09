using Microsoft.Xna.Framework;
using Mucho_Mungus.Components.EntityMovement;
using Mucho_Mungus.Entities.Actions;
using Mucho_Mungus.Entities.Constants;
using Mucho_Mungus.Scenes.Constants;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Entities.Munguses
{
    public class Spider : BaseMungus
    {
        public Spider(string name, Vector2 position) : base(name, position)
        {
        }

        public override void onAddedToScene()
        {
            //Set up animations and controls per scene
            List<Subtexture> subtexture = SetUpTextureAtlas("Characters/Spider");
            Animation = this.addComponent(new Sprite<MovementAnimations>(subtexture[0]));
            SetUpMovementAnimations(subtexture);
            this.addComponent(new NPCMover(Animation));


            var collisions = scene.findEntity(EntityNames.Map).getComponent<TiledMapComponent>().tiledMap.getLayer<TiledTileLayer>(MapLayerNames.Blockers);
            this.addComponent(new TiledMapMover(collisions));
            this.addComponent(new BoxCollider(12, 16));
            

        }


        public override void onRemovedFromScene()
        {
            this.removeComponent<NPCMover>();
            this.removeComponent<Sprite>();
            this.removeComponent<TiledMapMover>();
            this.removeComponent<BoxCollider>();

        }
    }
}
