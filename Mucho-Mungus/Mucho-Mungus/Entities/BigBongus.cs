﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mucho_Mungus.Components;
using Mucho_Mungus.Components.EntityMovement;
using Mucho_Mungus.Components.Interactions;
using Mucho_Mungus.Constants;
using Mucho_Mungus.Entities.Actions;
using Mucho_Mungus.Entities.Constants;
using Mucho_Mungus.Scenes.Constants;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
using System;
using System.Collections.Generic;

namespace Mucho_Mungus.Entities
{
    public class BigBongus : Character
    {
        
        public BigBongus(String name) : base(name)
        {
            position = new Vector2(150, 150);
            targetPosition = new Point(16, 15);
            //Allow us to move between areas
            this.addComponent<AreaTransitioner>();
        }

        public override void onAddedToScene()
        {
            //Set up animations and controls per scene
            List<Subtexture> subtexture = SetUpTextureAtlas("Characters/Man");
            Animation = this.addComponent(new Sprite<MovementAnimations>(subtexture[0]));
            SetUpMovementAnimations(subtexture);
            this.addComponent(new NPCMover(Animation));


            var collisions = scene.findEntity(EntityNames.Map).getComponent<TiledMapComponent>().tiledMap.getLayer<TiledTileLayer>(MapLayerNames.Blockers);
            this.addComponent(new TiledMapMover(collisions));
            this.addComponent(new BoxCollider(12, 16));

            this.addComponent<Interactable>();
            this.setTag((int)EntityTags.Interactable);

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

