using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mucho_Mungus.Components;
using Mucho_Mungus.Components.EntityMovement;
using Mucho_Mungus.Components.Interactions;
using Mucho_Mungus.Constants;
using Mucho_Mungus.Entities.Actions;
using Mucho_Mungus.Entities.Constants;
using Mucho_Mungus.Entities.Munguses;
using Mucho_Mungus.Scenes.Constants;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
using System;
using System.Collections.Generic;

namespace Mucho_Mungus.Entities
{
    public class Player : Character
    {

        public BaseMungus pet;

        public Player(String name) : base(name)
        {
            position = new Vector2(200, 200);
            //Allow us to move between areas
            this.addComponent<AreaTransitioner>();
        }

        public override void onAddedToScene()
        {
            //Set up animations and controls per scene
            List<Subtexture> subtexture = SetUpTextureAtlas("Characters/NakedMan");
            Animation = this.addComponent(new Sprite<MovementAnimations>(subtexture[0]));
            SetUpMovementAnimations(subtexture);
            this.addComponent(new PlayerMover(Animation));


            var collisions = scene.findEntity(EntityNames.Map).getComponent<TiledMapComponent>().tiledMap.getLayer<TiledTileLayer>(MapLayerNames.Blockers);
            this.addComponent(new TiledMapMover(collisions));
            this.addComponent(new BoxCollider(12, 16));

            this.addComponent<Interactor>();


            //Set as camera focus
            this.addComponent<CameraMover>();

        }

        public override void onRemovedFromScene()
        {
            this.removeComponent<PlayerMover>();
            this.removeComponent<Sprite>();
            this.removeComponent<TiledMapMover>();
            this.removeComponent<BoxCollider>();
            this.removeComponent<CameraMover>();

        }
        
        
        public void disableActions()
        {
            getComponent<EntityMover>().pauseMovement();
            getComponent<Interactor>().blockNewInteractions();
        }
        public void enableActions()
        {
            getComponent<EntityMover>().resumeMovement();
            getComponent<Interactor>().allowNewInteractions();
        }

    }

}
