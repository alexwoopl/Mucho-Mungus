using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mucho_Mungus.Entities.Actions;
using Nez;
using Nez.Sprites;
using Nez.Tiled;

namespace Mucho_Mungus
{
    internal class PlayerController : Component, IUpdatable
    {

        Sprite<MovementAnimations> animation;
        public float speed = 100f;

        TiledMapMover mover;
        BoxCollider collider;
        Vector2 velocity;
        TiledMapMover.CollisionState cs = new TiledMapMover.CollisionState();

        public PlayerController(Sprite<MovementAnimations> animation)
        {
            this.animation = animation;
        }
        
        public override void onAddedToEntity()
        {
            mover = this.getComponent<TiledMapMover>();
            collider = entity.getComponent<BoxCollider>();
        }

        public void update()
        {
            DecideToPauseOrPlayAnimations();
            MoveAndAnimate();
            
        }

        private void MoveAndAnimate()
        {
            if (Input.isKeyDown(Keys.A))
            {
                if (!animation.isAnimationPlaying(MovementAnimations.Left))
                {
                    animation.play(MovementAnimations.Left);
                }
                velocity.X = -speed;
            }
            else if (Input.isKeyDown(Keys.D))
            {
                if (!animation.isAnimationPlaying(MovementAnimations.Right))
                {
                    animation.play(MovementAnimations.Right);
                }
                velocity.X = speed;
            }
            else
            {
                velocity.X = 0;
            }

            if (Input.isKeyDown(Keys.W))
            {
                if (!animation.isAnimationPlaying(MovementAnimations.Up))
                {
                    animation.play(MovementAnimations.Up);
                }
                velocity.Y = -speed;
            }
            else if (Input.isKeyDown(Keys.S))
            {
                if (!animation.isAnimationPlaying(MovementAnimations.Down))
                {
                    animation.play(MovementAnimations.Down);
                }
                velocity.Y = speed;
            }
            else
            {
                velocity.Y = 0;
            }

            mover.move(velocity * Time.deltaTime, collider, cs);
        }

        private void DecideToPauseOrPlayAnimations()
        {
            if (!Input.isKeyDown(Keys.A) && !Input.isKeyDown(Keys.S) && !Input.isKeyDown(Keys.D) && !Input.isKeyDown(Keys.W))
            {
                animation.pause();
            }
            else
            {
                animation.unPause();
            }
        }
    }
}