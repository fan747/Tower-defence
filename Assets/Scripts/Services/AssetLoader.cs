using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Services
{
    public class AssetLoader
    {
        private string _assetPath;
        private AsyncOperationHandle _operationHandle;

        public AssetLoader(string assetPath)
        {
            _assetPath = assetPath;
        }

        public async Task<T> Load<T>() => await Load<T>(_assetPath);
        public void Unload() => Addressables.Release(_operationHandle);

        private async Task<T> Load<T>(string assetId)
        {
            _operationHandle = Addressables.LoadAssetAsync<T>(assetId);
            await _operationHandle.Task;

            if (_operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                return (T)_operationHandle.Result;
            }

            Debug.LogError($"Failed to load asset with ID: {assetId}");
            return default;
        }

    }
}
