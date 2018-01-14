using Microsoft.Xna.Framework;

namespace SharpEngine.Graphics.Components {

    public class Skin : Kernel.GameComponent {

        public int Shader;
        public int SpriteSheet;
        public int Sprite;
        public Color Color;
        public float Opacity;
        public float Depth;

        /// <summary>
        /// Get the filter of the skin.
        /// </summary>
        public Color Filter {
            get { return this.Color * this.Opacity; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Skin( ) : base( ) {
        }

    }

}
