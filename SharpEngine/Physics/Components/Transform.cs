using Microsoft.Xna.Framework;

namespace SharpEngine.Physics.Components {

    public class Transform : Kernel.GameComponent {

        public ETransformTypes TransformX;
        public ETransformTypes TransformY;

        public int TimerX;
        public int TimerY;
        public int DurationX;
        public int DurationY;
        public Vector2 Target;

        /// <summary>
        /// Constructor
        /// </summary>
        public Transform( ) : base() {
        }

        /// <summary>
        /// Set the velocity of the transform component.
        /// ( Defined the transform type to linear )
        /// </summary>
        /// <param name="x" >Velocity for x axe</param>
        /// <param name="y" >Velocity for y axe</param>
        /// <returns>The current transform component</returns>
        public Transform SetVelocity( float x, float y ) {
            if ( this.TransformX != ETransformTypes.LINEAR )
                this.TransformX = ETransformTypes.LINEAR;

            if ( this.TransformY != ETransformTypes.LINEAR )
                this.TransformY = ETransformTypes.LINEAR;

            this.Target.X = x;
            this.Target.Y = y;

            return this;
        }

        /// <summary>
        /// Set the target position of the transform component.
        /// ( Use this to use tweening )
        /// </summary>
        /// <param name="transformation_type_x" >Type of transformation on x</param>
        /// <param name="transformation_type_y" >Type of transformation on y</param>
        /// <param name="duration_x" >Duration of the transformation on x</param>
        /// <param name="duration_y" >Duration of the transformation on y</param>
        /// <param name="target" >Target position of the transform component</param>
        /// <returns>The current transform component</returns>
        public Transform SetTarget( ETransformTypes transformation_type_x, ETransformTypes transformation_type_y, int duration_x, int duration_y, ref Vector2 target ) {
            this.TransformX = transformation_type_x;
            this.TransformY = transformation_type_y;

            this.TimerX = 0;
            this.TimerY = 0;
            this.DurationX = duration_x;
            this.DurationY = duration_y;
            this.Target = target;

            return this;
        }

    }

}
