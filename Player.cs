using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void PlayerFiredBulletEventHandler(Bullet bullet, Vector2 position, Vector2 direction);

    [Export]
    private int _speed = 100;

    [Export]
    private PackedScene _bullet;

    private Marker2D _endOfGun;
    private Marker2D _gunDirection;

    private Timer _attackCooldown;
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _endOfGun = GetNode<Marker2D>("EndOfGun");
        _gunDirection = GetNode<Marker2D>("GunDirection");
        _attackCooldown = GetNode<Timer>("AttackCooldown");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

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
            Shoot();
    }

    private void Shoot()
    {
        if(_attackCooldown.IsStopped())
        {
            var bulletInstance = _bullet.Instantiate() as Bullet;
            var direction = (_gunDirection.GlobalPosition - _endOfGun.GlobalPosition).Normalized();

            EmitSignal(SignalName.PlayerFiredBullet, bulletInstance, _endOfGun.GlobalPosition, direction);
            _attackCooldown.Start();
            _animationPlayer.Play("MuzzleFlash");
        } 
    }
}
