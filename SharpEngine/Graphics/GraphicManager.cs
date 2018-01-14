using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace SharpEngine.Graphics {

    public class GraphicManager {

        public static GraphicManager Instance;

        private ContentManager ContentManager;
        private List<GameSpriteSheet> SpriteSheets;
        private List<GameShader> Shaders;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content_managers" >The current game content manager</param>
        public GraphicManager( ContentManager content_managers ) {
            Instance = this;

            this.ContentManager = content_managers;
            this.SpriteSheets = new List<GameSpriteSheet>( );
            this.Shaders = new List<GameShader>( );
        }

        /// <summary>
        /// Load a sprite sheet to the current game graphic manager.
        /// </summary>
        /// <param name="path" >Path to the base texture of the sprite sheet</param>
        /// <param name="is_lit" >If the sprite sheet is lit</param>
        public GameSpriteSheet LoadSpriteSheet( string path, bool is_lit = false ) {
            Texture2D texture = this.ContentManager.Load<Texture2D>( path );
            Texture2D normal = ( is_lit ) ? this.ContentManager.Load<Texture2D>( path + "_n" ) : null;

            GameSpriteSheet sheet = new GameSpriteSheet( texture, normal );

            int finder = 0;
            while ( finder < this.SpriteSheets.Count ) {
                if ( this.SpriteSheets[ finder ].Texture == texture && this.SpriteSheets[ finder ].Normal == normal )
                    return this.SpriteSheets[ finder ];

                finder++;
            }

            this.SpriteSheets.Add( sheet );

            return this.SpriteSheets[ this.SpriteSheets.Count - 1 ];
        }

        /// <summary>
        /// Load a shader to the current game graphic manager.
        /// </summary>
        /// <typeparam name="Type" >Type of the new shader</typeparam>
        /// <param name="path" >Path of the shader</param>
        /// <returns>The new shader</returns>
        public Type LoadShader<Type>( string path ) where Type : GameShader, new() {
            if ( path != "" && path != " " ) {
                Type shader = new Type( );

                if ( !this.Shaders.Contains( shader ) ) {
                    shader.Effect = this.ContentManager.Load<Effect>( path );
                    this.Shaders.Add( shader );
                }
            }

            return default( Type );
        }

        /// <summary>
        /// Apply the query shader.
        /// </summary>
        /// <param name="index" >Index of the query shader</param>
        public void GetShader( int index ) {
            if ( index > -1 && index < this.Shaders.Count )
                this.Shaders[ index ].Process( );
        } 

        /// <summary>
        /// Get a sprite sheet from the current game graphic manager.
        /// </summary>
        /// <param name="index" >Index of the query sprite sheet</param>
        /// <returns>The query sprite sheet</returns>
        public GameSpriteSheet GetSpriteSheet( int index ) {
            if ( index > -1 && index < this.SpriteSheets.Count )
                return this.SpriteSheets[ index ];

            return null;
        }

    }

}
