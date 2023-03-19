using Godot;
using System;
using TopDownShooter;

public partial class Player : CharacterBody2D, IHandleHitted
{
    [Signal]
    public delegate void PlayerFiredBulletEventHandler(Bullet bullet, Vector2 position, Vector2 direction);

    [Export]
    private int _speed = 100;

    private Health _health;
    private Weapon _weapon;

    public override void _Ready()
    {
        _health = GetNode<Health>("Health");
        _weapon = GetNode<Weapon>("Weapon");

        _weapon.WeaponFired += this.Shoot;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 movementDirection = Vector2.Zero;

        if (Input.IsActionPressed("Up"))
            movementDirection.Y = -1;
        if (Input.IsActionPressed("Down"))
            movementDirection.Y = 1;
        if (Input.IsActionPressed("Left"))
            movementDirection.X = -1;
        if (Input.IsActionPressed("Right"))
            movementDirection.X = 1;

        movementDirection = movementDirection.Normalized();

        MoveAndCollide(movementDirection * _speed * (float)delta);

        LookAt(GetGlobalMousePosition());
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased("Shoot"))
            _weapon.Shoot();
    }

    private void Shoot(Bullet bullet, Vector2 position, Vector2 direction)
    {
        EmitSignal(SignalName.PlayerFiredBullet, bullet, position, direction);
    }

    public void HandleHit()
    {
        _health.HealthValue -= 20;
        GD.Print("Player hitted ", _health);
    }
}
