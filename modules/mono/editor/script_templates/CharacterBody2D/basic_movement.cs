// meta-description: Classic movement for gravity games (platformer, ...)

using _BINDINGS_NAMESPACE_;
using System;

public partial class CharacterBody2d : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float Accel = 300.0f;
	public const float JumpVelocity = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Handle gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Get the vertical velocity (for jump).
		Vector2 verticalVelocity = velocity.Project(UpDirection);

		// Get the horizontal velocity (for acceleration).
		Vector2 horizontalVelocity = velocity - verticalVelocity;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			verticalVelocity = UpDirection * JumpVelocity;
		}

		// Get the input axis. As good practice, you should replace UI actions with custom gameplay actions.
		float inputAxis = Input.GetAxis("ui_left", "ui_right");

		// Calculate the intended direction in 2D plane.
		Vector2 inputDirection = new Vector2(inputAxis, 0).Rotated(Rotation);

		// Calculate the target horizontal velocity.
		Vector2 targetHorizontalVelocity = inputDirection * Speed;

		// Move the current horizontal velocity towards the target horizontal velocity by the acceleration factor multiplied by delta.
		horizontalVelocity = horizontalVelocity.MoveToward(targetHorizontalVelocity, Accel * (float)delta);

		// Compose the final velocity.
		velocity = horizontalVelocity + verticalVelocity;

		Velocity = velocity;
		MoveAndSlide();
	}
}
