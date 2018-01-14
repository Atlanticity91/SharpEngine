using SharpEngine.Kernel;

namespace SharpEngine.Physics.Components {

    public class Collider : GameComponent {

        public EColliderTypes Type;
        public float Width;
        public float Height;

        /// <summary>
        /// Constructor
        /// </summary>
        public Collider( ) : base( ) {
        }

        /// <summary>
        /// Set the size of the collider.
        /// ( Defined the collider as a rectangle )
        /// </summary>
        /// <param name="width" >Width of the collider</param>
        /// <param name="height">Height of the collider</param>
        /// <returns>The current collider</returns>
        public Collider SetSize( float width, float height ) {
            if ( this.Type != EColliderTypes.RECTANGLE )
                this.Type = EColliderTypes.RECTANGLE;

            this.Width = width;
            this.Height = height;

            return this;
        }

        /// <summary>
        /// Set the radius of the collider.
        /// ( Defined the collider as a circle )
        /// </summary>
        /// <param name="radius"></param>
        /// <returns>The current collider</returns>
        public Collider SetRadius( float radius ) {
            if ( this.Type != EColliderTypes.CIRCLE )
                this.Type = EColliderTypes.CIRCLE;

            this.Width = radius;
            this.Height = 2f * radius;

            return this;
        }

        /// <summary>
        /// Set the collider as a point.
        /// </summary>
        /// <returns>The current collider</returns>
        public Collider SetPoint( ) {
            if ( this.Type != EColliderTypes.POINT )
                this.Type = EColliderTypes.POINT;

            return this;
        }

    }

}
