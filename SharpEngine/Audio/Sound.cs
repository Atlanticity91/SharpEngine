using SharpEngine.Kernel;

namespace SharpEngine.Audio {

    public class Sound : GameComponent {

        public int SoundID;
        public float Volume;
        public float Pitch;
        public float Pan;

        /// <summary>
        /// Constructor
        /// </summary>
        public Sound( ) : base( ) {
            this.SoundID = -1;
            this.Volume = 1f;
            this.Pitch = 0f;
            this.Pan = 0f;
        }

    }

}
