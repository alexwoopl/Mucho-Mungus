using Microsoft.Xna.Framework;
using Mucho_Mungus.Constants;
using Mucho_Mungus.Entities;
using Mucho_Mungus.Entities.Constants;
using Mucho_Mungus.Scenes.Constants;
using Nez;
using Nez.Tiled;
using System.Collections.Generic;

namespace Mucho_Mungus.Scenes
{
    public abstract class ExtendedScene : Scene
    {

        public override void initialize()
        {
            clearColor = Color.DarkOliveGreen;
            //addRenderer(new DefaultRenderer());
            addRenderer(new RenderLayerRenderer(0,new int[] { (int)RenderLayerIds.First, (int)RenderLayerIds.Second, (int)RenderLayerIds.Third}));
            addRenderer(new ScreenSpaceRenderer(1, new int[] { (int)RenderLayerIds.UILayer }));

            var map = this.content.Load<TiledMap>(getMapName());
            var mapEntity = this.createEntity(EntityNames.Map);
            var mapComponent = mapEntity.addComponent(new TiledMapComponent(map, MapLayerNames.Blockers));
            mapComponent.renderLayer = (int)RenderLayerIds.Second;

            this.camera.zoomIn(2);
        }

        internal abstract string getMapName();

        public List<SceneChangeMapper> transitions = new List<SceneChangeMapper>();

        public abstract List<SceneChangeMapper> getTransitions();

    }
}
