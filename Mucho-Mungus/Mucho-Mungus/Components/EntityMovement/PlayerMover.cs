using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mucho_Mungus.Components;
using Mucho_Mungus.Entities;
using Mucho_Mungus.Entities.Actions;
using Nez;
using Nez.Sprites;
using Nez.Tiled;
using System;

namespace Mucho_Mungus.Components.EntityMovement
{
    internal class PlayerMover : EntityMover
    {
        public float speed = 100f;

        public PlayerMover(Sprite<MovementAnimations> animation) : base(animation)
        {
        }

        internal override void MoveAndAnimate()
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

            if (((Player)entity).pet != null) {

                if (Math.Abs(entity.position.X - ((Player)entity).pet.position.X) > 16 ||
                    Math.Abs(entity.position.Y - ((Player)entity).pet.position.Y) > 16) {

                    Point targetPosition = entity.position.ToPoint();
                    targetPosition.X = (targetPosition.X - 8) / 16;
                    targetPosition.Y = (targetPosition.Y - 8) / 16;

                    ((Player)entity).pet.changeTargetPosition(targetPosition);
                }
                if (Math.Abs(entity.position.X - ((Player)entity).pet.position.X) > 64 ||
                    Math.Abs(entity.position.Y - ((Player)entity).pet.position.Y) > 64) {
                    ((Player)entity).pet.position = entity.position;
                }
            }
        }

        internal override void DecideToPauseOrPlayAnimations()
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