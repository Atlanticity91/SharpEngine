using SharpEngine.Graphics.Rendering;

namespace SharpEngine.Kernel.Processors.Types {

    public interface IDProcessor {

        /// <summary>
        /// Process for draw processor.
        /// </summary>
        /// <param name="renderer" >Reference to the current game renderer</param>
        /// <param name="world" >Reference to the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        void Process( ref GameRenderer renderer, ref GameWorld world, ref GameProcessorManager processor_manager );

    }

}
