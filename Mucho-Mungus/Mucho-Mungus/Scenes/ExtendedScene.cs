using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Scenes
{
    public abstract class ExtendedScene : Scene
    {

        public List<SceneChangeMapper> transitions = new List<SceneChangeMapper>();

        public abstract List<SceneChangeMapper> getTransitions();

    }
}
