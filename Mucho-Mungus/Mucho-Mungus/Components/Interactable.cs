using Microsoft.Xna.Framework;
using Mucho_Mungus.Components.EntityMovement;
using Mucho_Mungus.Constants;
using Nez;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Components
{
    public class Interactable : UICanvas
    {
        
        public void interact(Entity interactor)
        {

            this.renderLayer = (int)RenderLayerIds.UILayer;
            entity.getComponent<EntityMover>().pauseMovement();

            
            //Create the base table to add interactable UI to.
            var table = stage.addElement(new Table());
            table.setFillParent(true);

            ////Start the dialogue with a dialogue box
            var conversation = DialogueBox(this.entity.name, "Stay a while and glisten", "Bye!");
            table.add(conversation);
        }


        public Dialog DialogueBox(string title, string messageText, string closeButtonText)
        {
            var skin = Skin.createDefaultSkin();

            var style = new WindowStyle
            {
                background = new PrimitiveDrawable(new Color(50, 50, 50)),
                //Dims the background
                stageBackground = new PrimitiveDrawable(new Color(0, 0, 0, 150))
            };

            var dialog = new Dialog(title, style);
            dialog.getTitleLabel().getStyle().background = new PrimitiveDrawable(new Color(55, 100, 100));
            dialog.pad(20, 5, 5, 5);
            dialog.addText(messageText);
            var nextButton = new TextButton(closeButtonText, skin);
            nextButton.onClicked += butt => dialog.hide();
            dialog.add(nextButton);


            return dialog;
        }
        
        
    }
}
