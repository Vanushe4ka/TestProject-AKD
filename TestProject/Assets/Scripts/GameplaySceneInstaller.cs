using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] bool isMobileDevice;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private MobileInput _mobileInputPrefab;
    [SerializeField] private DesktopPickupMessage _desktopPickupMessage;
    [SerializeField] private MobilePickupMessage _mobilePickupMessage;
    
    public override void InstallBindings()
    {
        if (isMobileDevice)
        {
            MobileInput mobileInput = Container.InstantiatePrefabForComponent<MobileInput>(
                _mobileInputPrefab.gameObject,
                Vector3.zero,
                Quaternion.identity,
                null);

            Container.Bind<IInput>().FromInstance(mobileInput).AsSingle();

            Container.BindInstance(mobileInput).AsSingle().NonLazy();

            MobilePickupMessage mobilePickupMessage = Container.InstantiatePrefabForComponent<MobilePickupMessage>(_mobilePickupMessage, Vector3.zero, Quaternion.identity, null);
            Container.Bind<IPickupMessage>().FromInstance(mobilePickupMessage).AsSingle();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;Container.BindInterfacesTo<DesctopInput>().AsSingle();
            DesktopPickupMessage desktopPickupMessage = Container.InstantiatePrefabForComponent<DesktopPickupMessage>(_desktopPickupMessage, Vector3.zero, Quaternion.identity, null);
            Container.Bind<IPickupMessage>().FromInstance(desktopPickupMessage).AsSingle();
        }
        Player player = Container.InstantiatePrefabForComponent<Player>(
            _playerPrefab.gameObject,
            _spawnPoint.position,
            _spawnPoint.rotation,
            null);
        Container.BindInstance(player).AsSingle().NonLazy();
        MovementHandler movementHandler = player.GetComponent<MovementHandler>();
        Container.BindInstance(movementHandler).AsSingle().NonLazy();
    }
}
