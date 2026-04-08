using UnityEngine;

public static class Constants
{
    // Player Movement
    public const float MOVE_SPEED = 7.0f;
    public const float SPRINT_SPEED = 12.0f;
    public const float AIR_SPEED = SPRINT_SPEED + 2;
    public const float ROTATION_SPEED = 100f;

    // Player Physics
    public const float GRAVITY = -9.81f;
    public const float JUMP_POWER = 3.0f;
    public const float JUMP_HORIZONTAL_BOOST = 4.0f;
    public const float GRAVITY_MULTIPLIER_FALLING = 2.3f;

    // Player Camera
    public const float VERTICAL_ROTATION_MIN = -80f;
    public const float VERTICAL_ROTATION_MAX = 80f;

    // Player Warp
    public const float MAX_WARP_DISTANCE = 50f;
    public const float WARP_OFFSET = 0.1f;
}
