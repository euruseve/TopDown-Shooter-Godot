using Godot;
using System;

public partial class Main : Node2D
{
    private BulletManager _bulletManager;
    private Player _player;

    public override void _Ready()
    {
        base._Ready();

        _bulletManager = GetNode<BulletManager>("BulletManager");
        _player = GetNode<Player>("Player");

        _player.PlayerFiredBullet += _bulletManager.HandleBulletSpawned;

    }

}
