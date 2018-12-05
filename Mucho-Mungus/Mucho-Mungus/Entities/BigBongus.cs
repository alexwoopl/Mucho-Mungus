using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mucho_Mungus.Components;
using Mucho_Mungus.Components.EntityMovement;
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
    public class BigBongus : Entity
    {

        private Sprite<MovementAnimations> Animation { get; set; }

        public BigBongus(String name) : base(name)
        {
            
            position = new Vector2(150, 150);

            //Allow us to move between areas
            this.addComponent<AreaTransitioner>();


        }

        public override void onAddedToScene()
        {
            //Set up animations and controls per scene
            List<Subtexture> subtexture = SetUpTextureAtlas(scene);
            Animation = this.addComponent(new Sprite<MovementAnimations>(subtexture[0]));
            SetUpAnimations(subtexture);
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
            this.removeComponent<CameraMover>();

        }


        private static List<Subtexture> SetUpTextureAtlas(Scene scene)
        {
            // load up the TextureAtlas that we generated with the Pipeline tool specifying individual files
            var textureAtlas = scene.content.Load<Texture2D>("characters");


            // fetch a Subtexture from the atlas. A Subtexture consists of the Texture2D and the rect on the Texture2D this particular image ended up
            var subtexture = Subtexture.subtexturesFromAtlas(textureAtlas, 16, 16);
            return subtexture;
        }

        private void SetUpAnimations(List<Subtexture> subtexture)
        {
            Animation.renderLayer = (int)RenderLayerIds.Second;
            Animation.addAnimation(MovementAnimations.Down, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[3],
                subtexture[4],
                subtexture[5],
            }));

            Animation.addAnimation(MovementAnimations.Up, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[39],
                subtexture[40],
                subtexture[41],
            }));

            Animation.addAnimation(MovementAnimations.Left, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[15],
                subtexture[16],
                subtexture[17],
            }));

            Animation.addAnimation(MovementAnimations.Right, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[27],
                subtexture[28],
                subtexture[29],
            }));
        }
    }

}

