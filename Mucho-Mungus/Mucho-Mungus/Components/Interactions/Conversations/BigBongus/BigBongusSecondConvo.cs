using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Mucho_Mungus.Components.EntityMovement;
using Mucho_Mungus.Entities;
using Mucho_Mungus.Entities.Munguses;
using Nez;
using Nez.Textures;
using Nez.UI;

namespace Mucho_Mungus.Components.Interactions.Conversations.BigBongus
{
    public class BigBongusSecondConvo : BaseConvoPoint
    {
        public override string text {
            get => "Pet pet, there you go";
        }

        public override Dialog addConvoContent(Dialog dialog, Player interactor, Entity interactable, Table table)
        {
            var skin = Skin.createDefaultSkin();
            dialog.addText(text);

            addGetSpiderButton(dialog, interactor, interactable, skin);

            addExitButton(dialog, interactor, interactable, skin);
            table.add(dialog);

            return dialog;
        }

        private static void addGetSpiderButton(Dialog dialog, Player interactor, Entity interactable, Skin skin)
        {
            var textureAtlas = interactor.scene.content.Load<Texture2D>("Characters/Spider");
            var subtexture = Subtexture.subtexturesFromAtlas(textureAtlas, 16, 16);
            Button pet1 = new Button(skin);
            Image i = new Image(subtexture[0]);
            pet1.add(i);
            var c = pet1.getCell(i);
            c.setMinHeight(c.getMinHeight() * 5);
            c.setMinWidth(c.getMinWidth() * 5);
            pet1.row();
            pet1.add("Crangly Crawler");

            pet1.onClicked += getButt =>
            {
                BaseMungus pet = new Spider("spiderman", interactor.position);
                interactor.pet = pet;

                interactor.scene.addEntity(pet);

                interactable.getComponent<EntityMover>().resumeMovement();
                interactor.enableActions();
                dialog.remove();
            };


            dialog.add(pet1);
        }
    }
}
