using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mucho_Mungus.Components.EntityMovement;
using Mucho_Mungus.Constants;
using Mucho_Mungus.Entities.Actions;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Entities
{
    public abstract class Character : Entity, ICharacter
    {
        public int width = 16, height = 16;
        protected Sprite<MovementAnimations> Animation { get; set; }

        //arbitrary lets fix this up later.
        public Point targetPosition;

        public Character(string name) : base(name)
        {

        }

        protected List<Subtexture> SetUpTextureAtlas(string charaterSpritePath)
        {
            // load up the TextureAtlas that we generated with the Pipeline tool specifying individual files
            var textureAtlas = scene.content.Load<Texture2D>(charaterSpritePath);


            // fetch a Subtexture from the atlas. A Subtexture consists of the Texture2D and the rect on the Texture2D this particular image ended up
            var subtexture = Subtexture.subtexturesFromAtlas(textureAtlas, width, height);
            return subtexture;
        }

        protected void SetUpMovementAnimations(List<Subtexture> subtexture)
        {
            Animation.renderLayer = (int)RenderLayerIds.First;
            Animation.addAnimation(MovementAnimations.Down, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[0],
                subtexture[1],
                subtexture[2],
            }));

            Animation.addAnimation(MovementAnimations.Left, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[3],
                subtexture[4],
                subtexture[5],
            }));

            Animation.addAnimation(MovementAnimations.Right, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[6],
                subtexture[7],
                subtexture[8],
            }));

            Animation.addAnimation(MovementAnimations.Up, new SpriteAnimation(new List<Subtexture>()
            {
                subtexture[9],
                subtexture[10],
                subtexture[11],
            }));
        }

        public void changeTargetPosition(Point point)
        {
            targetPosition = point;
        }
    }
}
