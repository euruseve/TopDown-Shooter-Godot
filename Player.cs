using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    private int _speed = 100;


    public override void _Process(double delta)
    {
        Vector2 movementDirection = Vector2.Zero;

        if(Input.IsActionPressed("Up"))
            movementDirection.Y = -1;
        if(Input.IsActionPressed("Down"))
            movementDirection.Y = 1;
        if(Input.IsActionPressed("Left"))
            movementDirection.X = -1;
        if(Input.IsActionPressed("Right"))
            movementDirection.X = 1;

        movementDirection = movementDirection.Normalized();

        MoveAndCollide(movementDirection * _speed * (float)delta);

        LookAt(GetGlobalMousePosition());
    }

}
