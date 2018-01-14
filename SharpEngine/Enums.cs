namespace SharpEngine {

    /// <summary>
    /// All transformation can be emulated by the engine.
    /// </summary>
    public enum ETransformTypes {

        NONE                = 0,
        LINEAR              = 1,

        EASE_IN_SIN         = 2,
        EASE_IN_EXPO        = 3,

        EASE_OUT_SIN        = 4,
        EASE_OUT_EXPO       = 5,

    };
    
    /// <summary>
    /// All collider type can be emulated by the engine.
    /// </summary>
    public enum EColliderTypes {

        POINT           = 0,
        RECTANGLE       = 1,
        CIRCLE          = 2,

    };

}
