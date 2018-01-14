using Microsoft.Xna.Framework;

using SharpEngine.Kernel;
using SharpEngine.Kernel.Processors;

namespace SharpEngine.AI {

    public abstract class GameAIState {

        public GameAIState MyRef;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameAIState( ) {
            this.MyRef = this;
        }

        /// <summary>
        /// On enter on the current state.
        /// </summary>
        /// <param name="owner" >Index of the game object who "own" the state machine component</param>
        /// <param name="ai_processor" >Reference to the current A.I. processor</param>
        /// <param name="game_time" >Reference to the current game time</param>
        /// <param name="world" >Reference to the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public abstract void OnEnter( int owner, ref AIProcessor ai_processor, ref GameTime game_time, ref GameWorld world, ref GameProcessorManager processor_manager );

        /// <summary>
        /// Process method of the current state.
        /// </summary>
        /// <param name="owner" >Index of the game object who "own" the state machine component</param>
        /// <param name="ai_processor" >Reference to the current A.I. processor</param>
        /// <param name="game_time" >Reference to the current game time</param>
        /// <param name="world" >Reference to the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public abstract void Process( int owner, ref AIProcessor ai_processor, ref GameTime game_time, ref GameWorld world, ref GameProcessorManager processor_manager );

        /// <summary>
        /// On quit the current state.
        /// </summary>
        /// <param name="owner" >Index of the game object who "own" the state machine component</param>
        /// <param name="ai_processor" >Reference to the current A.I. processor</param>
        /// <param name="game_time" >Reference to the current game time</param>
        /// <param name="world" >Reference to the current game world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public abstract void OnQuit( int owner, ref AIProcessor ai_processor, ref GameTime game_time, ref GameWorld world, ref GameProcessorManager processor_manager );

    }

}
