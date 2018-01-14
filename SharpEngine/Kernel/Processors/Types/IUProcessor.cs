using Microsoft.Xna.Framework;

namespace SharpEngine.Kernel.Processors.Types {

    public interface IUProcessor {

        /// <summary>
        /// Process for update processor.
        /// </summary>
        /// <param name="game_time" >Reference to the current game time</param>
        /// <param name="world" >Reference to the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        void Process( ref GameTime game_time, ref GameWorld world, ref GameProcessorManager processor_manager );

    }

}
