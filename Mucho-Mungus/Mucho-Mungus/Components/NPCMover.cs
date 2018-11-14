using Microsoft.Xna.Framework;
using Mucho_Mungus.Entities.Actions;
using Nez;
using Nez.AI.Pathfinding;
using Nez.Sprites;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Components
{
    public class NPCMover : Component, IUpdatable
    {
        private Sprite<MovementAnimations> animation;
        public float speed = 50f;

        TiledMapMover mover;
        BoxCollider collider;
        Vector2 velocity;
        TiledMapMover.CollisionState cs = new TiledMapMover.CollisionState();


        Point targetPosition = new Point(16, 15);

        public NPCMover(Sprite<MovementAnimations> animation)
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
            var graph = new AstarGridGraph(mover.collisionLayer);
            Point currentPosition = getGridPosition();
            var path = graph.search(currentPosition, targetPosition);

            if (path != null && currentPosition != targetPosition)
            {
                if (currentPosition.X < path[1].X)
                {
                    if (!animation.isAnimationPlaying(MovementAnimations.Right))
                    {
                        animation.play(MovementAnimations.Right);
                    }
                    velocity.X = speed;
                }
                else if (currentPosition.X > path[1].X)
                {
                    if (!animation.isAnimationPlaying(MovementAnimations.Left))
                    {
                        animation.play(MovementAnimations.Left);
                    }
                    velocity.X = -speed;
                }
                else
                {
                    velocity.X = 0;
                }

                if (currentPosition.Y < path[1].Y)
                {
                    if (!animation.isAnimationPlaying(MovementAnimations.Down))
                    {
                        animation.play(MovementAnimations.Down);
                    }
                    velocity.Y = speed;
                }
                else if (currentPosition.Y > path[1].Y)
                {
                    if (!animation.isAnimationPlaying(MovementAnimations.Up))
                    {
                        animation.play(MovementAnimations.Up);
                    }
                    velocity.Y = -speed;
                }
                else
                {
                    velocity.Y = 0;
                }
            }
            else
            {
                switchTargetPosition();
                velocity.X = 0;
                velocity.Y = 0;
            }

            mover.move(velocity * Time.deltaTime, collider, cs);
        }

        private void switchTargetPosition()
        {
            if(targetPosition.X == 16)
            {
                targetPosition.X = 9;
                targetPosition.Y = 9;
            }
            else
            {
                targetPosition.X = 16;
                targetPosition.Y = 17;
            }
        }

        private Point getGridPosition()
        {
            var currentPosition = entity.position.ToPoint();
            currentPosition.X = currentPosition.X / 16;
            currentPosition.Y = (currentPosition.Y / 16) - 1;
            return currentPosition;
        }

        private void DecideToPauseOrPlayAnimations()
        {
            if (velocity.X == 0 && velocity.Y == 0 )
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
