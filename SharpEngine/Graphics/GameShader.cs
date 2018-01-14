using Microsoft.Xna.Framework.Graphics;

namespace SharpEngine.Graphics {

    public abstract class GameShader {

        public Effect Effect;
        public int Technique;
        public int Passe;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="technique" >Index of the used technique of the shader</param>
        /// <param name="passe" >Index of the used passes on the used technique</param>
        public GameShader( int technique = 0, int passe = 0 ) {
            this.Effect = null;
            this.Technique = technique;
            this.Passe = passe;
        }

        /// <summary>
        /// Process the current game shader.
        /// </summary>
        public virtual void Process( ) {
            this.Effect.Techniques[ this.Technique ].Passes[ this.Passe ].Apply( );
        }

    }

}
