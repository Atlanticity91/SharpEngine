using SharpEngine.Graphics.Components;
using SharpEngine.Graphics.Rendering;
using SharpEngine.Kernel;
using SharpEngine.Kernel.Processors;
using SharpEngine.Kernel.Processors.Types;

namespace SharpEngine.Graphics.Processors {

    public class AnimationProcessor : GameProcessor<Animation>, IDProcessor {

        /// <summary>
        /// Constructor
        /// </summary>
        public AnimationProcessor( ) : base( ) {
        }

        /// <summary>
        /// Process the current game animation processor.
        /// </summary>
        /// <param name="renderer" >Reference to the current game renderer</param>
        /// <param name="world" >Reference to the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public void Process( ref GameRenderer renderer, ref GameWorld world, ref GameProcessorManager processor_manager ) {
            while ( this.GetNext( ) ) {
                this.Current.Time++;
                if ( this.Current.Time == this.Current.Duration ) {
                    this.Current.Time = 0;

                    this.Current.Current++;
                    if ( this.Current.Current == this.Current.FrameCount )
                        this.Current.Current = 0;

                    processor_manager.GetComponenOf<Skin>( this.CurrentID ).Sprite = this.Current.Frame;
                }
            }
        }

    }

}
