using UnityEngine;

namespace FrostFalls
{
    [CreateAssetMenu]
    public class SettingsStore : ScriptableObject
    {
        #region Layers
        [Header("LAYERS")]
        [Tooltip("Set this to the layer your player is on")]
        public LayerMask PlayerLayer;
        #endregion

        #region Input
        [Header("INPUT")]
        [Tooltip("Makes all Input snap to an integer. Prevents gamepads from walking slowly. Recommended value is true to ensure gamepad/keybaord parity.")]
        public bool SnapInput = true;

        [Tooltip("Minimum input required before you mount a ladder or climb a ledge. Avoids unwanted climbing using controllers"), Range(0.01f, 0.99f)]
        public float VerticalDeadZoneThreshold = 0.3f;

        [Tooltip("Minimum input required before a left or right is recognized. Avoids drifting with sticky controllers"), Range(0.01f, 0.99f)]
        public float HorizontalDeadZoneThreshold = 0.1f;
        #endregion

        #region Movement
        [Header("MOVEMENT")]
        [Tooltip("The top horizontal movement speed")]
        public float MaxSpeed = 14;

        [Tooltip("The player's capacity to gain horizontal speed")]
        public float Acceleration = 120;

        [Tooltip("The pace at which the player comes to a stop")]
        public float GroundDeceleration = 60;

        [Tooltip("Deceleration in air only after stopping input mid-air")]
        public float AirDeceleration = 30;

        [Tooltip("A constant downward force applied while grounded. Helps on slopes"), Range(0f, -10f)]
        public float GroundingForce = -1.5f;

        [Tooltip("The detection distance for grounding and roof detection"), Range(0f, 0.5f)]
        public float GrounderDistance = 0.05f;
        #endregion

        #region Jump
        [Header("JUMP")] [Tooltip("The immediate velocity applied when jumping")]
        public float JumpPower = 36;

        [Tooltip("The maximum vertical movement speed")]
        public float MaxFallSpeed = 40;

        [Tooltip("The player's capacity to gain fall speed. a.k.a. In Air Gravity")]
        public float FallAcceleration = 110;

        [Tooltip("The gravity multiplier added when jump is released early")]
        public float JumpEndEarlyGravityModifier = 3;

        [Tooltip("The time before coyote jump becomes unusable. Coyote jump allows jump to execute even after leaving a ledge")]
        public float CoyoteTime = .15f;

        [Tooltip("The amount of time we buffer a jump. This allows jump input before actually hitting the ground")]
        public float JumpBuffer = .2f;
        #endregion

        #region Grapple
        [Header("GRAPPLE")]
        [Tooltip("Set this to the layers considered grappleable surfaces")]
        public LayerMask GrappleableLayers;
        [Tooltip("Maximum distance the grappling hook can travel from the player to attach to a grappleable surface")]
        public float GrappleDistance = 10f;
        [Tooltip("Position from where the grapple is shot, typically the player's hand or a grappling device")]
        public Transform direction;
        #endregion
    }
}