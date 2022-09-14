using Game.Players;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private PlayerShooter playerShooter;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerController>();
        builder.Register<IPlayerInput, PlayerKyeInput>(Lifetime.Singleton);

        builder.RegisterComponent<IPlayerMover>(playerMover);
        builder.RegisterComponent<IPlayerShooter>(playerShooter);
    }
}
