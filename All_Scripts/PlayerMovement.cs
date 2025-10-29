using Godot;
using System;
using System.Numerics;

public partial class PlayerMovement : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 inputDir = Input.GetVector("left", "right", "forward", "backward");
		Vector3 direction = (inputDir.X, 0, inputDir.Y).Normalized();

		this.Position += direction * Speed * (float)delta;
	}
		
		
}
