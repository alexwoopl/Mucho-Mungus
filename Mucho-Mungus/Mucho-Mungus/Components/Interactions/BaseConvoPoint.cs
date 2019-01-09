using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mucho_Mungus.Components.EntityMovement;
using Mucho_Mungus.Entities;
using Nez;
using Nez.UI;

namespace Mucho_Mungus.Components.Interactions
{
    public abstract class BaseConvoPoint : IConvoPoint
    {
        public abstract string text { get; }

        public abstract Dialog addConvoContent(Dialog dialog, Player interactor, Entity interactable, Table table);


        protected static void addExitButton(Dialog dialog, Player interactor, Entity interactable, Skin skin)
        {
            var exitButton = new TextButton("Bye!", skin);
            exitButton.onClicked += butt =>
            {
                dialog.remove();
                interactable.getComponent<EntityMover>().resumeMovement();
                interactor.enableActions();
            };


            dialog.add(exitButton);
        }
    }
}
