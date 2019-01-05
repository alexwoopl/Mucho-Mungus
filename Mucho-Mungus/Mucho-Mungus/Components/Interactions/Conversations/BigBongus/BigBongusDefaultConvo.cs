using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mucho_Mungus.Components.EntityMovement;
using Mucho_Mungus.Entities;
using Nez;
using Nez.UI;

namespace Mucho_Mungus.Components.Interactions.Conversations.BigBongus
{
    public class BigBongusDefaultConvo : BaseConvoPoint
    {
        public override string text
        {
            get => "Stay a while and glisten!";
        }

        public override Dialog addConvoContent(Dialog dialog, Player interactor, Entity interactable, Table table)
        {
            var skin = Skin.createDefaultSkin();
            dialog.addText(text);
            addExitButton(dialog, interactor, interactable, skin);
            addGetPetButton(dialog, skin, interactor, interactable, table);

            return dialog;
        }

        private static void addGetPetButton(Dialog dialog, Skin skin, Player interactor, Entity interactable,Table table)
        {
            var getPetButton = new TextButton("Gimme a pet homie", skin);
            getPetButton.onClicked += getButt =>
            {
                //remove the current shown dialog
                table.removeElement(dialog);
                //Create the new one and use the next convo point.
                Dialog newDialog = Conversation.createDefaultDialogBox(interactable.name);
                BigBongusSecondConvo nextPoint = new BigBongusSecondConvo();
                nextPoint.addConvoContent(newDialog, interactor, interactable, table);
            };
            dialog.add(getPetButton);
        }
        
    }
}
