using Godot;
using System;
using TopDownShooter;

public partial class Bullet : Area2D
{
    [Export]
    private int _speed = 10;

    private Timer _killTimer;

    private Vector2 _direction = Vector2.Zero;

    public override void _Ready()
    {
        base._Ready();

        _killTimer = GetNode<Timer>("KillTimer");

        _killTimer.Start();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if(_direction != Vector2.Zero) 
        {
            Vector2 velocity = _direction * _speed;

            this.GlobalPosition += velocity;
        }
    }

    public void OnKillTimerTimeout()
    {
        QueueFree();
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
        this.Rotation += direction.Angle();
    }

    public void OnBodyEntered(Node body)
    {
        var hh = body as IHandleHitted;
        if(hh != null)
        {
            hh.HandleHit();
            QueueFree();
        }
    }
}
