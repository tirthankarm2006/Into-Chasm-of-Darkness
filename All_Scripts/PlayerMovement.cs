using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	public const float Speed = 400f;
	public const float JumpVelocity = 100f;
	public const float Gravity = 300f;
	float JumpBoost = JumpVelocity;
	bool IsJumping = false;
	public float MouseSensitivity = 0.5f;
	Node3D cameraPivot;


    public override void _Ready()
    {
		cameraPivot = GetNode<Node3D>("Head");
		Input.MouseMode = Input.MouseModeEnum.Captured;
    }


	public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
		{
            this.RotateY(-Mathf.DegToRad(mouseMotion.Relative.X * MouseSensitivity));

            float pitchChange = -mouseMotion.Relative.Y * MouseSensitivity;
            cameraPivot.RotateX(Mathf.DegToRad(pitchChange));

            Vector3 rot = cameraPivot.RotationDegrees;
            rot.X = Mathf.Clamp(rot.X, -89, 89);
            cameraPivot.RotationDegrees = rot;
        }
    }

	public override void _PhysicsProcess(double delta)
	{
			
		Vector2 inputDir = Input.GetVector("left", "right", "forward", "backward");
		Vector3 direction = new Vector3(inputDir.X, 0f, inputDir.Y).Normalized();

		Vector3 velocity = Velocity;
		
		if (direction != Vector3.Zero)
		{
			velocity = Transform.Basis * new Vector3(direction.X * Speed, 0, direction.Z * Speed) * (float)delta;
		}
        else
        {
			velocity = Vector3.Zero;
        }

		if (!IsOnFloor() && !IsJumping)
		{
			velocity.Y -= Gravity * (float)delta;
		}
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
    	{
        	IsJumping = true;
        	JumpBoost = JumpVelocity;
    	}

    	if (IsJumping)
    	{	
        	velocity.Y = Mathf.Lerp(velocity.Y, JumpBoost, 5f * (float)delta);
        	JumpBoost -= Gravity * (float)delta;

        	if (JumpBoost <= 0) IsJumping = false;
    	}

		Velocity = velocity;
		MoveAndSlide();
	}
		
		
}
