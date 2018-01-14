using Microsoft.Xna.Framework;

using SharpEngine.Kernel;
using SharpEngine.Kernel.Processors;
using SharpEngine.Kernel.Processors.Types;
using SharpEngine.Physics.Components;

namespace SharpEngine.Physics.Processors {

    public struct ColliderData {

        public float X;
        public float Y;
        public float Width;
        public float Height;

    };

    public class ColliderProcessor : GameProcessor<Collider>, IUProcessor {

        private ColliderData ColliderOne;
        private ColliderData ColliderTwo;
        private ColliderData CircleBox;
        private ColliderData Temp;
        private Vector2 Offset;

        /// <summary>
        /// Constructor
        /// </summary>
        public ColliderProcessor( ) : base( ) {
        }

        /// <summary>
        /// Make a collider.
        /// </summary>
        /// <param name="x" >Collider x value</param>
        /// <param name="y" >Collider y value</param>
        /// <param name="collider" >Reference to the current collider component</param>
        /// <param name="data" >Reference to the output collider data</param>
        private void MakeCollider( float x, float y, ref Collider collider, ref ColliderData data ) {
            data.X = x;
            data.Y = y;
            data.Width = collider.Width;
            data.Height = collider.Height;
        }

        /**
         * Based on OpenClassroom "Theorie des collisions"
         **/
        #region TEMPORARY COLLISION FUNCTIONS
        private bool PointRectangle( ref ColliderData point, ref ColliderData rectangle ) {
            if ( 
                 point.X > rectangle.X && point.X < rectangle.X + rectangle.Width  && 
                 point.Y > rectangle.Y && point.Y < rectangle.Y + rectangle.Height 
                ) {
                this.Offset.X = rectangle.X - point.X;
                this.Offset.Y = rectangle.Y - point.Y;

                return true;
            }

            return false;
        }

        private bool RectangleRectangle( ref ColliderData rect_one, ref ColliderData rect_two ) {
            if (
                ( rect_two.X > rect_one.X + rect_one.Width  ) || // trop a droite
                ( rect_two.X + rect_two.Width < rect_one.X  ) || // trop a gauche
                ( rect_two.Y > rect_one.Y + rect_one.Height ) || // trop en base
                ( rect_two.Y + rect_two.Height < rect_one.Y )    // trop en haut
               )
                return false;
            else {
                this.Offset.X = rect_two.X - rect_one.X;
                this.Offset.Y = rect_two.Y - rect_one.Y;

                return true;
            }
        }

        private bool PointCircle( ref ColliderData point, ref ColliderData circle ) {
            var dist = ( point.X - circle.X ) * ( point.X - circle.X ) + ( point.Y - circle.Y ) * ( point.Y - circle.Y );

            if ( dist > circle.Height )
                return false;
            else {
                this.Offset.X = 0f;
                this.Offset.Y = 0f;

                return true;
            }
        }

        private bool PointCircle( float x, float y, ref ColliderData circle ) {
            var dist = ( x - circle.X ) * ( x - circle.X ) + ( y - circle.Y ) * ( y - circle.Y );

            if ( dist > circle.Height )
                return false;
            else {
                this.Offset.X = 0f;
                this.Offset.Y = 0f;

                return true;
            }
        }

        private bool CircleCircle( ref ColliderData cir_one, ref ColliderData cir_two ) {
            var dist = (cir_one.X - cir_two.X ) * ( cir_one.X - cir_two.X ) + ( cir_one.Y - cir_two.Y )*( cir_one.Y - cir_two.Y );
            var dist_two = ( cir_one.Height + cir_two.Height ) * ( cir_one.Height + cir_two.Height );

            if ( dist > dist_two )
                return false;
            else {
                this.Offset.X = 0f;
                this.Offset.Y = 0f;

                return true;
            }
        }

        private bool RectangleCircle( ref ColliderData rectangle, ref ColliderData circle ) {
            this.GetBox( ref circle );

            if ( this.RectangleRectangle( ref rectangle, ref this.CircleBox ) )
                return false;

            if ( 
                PointCircle( rectangle.X, rectangle.Y, ref circle ) ||
                PointCircle( rectangle.X, rectangle.Y + rectangle.Height, ref circle ) ||
                PointCircle( rectangle.X + rectangle.Width, rectangle.Y, ref circle )  ||
                PointCircle( rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height, ref circle )
               ) {
                this.Offset.X = 0f;
                this.Offset.Y = 0f;

                return true;
            }

            if ( PointRectangle( ref circle, ref rectangle ) ) {
                this.Offset.X = 0f;
                this.Offset.Y = 0f;

                return true;
            }

            var proj_vert = ProjectionSegment( circle.X, circle.Y, rectangle.X, rectangle.Y, rectangle.X, rectangle.Y + rectangle.Height );
            var proj_hori = ProjectionSegment( circle.X, circle.Y, rectangle.X, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y );

            if ( proj_vert || proj_hori ) {
                this.Offset.X = 0f;
                this.Offset.Y = 0f;

                return true;
            }

            return false;
        }

