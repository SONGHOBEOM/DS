using PlayerCORE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class PatchManager : Singleton<PatchManager>
{
    private Queue<IEnumerator> patchFuncs = new Queue<IEnumerator>();
    private Dictionary<string, GameObject> loadedObjects = new Dictionary<string, GameObject>();

    private float totalAssetCount = 0;
    private float loadedAssetCount = 0;

    private PatchProgress progress;

    public void Load()
    {
        EnqueueLoadAsyncFunc();
        StartCoroutine(PatchProcess());
    }


    private void EnqueueLoadAsyncFunc()
    {
        patchFuncs.Enqueue(InstantiateAsyncPatchProgressAsset());
        patchFuncs.Enqueue(LoadDataContainers());
        patchFuncs.Enqueue(LoadAssets());
        patchFuncs.Enqueue(InstantiateAsyncEnvironmentAsset());
        patchFuncs.Enqueue(InstantiateAsyncPlayerAsset());
        patchFuncs.Enqueue(InstantiateAsyncCameraAsset());
        patchFuncs.Enqueue(InstantiateAsyncUserInterfaceAsset());
        patchFuncs.Enqueue(InstantiateQuestListItem());
    }

    private IEnumerator PatchProcess()
    {
        while(patchFuncs.Count > 0)
        {
            var func = patchFuncs.Dequeue();
            yield return func;
        }
    }

    private IEnumerator LoadDataContainers()
    {
        var isFinished = false;
        var labelString = "DataContainer";
        AssetLabelReference label = new();
        label.labelString = labelString;

        Addressables.LoadResourceLocationsAsync(label.labelString).Completed += (loaded) =>
        {
            IList<IResourceLocation> _locations;
            _locations = loaded.Result;
           Addressables.LoadAssetsAsync<ScriptableObject>(_locations, null).Completed += (loadedData) =>
            {
                CreateData(loadedData.Result);
                isFinished = true;
            };

            void CreateData(IList<ScriptableObject> dataList)
            {
                var _dataList = dataList;
                foreach (var data in dataList)
                {
                    DataManager.Instance.entireDataContainer.Push(data);
                };
                DataManager.Instance.CreateDatas();
            }
        };

        yield return new WaitUntil(() => isFinished == true);
    }

    private IEnumerator InstantiateAsyncPatchProgressAsset()
    {
        var isFinished = false;
        Addressables.InstantiateAsync("PatchProgress").Completed += (loadedObj) =>
        {
            progress = loadedObj.Result.GetComponent<PatchProgress>();
            isFinished = true;
        };

        yield return new WaitUntil(() => isFinished == true);
    }
    private IEnumerator LoadAssets()
    {
        var patchDatas = DataManager.Instance.entireDataContainer.patchDataContainer.Get();
        totalAssetCount = patchDatas.Count;
        var isfinished = false;
        var waitAsync = new WaitUntil(() => isfinished == true);
        foreach (var patchData in patchDatas)
        {
            isfinished = false;
            Addressables.LoadAssetAsync<GameObject>(patchData.assetName).Completed += (loadedObj) =>
            {
                Init(loadedObj);
                Debug.Log($"Load{patchData.assetName}Asset");
                isfinished = true;
            };

            yield return waitAsync;

            void Init(AsyncOperationHandle<GameObject> loadedObj)
            {
                IsEquipment(loadedObj);
                loadedObjects.Add(patchData.assetName, loadedObj.Result);
                loadedAssetCount++;
                progress.UpdateProgress(loadedAssetCount, totalAssetCount);
            }

            void IsEquipment(AsyncOperationHandle<GameObject> loadedObj)
            {
                if (loadedObj.Result.GetComponent<ItemBase>() != null)
                    DataManager.Instance.entireDataContainer.itemContainer.Regist(loadedObj.Result.GetComponent<ItemBase>());
            }
        }
        ItemHelper.CreateEquipment();
        progress.UpdateProgress(loadedAssetCount, totalAssetCount);
    }

    private IEnumerator InstantiateAsyncPlayerAsset()
    {
        var isFinished = false;
        Addressables.InstantiateAsync("Player").Completed += (loadedObj) => Init(loadedObj);

        yield return new WaitUntil(() => isFinished == true);

        void Init(AsyncOperationHandle<GameObject> loadedObj)
        {
            var go = loadedObj.Result;

            NormalizationAssetName(loadedObj.Result);

            var player = go.GetComponent<Player>();

            EntityManager.Instance.SetPlayer(player);
            var animator = go.GetComponent<Animator>();
            PlayerAnimationManager.Instance.SetPlayerAnimator(animator);
            Debug.Log("InstantiatePlayerAsset");

            isFinished = true;
        }
    }

    private IEnumerator InstantiateAsyncEnvironmentAsset()
    {
        var isFinished = false;
        Addressables.InstantiateAsync("Environment", transform.position, Quaternion.identity).Completed += (loadedObj) => Init(loadedObj);

        yield return new WaitUntil(() => isFinished == true);

        void Init(AsyncOperationHandle<GameObject> loadedObj)
        { 
            NormalizationAssetName(loadedObj.Result);

            var spawners = loadedObj.Result.GetComponentsInChildren<Spawner>();
            var surface = loadedObj.Result.GetComponent<NavMeshSurface>();
            
            if (surface == null)
            {
                surface = loadedObj.Result.AddComponent<NavMeshSurface>();
                surface.layerMask = LayerMask.GetMask("Wall","Ground");
            }
            surface.BuildNavMesh();
            Debug.Log("InstantiateEnvironmentAsset");

            isFinished = true;
        }
    }

    private IEnumerator InstantiateAsyncCameraAsset()
    {
        var isFinished = false;
        Addressables.InstantiateAsync("Camera", transform.position, Quaternion.identity).Completed += (loadedObj) => Init(loadedObj);

        yield return new WaitUntil(() => isFinished == true);

        void Init(AsyncOperationHandle<GameObject> loadedObj)
        {
            var cameraController = loadedObj.Result.GetComponent<CameraMoveController>();
            cameraController.targetObject = EntityManager.Instance.player.GetCameraTarget();
            EntityManager.Instance.mainCamera = cameraController.mainCamera;
            EntityManager.Instance.player.GetComponent<PlayerStateController>().SetMainCamera(cameraController.mainCamera);

            cameraController.UpdateRotate();
            NormalizationAssetName(loadedObj.Result);
            Debug.Log("InstantiateCameraAsset");
            isFinished = true;
        }
    }

    private IEnumerator InstantiateAsyncUserInterfaceAsset()
    {
        var isFinished = false;
        Addressables.InstantiateAsync("UserInterface").Completed += (loadedObj) =>
        {
            NormalizationAssetName(loadedObj.Result);
            isFinished = true;
        };

        yield return new WaitUntil(() => isFinished == true);
    }


    private IEnumerator InstantiateQuestListItem()
    {
        QuestManager.updateQuestListItem?.Invoke(EntityManager.Instance.player.questLevel);
        yield return null;
    }

    public GameObject Get(string name)
    {
        if(loadedObjects.TryGetValue(name, out var obj)) return obj;
        return default;
    }
    
    private void NormalizationAssetName(GameObject go)
    {
        var index = go.name.IndexOf("(Clone)");
        if (index == -1) return;
        if (index > 0)
            go.name = go.name.Substring(0, index);
    }
}
