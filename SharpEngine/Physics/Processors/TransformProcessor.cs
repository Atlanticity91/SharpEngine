using Microsoft.Xna.Framework;

using SharpEngine.Kernel;
using SharpEngine.Kernel.Processors;
using SharpEngine.Kernel.Processors.Types;
using SharpEngine.Physics.Components;

namespace SharpEngine.Physics.Processors {

    public class TransformProcessor : GameProcessor<Transform>, IUProcessor {

        private Vector2 Position;

        /// <summary>
        /// Constructor
        /// </summary>
        public TransformProcessor( ) : base( ) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="timer"></param>
        /// <param name="value"></param>
        /// <param name="target"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        private float GetTransform( ETransformTypes type, int timer, float value, float target, int duration ) {
            switch ( type ) {
                case ETransformTypes.LINEAR        : return ( value + target );

                case ETransformTypes.EASE_IN_SIN   : return 0f;
                case ETransformTypes.EASE_IN_EXPO  : return 0f;

                case ETransformTypes.EASE_OUT_SIN  : return 0f;
                case ETransformTypes.EASE_OUT_EXPO : return 0f;
            }

            return value;
        }

        /// <summary>
        /// Get the next game component who can be process by the processor.
        /// </summary>
        /// <returns>True of false</returns>
        protected override bool GetNext( ) {
            do {
                this.CurrentID++;
            } while ( this.CurrentID < this.Size && !this.Content[ this.CurrentID ].IsEnable && this.Content[ this.CurrentID ].TransformX != ETransformTypes.NONE || this.Content[ this.CurrentID ].TransformY != ETransformTypes.NONE );

            if ( this.CurrentID < this.Size && this.Content[ this.CurrentID ].TransformX != ETransformTypes.NONE || this.Content[ this.CurrentID ].TransformY != ETransformTypes.NONE ) {
                this.Current = this.Content[ this.CurrentID ];

                return true;
            }

            this.CurrentID = -1;
            this.Current = null;

            return false;
        }

        /// <summary>
        /// Process the transform processor.
        /// </summary>
        /// <param name="game_time" >Reference to the current game time</param>
        /// <param name="world" >Reference to the current world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public void Process( ref GameTime game_time, ref GameWorld world, ref GameProcessorManager processor_manager ) {
            while ( this.GetNext( ) ) {
                this.Position = world.FastGet( this.CurrentID ).Position;

                this.Position.X = this.GetTransform( this.Current.TransformX, this.Current.TimerX, this.Position.X, this.Current.Target.X, this.Current.DurationX );
                this.Position.Y = this.GetTransform( this.Current.TransformY, this.Current.TimerY, this.Position.Y, this.Current.Target.Y, this.Current.DurationY );

                if ( this.Current.TimerX == this.Current.DurationX )
                    this.Current.TransformX = ETransformTypes.NONE;

                if ( this.Current.TimerY == this.Current.DurationY )
                    this.Current.TransformY = ETransformTypes.NONE;

                world.Move( this.CurrentID, ref this.Position );
            }
        }

    }

}
