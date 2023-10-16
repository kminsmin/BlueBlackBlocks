using UnityEngine;

[CreateAssetMenu(menuName = "Player Controller Data")]
public class PlayerControllerSO : ScriptableObject
{
	[Header("LAYERS")] [Tooltip("Set this to the layer your player is on")]
	public LayerMask PlayerLayer;
	
	[Header("Gravity")]
	//Downwards force (gravity) needed for the desired jumpHeight and jumpTimeToApex.
	[HideInInspector] public float gravityStrength;
	//Strength of the player's gravity as a multiplier of gravity (set in ProjectSettings/Physics2D).
	//Also the value the player's rigidbody2D.gravityScale is set to.
	[HideInInspector] public float gravityScale; 
	[Space(5)]
	[Tooltip("Multiplier to the player's gravityScale when falling.")]
	public float fallGravityMult = 1.5f;
	[Tooltip("Maximum fall speed (terminal velocity) of the player when falling.")]
	public float maxFallSpeed = 25f;
	
	[Space(5)]
	[Tooltip("Larger multiplier to the player's gravityScale when they are falling and a downwards input is pressed.\nSeen in games such as Celeste, lets the player fall extra fast if they wish.")]
	public float fastFallGravityMult = 2f;
	[Tooltip("Maximum fall speed(terminal velocity) of the player when performing a faster fall.")]
	public float maxFastFallSpeed = 30f;
	
	[Space(5)]
	[Tooltip("The detection distance for grounding detection"), Range(0f, 0.5f)]
	public float GrounderDistance = 0.05f;
	[Tooltip("The detection distance for wall detection"), Range(0f, 0.5f)]
	public float WallDistance = 0.05f;
	
	[Space(20)]

	[Header("Run")]
	[Tooltip("Target speed we want the player to reach."), Range(0.01f, 100f)]
	public float runMaxSpeed = 11f;
	[Tooltip("The speed at which our player accelerates to max speed, can be set to runMaxSpeed for instant acceleration down to 0 for none at all")]
	public float runAcceleration = 2.5f;
	[HideInInspector] public float runAccelAmount; //The actual force (multiplied with speedDiff) applied to the player.
	[Tooltip("The speed at which our player decelerates from their current speed, can be set to runMaxSpeed for instant deceleration down to 0 for none at all")]
	public float runDecceleration = 5f;
	[HideInInspector] public float runDeccelAmount; //Actual force (multiplied with speedDiff) applied to the player .
	[Space(5)]
	[Tooltip("Multipliers applied to acceleration rate when airborne.")]
	[Range(0f, 1)] public float accelInAir = 0.65f;
	[Range(0f, 1)] public float deccelInAir = 0.65f;
	[Space(5)]
	public bool doConserveMomentum = true;

	[Space(20)]

	[Header("Jump")]
	[Tooltip("Height of the player's jump")]
	public float jumpHeight = 3.5f;
	[Tooltip("Time between applying the jump force and reaching the desired jump height. These values also control the player's gravity and jump force."), Range(0.01f, 10f)]
	public float jumpTimeToApex = 0.3f;
	[HideInInspector] public float jumpForce; //The actual force applied (upwards) to the player when they jump.

	[Header("Both Jumps")]
	[Tooltip("Multiplier to increase gravity if the player releases the jump button while still jumping")]
	public float jumpCutGravityMult = 2f;
	[Tooltip("Reduces gravity while close to the apex (desired max height) of the jump"), Range(0f, 1)]
	public float jumpHangGravityMult = 0.5f;
	[Tooltip("Speeds (close to 0) where the player will experience extra \"jump hang\". The player's velocity.y is closest to 0 at the jump's apex (think of the gradient of a parabola or quadratic function)")]
	public float jumpHangTimeThreshold = 1f;
	[Space(0.5f)]
	public float jumpHangAccelerationMult = 1.1f; 
	public float jumpHangMaxSpeedMult = 1.3f; 				

	[Header("Wall Jump")]
	[Tooltip("The actual force (this time set by us) applied to the player when wall jumping.")]
	public Vector2 wallJumpForce = new Vector2(15f, 25f);
	[Space(5)]
	[Tooltip("Reduces the effect of player's movement while wall jumping."), Range(0f, 1f)]
	public float wallJumpRunLerp = 0.5f;
	[Tooltip("Time after wall jumping the player's movement is slowed for."), Range(0f, 1.5f)]
	public float wallJumpTime = 0.15f;
	[Tooltip("Player will rotate to face wall jumping direction")]
	public bool doTurnOnWallJump = false;

	[Space(20)]

	[Header("Slide")]
	public float slideSpeed = 0f;
	public float slideAccel = 0f;

    [Header("Assists")]
	[Tooltip("Grace period after falling off a platform, where you can still jump"), Range(0.01f, 0.5f)]
    public float coyoteTime = 0.1f;
	[Tooltip("Grace period after pressing jump where a jump will be automatically performed once the requirements (eg. being grounded) are met."), Range(0.01f, 0.5f)]
	public float jumpInputBufferTime = 0.1f;
	

	//Unity Callback, called when the inspector updates
    private void OnValidate()
    {
		//Calculate gravity strength using the formula (gravity = 2 * jumpHeight / timeToJumpApex^2) 
		gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
		
		//Calculate the rigidbody's gravity scale (ie: gravity strength relative to unity's gravity value, see project settings/Physics2D)
		gravityScale = gravityStrength / Physics2D.gravity.y;

		//Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
		runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
		runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

		//Calculate jumpForce using the formula (initialJumpVelocity = gravity * timeToJumpApex)
		jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

		#region Variable Ranges
		runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
		runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
		#endregion
	}
}