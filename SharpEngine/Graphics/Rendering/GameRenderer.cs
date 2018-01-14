using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace SharpEngine.Graphics.Rendering {

    public class GameRenderer {

        private SpriteBatch sprite_batch;

        private int Finder;
        private int BatchCount;
        private int CurrrentID;
        private int TempIndex;
        private Batch Current;
        private List<Batch> Batchs;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameRenderer( ) {
            this.sprite_batch = null;
            this.BatchCount = 0;
            this.CurrrentID = 0;
            this.Current = null;
            this.Batchs = null;
        }

        /// <summary>
        /// Awake the current graphic manager.
        /// </summary>
        /// <param name="device" >current game graphics device</param>
        public void Awake( GraphicsDevice device ) {
            this.sprite_batch = new SpriteBatch( device );
            this.Batchs = new List<Batch>( );
        }

        /// <summary>
        /// Push a sprite to the current game renderer.
        /// </summary>
        /// <param name="shader" >Shader used to render the sprite</param>
        /// <param name="sprite_sheet" >Sprite sheet used to render the sprite</param>
        /// <param name="position" >Reference to the position where draw the sprite on screen</param>
        /// <param name="frame" >Index of the frame used to draw the sprite</param>
        /// <param name="filter" >Filter used to draw the sprite</param>
        /// <param name="rotation" >Rotation of the sprite on the screen</param>
        /// <param name="scale" >Reference to the scale of the sprite on screen</param>
        /// <param name="depth" >Depath value of the sprite</param>
        public void Push( int shader, int sprite_sheet, ref Vector2 position, int sprite, Color filter, float rotation, ref Vector2 scale, float depth ) {
            this.Finder = 0;

            while ( this.Finder < this.BatchCount ) {
                var batch = this.Batchs[ this.Finder ];

                if ( batch.Shader != shader || batch.SpriteSheet != sprite_sheet ) {
                    this.Finder++;
                    continue;
                } else {
                    if ( !batch.IsFull ) {
                        batch.Add( ref position, sprite, ref filter, rotation, ref scale, depth );
                        return;
                    }
                }
            }

            this.Batchs.Add( new Batch( shader, sprite_sheet ) );
            this.Batchs[ this.BatchCount++ ].Add( ref position, sprite, ref filter, rotation, ref scale, depth );
        }

        /// <summary>
        /// Process the current game renderer.
        /// </summary>
        /// <param name="graphic_manager" >Reference to the current game graphic manager</param>
        public void Process( ref GraphicManager graphic_manager ) {
            this.sprite_batch.Begin( SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp );

            this.CurrrentID = 0;
            while ( this.CurrrentID < this.BatchCount ) {
                this.Current = this.Batchs[ this.CurrrentID ];

                graphic_manager.GetShader( this.Current.Shader );
                var sprite_sheet = graphic_manager.GetSpriteSheet( this.Current.SpriteSheet );

                this.TempIndex = 0;
                while ( this.TempIndex < this.Current.Pointer ) {
                    var temp = this.Current.Data[ this.TempIndex++ ];

                    this.sprite_batch.Draw(
                        sprite_sheet.Texture,
                        temp.Position,
                        sprite_sheet.Sprites[ temp.Frame ],
                        temp.Filter,
                        temp.Rotation, 
                        Vector2.Zero,
                        temp.Scale,
                        SpriteEffects.None,
                        temp.Depth
                    );
                }

                this.CurrrentID++;
            }

            this.sprite_batch.End( );

            this.Batchs.Clear( );
        }

    }

}
