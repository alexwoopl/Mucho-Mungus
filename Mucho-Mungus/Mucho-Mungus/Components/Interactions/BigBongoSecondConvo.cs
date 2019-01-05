using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mucho_Mungus.Entities;
using Nez;
using Nez.UI;

namespace Mucho_Mungus.Components.Interactions
{
    public class BigBongoSecondConvo : BaseConvoPoint
    {
        public override string text {
            get => "Pet pet, there you go";
        }

        public override Dialog addConvoContent(Dialog dialog, Player interactor, Entity interactable, Table table)
        {
            var skin = Skin.createDefaultSkin();
            dialog.addText(text);
            
            addExitButton(dialog, interactor, interactable, skin);
            table.add(dialog);

            return dialog;
        }
    }
}
