using SharpEngine.Kernel;

namespace SharpEngine.Graphics.Components {

    public class Animation : GameComponent {

        public int Time;
        public int Current;
        public int FrameCount;

        public int[ ] Durations;
        public int[ ] Frames;

        /// <summary>
        /// Get the duration of the current frame.
        /// </summary>
        public int Duration {
            get { return this.Durations[ this.Current ]; }
        }

        /// <summary>
        /// Get the current frame value.
        /// </summary>
        public int Frame {
            get { return this.Frames[ this.Current ]; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Animation( ) : base( ) {
        }

        /// <summary>
        /// Setup the current animation.
        /// </summary>
        /// <param name="frame_count" >Count of frame of the animation</param>
        /// <returns>The current animation</returns>
        public Animation Setup( int frame_count ) {
            this.Current = 0;
            this.FrameCount = frame_count;
            this.Durations = new int[ frame_count ];
            this.Frames = new int[ frame_count ];

            return this;
        }

        /// <summary>
        /// Set a frame of the current animation.
        /// </summary>
        /// <param name="index" >Index of the frame on the animation</param>
        /// <param name="duration" >Duration of the frame</param>
        /// <param name="frame" >Index of the sprite of the frame</param>
        /// <returns>The current animation</returns>
        public Animation SetFrame( int index, int duration, int frame ) {
            if ( index > -1 && index < this.FrameCount ) {
                this.Durations[ index ] = duration;
                this.Frames[ index ] = frame;
            }

            return this;
        }

    }

}