        private bool ProjectionSegment( float cir_x, float cir_y, float rect_x, float rect_y, float width, float height ) {
            this.CircleBox.X = cir_x - rect_x;
            this.CircleBox.Y = cir_y - rect_y;
            this.CircleBox.Width = width - rect_x;
            this.CircleBox.Height = height - rect_y;

            this.Temp.X = cir_x - width;
            this.Temp.Y = cir_y - height;
            this.Temp.Width = ( this.CircleBox.X * this.CircleBox.Width ) + ( this.CircleBox.Y * this.CircleBox.Height );
            this.Temp.Height = ( this.Temp.X * this.CircleBox.Width ) + ( this.Temp.Y * this.CircleBox.Height );

            if ( this.Temp.Width * this.Temp.Height > 0 )
                return false;

            return true;
        }

        private void GetBox( ref ColliderData circle ) {
            this.CircleBox.X = circle.X - circle.Width;
            this.CircleBox.Y = circle.Y - circle.Width;

            this.CircleBox.Width = circle.Height;
            this.CircleBox.Height = circle.Height;
        }
        #endregion

        /// <summary>
        /// Process the current game collider processor.
        /// </summary>
        /// <param name="game_time" >Reference to the current game time</param>
        /// <param name="world" >Reference to the current world</param>
        /// <param name="processor_manager" >Reference to the current game processor manager</param>
        public void Process( ref GameTime game_time, ref GameWorld world, ref GameProcessorManager processor_manager ) {
            #region TEMPORARY COLLISION DETECTION BLOCK
            while ( this.GetNext( ) ) {
                this.Offset = world.GetPosition( this.CurrentID );

                this.MakeCollider( this.Offset.X, this.Offset.Y, ref this.Current, ref this.ColliderOne );

                for ( int id = this.CurrentID + 1; id < this.Size; id++ ) {
                    if ( !this.Content[ id ].IsEnable )
                        continue;

                    this.Offset = world.GetPosition( id );
                    this.MakeCollider( this.Offset.X, this.Offset.Y, ref this.Content[ id ], ref this.ColliderTwo );

                    this.Offset.X = 0f;
                    this.Offset.Y = 0f;

                    switch ( this.Current.Type ) {
                        case EColliderTypes.POINT :

                            switch ( this.Content[ id ].Type ) {
                                case EColliderTypes.RECTANGLE :

                                    if ( this.PointRectangle( ref this.ColliderOne, ref this.ColliderTwo ) )
                                        world.Move( this.CurrentID, ref this.Offset );

                                    break;

                                case EColliderTypes.CIRCLE :

                                    if ( this.PointCircle( ref this.ColliderOne, ref this.ColliderTwo ) )
                                        world.Move( this.CurrentID, ref this.Offset );

                                    break;
                            }

                            break;

                        case EColliderTypes.RECTANGLE :

                            switch ( this.Content[ id ].Type ) {
                                case EColliderTypes.POINT :

                                    if ( this.PointRectangle( ref this.ColliderTwo, ref this.ColliderOne ) )
                                        world.Move( this.CurrentID, ref this.Offset );

                                    break;

                                case EColliderTypes.RECTANGLE :

                                    if ( this.RectangleRectangle( ref this.ColliderOne, ref this.ColliderTwo ) )
                                        world.Move( this.CurrentID, ref this.Offset );

                                    break;
                                case EColliderTypes.CIRCLE :

                                    if ( this.RectangleCircle( ref this.ColliderOne, ref this.ColliderTwo ) )
                                        world.Move( this.CurrentID, ref this.Offset );

                                    break;
                            }

                            break;

                        case EColliderTypes.CIRCLE :

                            switch ( this.Content[ id ].Type ) {
                                case EColliderTypes.POINT :

                                    if ( this.PointCircle( ref this.ColliderTwo, ref this.ColliderOne ) )
                                        world.Move( this.CurrentID, ref this.Offset );

                                    break;

                                case EColliderTypes.RECTANGLE:

                                    if ( this.RectangleCircle( ref this.ColliderTwo, ref this.ColliderOne ) )
                                        world.Move( this.CurrentID, ref this.Offset );

                                    break;

                                case EColliderTypes.CIRCLE :

                                    if ( this.PointCircle( ref this.ColliderOne, ref this.ColliderTwo ) )
                                        world.Move( this.CurrentID, ref this.Offset );

                                    break;
                            }

                            break;
                    }
                }
            }
            #endregion
        }

    }

}
