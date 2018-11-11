using Mucho_Mungus.Entities;
using Mucho_Mungus.Scenes;
using Nez;

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
            
            var myScene = new Home();
            myScene.addEntity(new Player("player"));
            
            // set the scene so Nez can take over
            scene = myScene;
        }
        
    }
}
