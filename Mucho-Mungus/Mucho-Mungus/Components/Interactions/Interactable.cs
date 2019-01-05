using Microsoft.Xna.Framework;
using Mucho_Mungus.Components.EntityMovement;
using Mucho_Mungus.Constants;
using Mucho_Mungus.Entities;
using Nez;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Components.Interactions
{
    public class Interactable : UICanvas
    {

        private Table table;
        private Player interactor;
        
        public void interact(Entity interactor)
        {
            //Stop the interactor from making actions while interacting
            this.interactor = (Player)interactor;
            this.interactor.disableActions();

            //Stop the current entity also
            entity.getComponent<EntityMover>().pauseMovement();

            this.renderLayer = (int)RenderLayerIds.UILayer;

            //Create the base table to add interactable UI to.
            initUITable();

            IConversation convo = new Conversation(this.interactor, this.entity, this.table);
            var conversation = convo.getConvoDialog();

            ////Start the dialogue with a dialogue box
            table.add(conversation);
        }

        private void initUITable()
        {
            table = stage.addElement(new Table());
            table.setFillParent(true);
        }
        

    }
}
