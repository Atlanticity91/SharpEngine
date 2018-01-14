using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using SharpEngine.Kernel;
using SharpEngine.Kernel.Processors;
using SharpEngine.Kernel.Processors.Types;

namespace SharpEngine.Audio {

    public class SoundProcessor : GameProcessor<Sound>, IUProcessor {

        private AudioManager AudioManager;
        private SoundEffect Sound;
        private Song Music;

        /// <summary>
        /// Constructor
        /// </summary>
        public SoundProcessor( ) : base( ) {
            this.AudioManager = AudioManager.INSTANCE;

            this.Sound = null;
            this.Music = null;
        }

        /// <summary>
        /// Process the current 
        /// </summary>
        /// <param name="game_time" >Reference to the current game time</param>
        /// <param name="world" >Reference to the current world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public void Process( ref GameTime game_time, ref GameWorld world, ref GameProcessorManager processor_manager ) {
            while ( this.GetNext( ) ) {
                this.Sound = this.AudioManager.GetSound( this.Current.SoundID );
                this.Sound.Play( this.Current.Volume, this.Current.Pitch, this.Current.Pan );
            }
        }

    }

}
