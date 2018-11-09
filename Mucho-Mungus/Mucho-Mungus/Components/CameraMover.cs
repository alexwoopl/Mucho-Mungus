using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Components
{
    public class CameraMover : Component, IUpdatable
    {

        public void update()
        {
            var cameraFocus = new Vector2();
            cameraFocus.X = entity.position.X;
            cameraFocus.Y = entity.position.Y;

            entity.scene.camera.position = cameraFocus;
        }
    }
}
