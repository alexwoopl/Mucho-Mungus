using Microsoft.Xna.Framework.Graphics;
using Mucho_Mungus.Components;
using Mucho_Mungus.Entities.Actions;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Content.Characters
{
    public class Player : Entity, ICharacter
    {

        private Sprite<MovementAnimations> Animation { get; set; }

        public Player(String name)
        {
            
            //Create the player
            position = new Microsoft.Xna.Framework.Vector2(200, 200);

            //Allow us to move between areas
            this.addComponent<AreaTransitioner>();


        }

        public override void onAddedToScene()
        {
            //Set up animations and controls per scene
            List<Subtexture> subtexture = SetUpTextureAtlas(scene);
            Animation = this.addComponent(new Sprite<MovementAnimations>(subtexture[0]));
            SetUpAnimations(subtexture);
            this.addComponent(new PlayerController(Animation));


            var collisions = scene.findEntity("map").getComponent<TiledMapComponent>().tiledMap.getLayer<TiledTileLayer>("blockers");
            this.addComponent(new TiledMapMover(collisions));
            this.addComponent(new BoxCollider(12, 16));


            //Set as camera focus
            this.addComponent<CameraMover>();

        }

        public override void onRemovedFromScene()
        {
            this.removeComponent<PlayerController>();
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
    }

}
