using Godot;
using System;

public partial class Weapon : Node2D
{
    [Signal]
    public delegate void WeaponFiredEventHandler(Bullet bullet, Vector2 position, Vector2 direction);


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

    public void Shoot()
    {
        if (_attackCooldown.IsStopped() && _bullet != null)
        {
            var bulletInstance = _bullet.Instantiate() as Bullet;
            var direction = (_gunDirection.GlobalPosition - _endOfGun.GlobalPosition).Normalized();

            EmitSignal(SignalName.WeaponFired, bulletInstance, _endOfGun.GlobalPosition, direction);
            _attackCooldown.Start();
            _animationPlayer.Play("MuzzleFlash");
        }
    }
}
