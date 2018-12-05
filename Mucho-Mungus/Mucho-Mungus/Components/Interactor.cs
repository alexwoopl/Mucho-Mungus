using Microsoft.Xna.Framework.Input;
using Mucho_Mungus.Components.EntityMovement;
using Mucho_Mungus.Entities;
using Mucho_Mungus.Entities.Constants;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Components
{
    public class Interactor : Component, IUpdatable
    {



        public void update()
        {
            if (Input.isKeyPressed(Keys.E))
            {
                var currentPostion = this.entity.position;
                
                var entitiesInThisScene = this.entity.scene.entities;

                var closestEntity = getClosestEntity(entitiesInThisScene);

                if (closestEntity == null)
                {
                    return;
                }

                entity.getComponent<EntityMover>().pauseMovement();

                var interactable = closestEntity.getComponent<Interactable>();

                interactable.interact(entity);

            }
            
        }

        private Entity getClosestEntity(EntityList entitiesInThisScene)
        {
            foreach (Entity entity in entitiesInThisScene.entitiesWithTag((int)EntityTags.Interactable)){
                //This couples to the player class we might wanna solve it later
                if (entity.position.X > (this.entity.position.X - Player.width) && entity.position.X < (this.entity.position.X + Player.width) &&
                    entity.position.Y > (this.entity.position.Y - Player.height) && entity.position.Y < (this.entity.position.Y + Player.height))
                {
                    return entity;
                }
                
            }

            return null;
        }
    }
}
