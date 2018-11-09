using Mucho_Mungus.Scenes;
using Nez;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Components
{
    public class AreaTransitioner : Component
    {
        private bool entityCanTransition = true;

        public override void onEntityTransformChanged(Transform.Component comp)
        {
            base.onEntityTransformChanged(comp);

            if (entityCanTransition)
            {

                var transitionBlocks = entity.scene.findEntity("map").getComponent<TiledMapComponent>().tiledMap.getLayer<TiledTileLayer>("transition");

                foreach (TiledTile tile in transitionBlocks.tiles)
                {
                    if (tile == null)
                    {
                        continue;
                    }

                    var pos = tile.getWorldPosition(entity.scene.findEntity("map").getComponent<TiledMapComponent>().tiledMap);

                    if (entity.position.X > pos.X && entity.position.X < pos.X + 16 && entity.position.Y > pos.Y && entity.position.Y < pos.Y + 16)
                    {
                        foreach (SceneChangeMapper nextScene in ((ExtendedScene)entity.scene).getTransitions())
                        {
                            if (nextScene.min.X <= entity.position.X && nextScene.min.Y <= entity.position.Y &&
                                nextScene.max.X >= entity.position.X && nextScene.max.Y >= entity.position.Y)
                            {
                                Core.startSceneTransition(new FadeTransition(() => nextScene.sceneName));

                                entity.detachFromScene();
                                entity.attachToScene(nextScene.sceneName);


                                entityCanTransition = false;
                                Core.schedule(3f, t => entityCanTransition = true);

                                break;
                            }
                        }

                    }

                }

                

            }
        }
        
    }
}
