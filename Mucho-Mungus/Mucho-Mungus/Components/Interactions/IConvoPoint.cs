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
    public interface IConvoPoint
    {
        string text { get; }

        Dialog addConvoContent(Dialog dialog, Player interactor, Entity interactable, Table table);
    }
}
