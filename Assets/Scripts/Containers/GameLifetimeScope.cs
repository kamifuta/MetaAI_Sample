using Game.Enemies;
using Game.Players;
using Game.Players.Presenters;
using Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private PlayerHealthView playerHealthView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerController>();
        builder.RegisterEntryPoint<PlayerHealthPresenter>();

        builder.Register<IPlayerInput, PlayerKyeInput>(Lifetime.Singleton);

        builder.RegisterComponent(playerObj);
        builder.RegisterComponent(playerHealthView);
    }
}
