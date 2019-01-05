using Microsoft.Xna.Framework;
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
    public class Player : Entity
    {


        public static int width = 16, height = 16;

        private Sprite<MovementAnimations> Animation { get; set; }

        public Player(String name) : base(name)
        {
            
            position = new Vector2(200, 200);

            //Allow us to move between areas
            this.addComponent<AreaTransitioner>();


        }

        public override void onAddedToScene()
        {

            //Set up animations and controls per scene
            List<Subtexture> subtexture = SetUpTextureAtlas(scene);
            Animation = this.addComponent(new Sprite<MovementAnimations>(subtexture[0]));
            SetUpAnimations(subtexture);
            this.addComponent(new PlayerMover(Animation));


            var collisions = scene.findEntity(EntityNames.Map).getComponent<TiledMapComponent>().tiledMap.getLayer<TiledTileLayer>(MapLayerNames.Blockers);
            this.addComponent(new TiledMapMover(collisions));
            this.addComponent(new BoxCollider(12, 16));

            this.addComponent<Interactor>();


            //Set as camera focus
            //scene.camera.addComponent(new FollowCamera(this));
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


        private static List<Subtexture> SetUpTextureAtlas(Scene scene)
        {
            // load up the TextureAtlas that we generated with the Pipeline tool specifying individual files
            var textureAtlas = scene.content.Load<Texture2D>("characters");
            

            // fetch a Subtexture from the atlas. A Subtexture consists of the Texture2D and the rect on the Texture2D this particular image ended up
            var subtexture = Subtexture.subtexturesFromAtlas(textureAtlas, width, height);
            return subtexture;
        }

        private void SetUpAnimations(List<Subtexture> subtexture)
        {
            Animation.renderLayer = (int)RenderLayerIds.First;
            Animation.addAnimation(MovementAnimations.Down, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[0],
                subtexture[1],
                subtexture[2],
            }));

            Animation.addAnimation(MovementAnimations.Up, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[36],
                subtexture[37],
                subtexture[38],
            }));

            Animation.addAnimation(MovementAnimations.Left, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[12],
                subtexture[13],
                subtexture[14],
            }));

            Animation.addAnimation(MovementAnimations.Right, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[24],
                subtexture[25],
                subtexture[26],
            }));
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
