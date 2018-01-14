using Microsoft.Xna.Framework;

using SharpEngine.Audio;
using SharpEngine.Graphics;
using SharpEngine.Graphics.Rendering;
using SharpEngine.Kernel;
using SharpEngine.Kernel.Processors;

namespace SharpEngine {

    public abstract class SharpGame : Game {

        private GraphicsDeviceManager Graphics;

        protected GraphicManager GraphicManager;
        protected AudioManager AudioManager;

        protected GameRenderer Renderer;
        protected GameWorld GameWorld;
        protected GameProcessorManager GameProcessorManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public SharpGame( ) : base( ) {
            this.Graphics = new GraphicsDeviceManager( this );
            this.Content.RootDirectory = "Content";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="full_screen"></param>
        public void SetSize( int width, int height, bool full_screen = false ) {
            this.Graphics.PreferredBackBufferWidth = 1280;
            this.Graphics.PreferredBackBufferHeight = 720;
            this.Graphics.IsFullScreen = full_screen;

            this.Graphics.ApplyChanges( );
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize( ) {
            this.GraphicManager = new GraphicManager( this.Content );
            this.AudioManager = new AudioManager( this.Content );

            this.Renderer = new GameRenderer( );
            this.GameProcessorManager = new GameProcessorManager( );

            base.Initialize( );
        }

        /// <summary>
        /// Initialize game.
        /// </summary>
        public abstract void GameInit( );

        /// <summary>
        /// Load the game content.
        /// </summary>
        public abstract void LoadGameContent( );

        /// <summary>
        /// Awake the game.
        /// </summary>
        public abstract void GameAwake( );

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent( ) {
            this.Renderer.Awake( this.GraphicsDevice );
            this.GameWorld.Awake( );
            this.GameProcessorManager.Awake( this.GameWorld.Size );

            this.GameInit( );
            this.LoadGameContent( );
            this.GameAwake( );

            this.GameWorld.Awake( ref this.GameProcessorManager );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent( ) {
            this.Content.Unload( );
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="game_time">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime game_time ) {
            this.GameWorld.Process( ref this.GameProcessorManager, ref this.Renderer, ref this.AudioManager );
            this.GameProcessorManager.Update( ref game_time, ref this.GameWorld );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="game_time">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime game_time ) {
            // Render Processors
            this.GameProcessorManager.Draw( ref this.Renderer, ref this.GameWorld );

            // Game rendering
            GraphicsDevice.Clear( Color.Gray );
            this.Renderer.Process( ref this.GraphicManager );
        }

    }

}
