using Mucho_Mungus.Entities;
using Mucho_Mungus.Entities.Constants;
using Mucho_Mungus.Scenes;
using Mucho_Mungus.Scenes.Constants;
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
        private const float TimeToWaitAfterNewSceneTransitionIsAllowed = 3f;
        private bool entityCanTransition = true;

        public override void onEntityTransformChanged(Transform.Component comp)
        {
            base.onEntityTransformChanged(comp);

            if (entityCanTransition)
            {

                var transitionBlocks = GetTransitionBlocks();

                foreach (TiledTile tile in transitionBlocks.tiles)
                {
                    if (tile == null)
                    {
                        continue;
                    }

                    var tilePosition = getTilePosition(tile);

                    if (entityIsInsideofTile(tilePosition))
                    {
                        foreach (SceneChangeMapper nextSceneLink in ((ExtendedScene)entity.scene).getTransitions())
                        {
                            if (isSceneToLoad(nextSceneLink))
                            {
                                Core.startSceneTransition(new FadeTransition(() => nextSceneLink.scene));

                                entity.detachFromScene();
                                entity.attachToScene(nextSceneLink.scene);


                                entityCanTransition = false;
                                Core.schedule(TimeToWaitAfterNewSceneTransitionIsAllowed, t => entityCanTransition = true);

                                break;
                            }
                        }

                    }

                }



            }
        }

        private bool isSceneToLoad(SceneChangeMapper nextScene)
        {
            return nextScene.min.X <= entity.position.X && nextScene.min.Y <= entity.position.Y &&
                                            nextScene.max.X >= entity.position.X && nextScene.max.Y >= entity.position.Y;
        }

        private bool entityIsInsideofTile(Microsoft.Xna.Framework.Vector2 tilePosition)
        {
            return entity.position.X > tilePosition.X && entity.position.X < tilePosition.X + 16 && entity.position.Y > tilePosition.Y && entity.position.Y < tilePosition.Y + 16;
        }

        private TiledTileLayer GetTransitionBlocks()
        {
            return entity.scene.findEntity(EntityNames.Map).getComponent<TiledMapComponent>().tiledMap.getLayer<TiledTileLayer>(MapLayerNames.Transitionals);
        }

        private Microsoft.Xna.Framework.Vector2 getTilePosition(TiledTile tile)
        {
            return tile.getWorldPosition(entity.scene.findEntity(EntityNames.Map).getComponent<TiledMapComponent>().tiledMap);
        }
    }
}
