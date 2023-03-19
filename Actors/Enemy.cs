using Godot;
using System;
using TopDownShooter;

public partial class Enemy : CharacterBody2D, IHandleHitted
{
    private Health _health;

    public override void _Ready()
    {
        _health = GetNode<Health>("Health");
    }

    public void HandleHit()
    {
        _health.HealthValue -= 20;

        if(_health.HealthValue <= 0)
            QueueFree();
    }
}
