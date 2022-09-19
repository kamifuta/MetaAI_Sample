using Game.Enemies;
using Game.Players;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameObject playerObj;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerController>();

        builder.Register<IPlayerInput, PlayerKyeInput>(Lifetime.Singleton);

        builder.RegisterComponent(playerObj);
    }
}
