using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Mucho_Mungus.Components.Interactions.Conversations.BigBongus;
using Mucho_Mungus.Entities;
using Nez;
using Nez.UI;

namespace Mucho_Mungus.Components.Interactions
{
    public class Conversation : IConversation
    {
        //I will make the whole dialog and use the Convo points!

        private IConvoPoint currentConvoPoint;
        private Player interactor;
        private Entity interactable;
        private Table table;

        public Conversation(Player interactor, Entity interactable, Table table)
        {
            this.interactable = interactable;
            this.interactor = interactor;
            this.table = table;
            currentConvoPoint = new BigBongusDefaultConvo();
        }

        public Dialog getConvoDialog()
        {
            var skin = Skin.createDefaultSkin();

            Dialog dialog = createDefaultDialogBox(interactable.name);
            dialog = currentConvoPoint.addConvoContent(dialog, interactor, interactable, table);

            return dialog;
        }

        public static Dialog createDefaultDialogBox(string title)
        {
            var style = new WindowStyle
            {
                background = new PrimitiveDrawable(new Color(50, 50, 50)),
                //Dims the background
                stageBackground = new PrimitiveDrawable(new Color(0, 0, 0, 150))
            };

            var dialog = new Dialog(title, style);
            dialog.getTitleLabel().getStyle().background = new PrimitiveDrawable(new Color(55, 100, 100));
            dialog.pad(20, 5, 5, 5);
            return dialog;
        }
    }
}
