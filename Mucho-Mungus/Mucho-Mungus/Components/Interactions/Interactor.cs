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

namespace Mucho_Mungus.Components.Interactions
{
    public class Interactor : Component, IUpdatable
    {

        //This is controlled by the interactable as only it knows when its finished.
        private Boolean allowNewInteraction = true;

        
        public void update()
        {
            if (Input.isKeyPressed(Keys.E) && allowNewInteraction)
            {
                var currentPostion = this.entity.position;
                
                var entitiesInThisScene = this.entity.scene.entities;

                var closestEntity = getClosestEntity(entitiesInThisScene);

                if (closestEntity == null)
                {
                    return;
                }
                
                var interactable = closestEntity.getComponent<Interactable>();

                interactable.interact(entity);

            }
            
        }

        public void blockNewInteractions()
        {
            allowNewInteraction = false;
        }
        public void allowNewInteractions()
        {
            allowNewInteraction = true;
        }

        private Entity getClosestEntity(EntityList entitiesInThisScene)
        {
            //Convert this entity to a player because it always will be.
            var player = (Player)this.entity;

            foreach (Entity entity in entitiesInThisScene.entitiesWithTag((int)EntityTags.Interactable)){
                //This couples to the player class we might wanna solve it later
                if (entity.position.X > (player.position.X - player.width) && entity.position.X < (player.position.X + player.width) &&
                    entity.position.Y > (player.position.Y - player.height) && entity.position.Y < (player.position.Y + player.height))
                {
                    return entity;
                }
                
            }

            return null;
        }
    }
}
