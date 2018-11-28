using Microsoft.Xna.Framework;
using Mucho_Mungus.Entities.Actions;
using Nez;
using Nez.AI.Pathfinding;
using Nez.Sprites;
using System.Collections.Generic;

namespace Mucho_Mungus.Components
{
    public class NPCMover : EntityMover
    {

        public float speed = 50f;
        
        //arbitrary lets fix this up later.
        Point targetPosition = new Point(16, 15);

        public NPCMover(Sprite<MovementAnimations> animation) : base(animation)
        {
        }

        
        internal override void MoveAndAnimate()
        {

            Point currentPosition = getGridPosition();
            List<Point> pathToTargetPosition = FindAStarPath(currentPosition, targetPosition);

            if (pathToTargetPosition != null && currentPosition != targetPosition)
            {
                if (currentPosition.X < pathToTargetPosition[1].X)
                {
                    if (!animation.isAnimationPlaying(MovementAnimations.Right))
                    {
                        animation.play(MovementAnimations.Right);
                    }
                    velocity.X = speed;
                }
                else if (currentPosition.X > pathToTargetPosition[1].X)
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

                if (currentPosition.Y < pathToTargetPosition[1].Y)
                {
                    if (!animation.isAnimationPlaying(MovementAnimations.Down))
                    {
                        animation.play(MovementAnimations.Down);
                    }
                    velocity.Y = speed;
                }
                else if (currentPosition.Y > pathToTargetPosition[1].Y)
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

        private List<Point> FindAStarPath(Point currentPosition, Point targetPosition)
        {
            var graph = new AstarGridGraph(mover.collisionLayer);
            var path = graph.search(currentPosition, targetPosition);
            return path;
        }

        //Just for some cheap movement, will change later.
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
