﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mucho_Mungus.Content.Characters;
using Nez;
using Nez.Tiled;

namespace Mucho_Mungus
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
    
        //GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;

        public Game1() : base(width: 1280, height: 768, isFullScreen: false, enableEntitySystems: false)
        {

        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            
            
            var myScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);
            //myScene.camera.zoomIn(2);
            var map = myScene.content.Load<TiledMap>("basiclevel");
            var mapEntity = myScene.createEntity("map");
            var mapComponent = mapEntity.addComponent(new TiledMapComponent(map, "blockers"));
            


            var player = new Player();
            player.Spawn(myScene);

            

            // set the scene so Nez can take over
            scene = myScene;
        }
        
    }
}
