using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace SharpEngine.Graphics {

    public class GameSpriteSheet {

        public Texture2D Texture;
        public Texture2D Normal;
        public List<Rectangle> Sprites;
        public List<Vector2> Origins;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="texture" >Sprite sheet texture</param>
        /// <param name="normal" >Sprite sheet normal texture</param>
        public GameSpriteSheet( Texture2D texture, Texture2D normal ) {
            this.Texture = texture;
            this.Normal = normal;
            this.Sprites = new List<Rectangle>( );
            this.Sprites.Add( new Rectangle( 0, 0, texture.Width, texture.Height ) );
            this.Origins = new List<Vector2>( );
            this.Origins.Add( new Vector2( texture.Width + texture.Width * 0.5f, texture.Height + texture.Height * 0.5f ) );
        }

        /// <summary>
        /// Create all sprite defined by the params.
        /// </summary>
        /// <param name="column" >Amount of row of the sprite sheet</param>
        /// <param name="line" >Amount of line of the sprite sheet</param>
        /// <param name="width" >Width of one sprite of the sprite sheet</param>
        /// <param name="height" >Height of one sprite of the sprite sheet</param>
        /// <returns>The current game sprite sheet</returns>
        public GameSpriteSheet Make( int column, int line, int width, int height ) {
            Rectangle temp;
            int x;

            for ( int y = 0; y < line; y++ ) {
                for ( x = 0; x < column; x++ ) {
                    temp = new Rectangle( x * width, y * height, width, height );

                    if ( !this.Sprites.Contains( temp ) )
                        this.Sprites.Add( temp );
                }
            }
            return this;
        }

        /// <summary>
        /// Manually create a sprite for the sprite sheet.
        /// </summary>
        /// <param name="column" >Index of the column of the sprite</param>
        /// <param name="line" >Index of the line of the sprite</param>
        /// <param name="width" >Width of one sprite</param>
        /// <param name="height" >Height of one sprite</param>
        /// <param name="offset_x" >Offset x of the sprite from original column</param>
        /// <param name="offset_y" >Offset y of the sprite from original line</param>
        /// <returns>The current game sprite sheet</returns>
        public GameSpriteSheet Add( int column, int line, int width, int height, int offset_x = 0, int offset_y = 0 ) {
            Rectangle temp = new Rectangle( offset_x + column * width, offset_y + line * height, width, height );

            if ( !this.Sprites.Contains( temp ) ) {
                this.Sprites.Add( temp );
                this.Origins.Add( new Vector2( temp.X + width * 0.5f, temp.Y + height * 0.5f ) );
            }

            return this;
        }

    }

}
