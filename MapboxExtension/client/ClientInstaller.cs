using UnityEngine;
using Zenject;

public class ClientInstaller : MonoInstaller<ClientInstaller>
{
  [SerializeField] private ZoomMap.Settings zoomMapSettings;
  [SerializeField] GlobeMap.Settings globeMapSettings;

  [SerializeField] private GlobalPrefab.Settings globalSettings;
  [SerializeField] private GlobalData.Settings globalDataSettings;

  public override void InstallBindings()
  {
    Container.BindInstances(zoomMapSettings, globeMapSettings, globalSettings, globalDataSettings);


    Container.BindInterfacesTo<ZoomMap>().AsSingle();
    Container.BindInterfacesTo<GlobeMap>().AsSingle();
    Container.BindInterfacesTo<GlobalPrefab>().AsSingle();
    Container.BindInterfacesTo<GlobalData>().AsSingle();

    Container.Bind<gRPCClient>().AsSingle();
    Container.Bind<MapConf>().AsSingle();
 
   
    InstallPrefabs();
    InstallDeclareSiganl();
  }

  void InstallPrefabs()
  {
    Container.Bind<DisposeDataFactory>().AsSingle().WithArguments(globalSettings.noteItem);
    Container.Bind<PutNZTTowerFactory>().AsSingle().WithArguments(globalSettings.nztPrefab);
    Container.Bind<PutZXTTowerFactory>().AsSingle().WithArguments(globalSettings.zxtPrefab);
  }

  void InstallDeclareSiganl()
  {
    Container.DeclareSignal<ZoomChangeDSignal>();
    Container.DeclareSignal<ToGlobalSignal>();
    Container.DeclareSignal<ToZoomSignal>();
  }
}