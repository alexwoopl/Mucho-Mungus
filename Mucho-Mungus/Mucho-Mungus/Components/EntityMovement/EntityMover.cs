using Microsoft.Xna.Framework;
using Mucho_Mungus.Entities.Actions;
using Nez;
using Nez.Sprites;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Components.EntityMovement
{
    public abstract class EntityMover : Component, IUpdatable
    {
        protected Sprite<MovementAnimations> animation;

        private bool paused = false;


        protected TiledMapMover mover;
        protected BoxCollider collider;
        protected Vector2 velocity;
        protected TiledMapMover.CollisionState cs = new TiledMapMover.CollisionState();

        public EntityMover(Sprite<MovementAnimations> animation)
        {
            this.animation = animation;
        }

        public override void onAddedToEntity()
        {
            mover = this.getComponent<TiledMapMover>();
            collider = entity.getComponent<BoxCollider>();
        }

        public Point getGridPosition()
        {
            var currentPosition = entity.position.ToPoint();
            currentPosition.X = currentPosition.X / 16;
            currentPosition.Y = (currentPosition.Y / 16) - 1;
            return currentPosition;
        }


        public void update()
        {
            if (!paused) {
                DecideToPauseOrPlayAnimations();
                MoveAndAnimate();
            }
        }

        internal abstract void MoveAndAnimate();
        internal abstract void DecideToPauseOrPlayAnimations();

        public void pauseMovement()
        {
            paused = true;
        }

        public void resumeMovement()
        {
            paused = false;
        }
    }
}
