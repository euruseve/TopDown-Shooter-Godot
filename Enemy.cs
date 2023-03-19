using Godot;
using System;
using TopDownShooter;

public partial class Enemy : CharacterBody2D, IHandleHitted
{
    private int _health = 100;

    public void HandleHit()
    {
        _health -= 20;

        if(_health <= 0 )
            QueueFree();
    }
}
