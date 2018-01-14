using SharpEngine.Kernel;

namespace SharpEngine.AI {

    public class StateMachine : GameComponent {

        public StateMachine MyRef;
        public bool HasChanged;
        public string CurrentState;

        /// <summary>
        /// Constructor
        /// </summary>
        public StateMachine( ) : base( ) {
            this.MyRef = this;
            this.CurrentState = "";
        }

        /// <summary>
        /// Set the state of the current state machine.
        /// </summary>
        /// <param name="name" >Name of the new state of the current state machine</param>
        /// <returns>The current state machine</returns>
        public StateMachine SetState( string name ) {
            this.HasChanged = true;
            this.CurrentState = name;

            return this;
        }

    }

}
