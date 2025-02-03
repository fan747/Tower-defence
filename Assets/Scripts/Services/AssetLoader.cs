using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Services
{
    public class AssetLoader
    {
        private string _assetPath;
        private AsyncOperationHandle _operationHandle;
        private bool _isLoading = false;
        private bool _isLoaded = false;

        public AssetLoader(string assetPath)
        {
            _assetPath = assetPath;
        }

        public async UniTask<T> Load<T>() => await Load<T>(_assetPath);

        public void Unload()
        {
            if (!_operationHandle.IsValid()) return;

            Addressables.Release(_operationHandle);
            _isLoaded = false;
        }

        private async UniTask<T> Load<T>(string assetId)
        {
            if (_isLoaded) return (T)_operationHandle.Result;


            if (!_isLoading)
            {
                _operationHandle = Addressables.LoadAssetAsync<T>(assetId);
                _isLoading = true;
            }

            await _operationHandle.ToUniTask();


            if (_operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                _isLoading = false;
                _isLoaded = true;
                return (T)_operationHandle.Result;
            }

            Debug.LogError($"Failed to load asset with ID: {assetId}");
            return default;
        }
    }
}
