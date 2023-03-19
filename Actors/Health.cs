using Godot;
using System;

public partial class Health : Node2D
{
    [Export]
    private int _health = 100;

    public int HealthValue
    {
        get => _health;
        set 
        {
            _health = Math.Clamp(value, 0, 100);
        } 
    }
}
