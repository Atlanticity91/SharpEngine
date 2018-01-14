using Microsoft.Xna.Framework;

namespace SharpEngine.Graphics.Rendering {
    
    internal struct BatchData {

        public Vector2 Position;
        public int Frame;
        public Color Filter;
        public float Rotation;
        public Vector2 Scale;
        public float Depth;

    };

    internal class Batch {

        public int Shader {
            get;
            private set;
        }

        public int SpriteSheet {
            get;
            private set;
        }

        public int Size {
            get;
            private set;
        }

        public int Pointer {
            get;
            private set;
        }

        /// <summary>
        /// Get if the batch is full.
        /// </summary>
        public bool IsFull {
            get { return this.Pointer < this.Size; }
        }

        public BatchData[ ] Data;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shader" >Index of the shader used to render the current batch</param>
        /// <param name="sprite_sheet" >Index of the sprite sheet used by the entire batch</param>
        public Batch( int shader, int sprite_sheet ) {
            this.Shader = shader;
            this.SpriteSheet = sprite_sheet;
            this.Size = 256;
            this.Pointer = 0;
            this.Data = new BatchData[ this.Size ];
        }

        /// <summary>
        /// Add data for sprite rendering to the batch.
        /// </summary>
        /// <param name="position" >Reference to the position where draw the sprite on screen</param>
        /// <param name="frame" >Index of the frame used to draw the sprite</param>
        /// <param name="filter" >Reference to the filter used to draw the sprite</param>
        /// <param name="rotation" >Rotation of the sprite on the screen</param>
        /// <param name="scale" >Reference to the scale of the sprite on screen</param>
        /// <param name="depth" >Depath value of the sprite</param>
        public void Add( ref Vector2 position, int frame, ref Color filter, float rotation, ref Vector2 scale, float depth ) {
            this.Data[ this.Pointer ].Position = position;
            this.Data[ this.Pointer ].Frame = frame;
            this.Data[ this.Pointer ].Filter = filter;
            this.Data[ this.Pointer ].Rotation = rotation;
            this.Data[ this.Pointer ].Scale = scale;
            this.Data[ this.Pointer ].Depth = depth;

            this.Pointer++;
        }

    }

}
