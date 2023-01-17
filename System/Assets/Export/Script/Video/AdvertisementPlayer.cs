using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Video;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdvertisementPlayer : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer player;
    [SerializeField]
    private string label = "";

    void Start()
    {
        Process();
    }
    
    private void Process()
    {
        var handle = Addressables.LoadAssetAsync<VideoClip>(label);
        handle.WaitForCompletion();

        if(handle.Status == AsyncOperationStatus.Succeeded )
        {
            player.clip = handle.Result;
            player.Play();
        }        
    }
}
