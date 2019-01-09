using Microsoft.Xna.Framework;
using Mucho_Mungus.Entities;
using Mucho_Mungus.Entities.Actions;
using Nez;
using Nez.AI.Pathfinding;
using Nez.Sprites;
using System.Collections.Generic;

namespace Mucho_Mungus.Components.EntityMovement
{
    public class NPCMover : EntityMover
    {

        public float speed = 125f;

        private Character npc;
        

        public NPCMover(Sprite<MovementAnimations> animation) : base(animation)
        {
        }

        
        internal override void MoveAndAnimate()
        {

            npc = (Character)entity;
            Point currentPosition = getGridPosition();
            List<Point> pathToTargetPosition = FindAStarPath(currentPosition, npc.targetPosition);

            if (pathToTargetPosition != null && currentPosition != npc.targetPosition)
            {
                if (currentPosition.X * 16 < pathToTargetPosition[1].X * 16)
                {
                    if (!animation.isAnimationPlaying(MovementAnimations.Right))
                    {
                        animation.play(MovementAnimations.Right);
                    }
                    velocity.X = speed;
                }
                else if (currentPosition.X * 16 > pathToTargetPosition[1].X * 16)
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

                if (currentPosition.Y * 16 < pathToTargetPosition[1].Y * 16)
                {
                    if (!animation.isAnimationPlaying(MovementAnimations.Down))
                    {
                        animation.play(MovementAnimations.Down);
                    }
                    velocity.Y = speed;
                }
                else if (currentPosition.Y * 16 > pathToTargetPosition[1].Y * 16)
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
                velocity.X = 0;
                velocity.Y = 0;
            }

            mover.move(velocity * Time.deltaTime, collider, cs);
        }

        private List<Point> FindAStarPath(Point currentPosition, Point targetPosition)
        {
            var graph = new AstarGridGraph(mover.collisionLayer);
            var path = graph.search(currentPosition, targetPosition);
            return path;
        }
        
        internal override void DecideToPauseOrPlayAnimations()
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
