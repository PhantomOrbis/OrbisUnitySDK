using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldData", menuName = "Metaverse/WorldData", order = int.MaxValue)]
public class WorldData : ScriptableObject
{
    [Header("[ Skybox ]")]
    public bool useSkybox = false;
    public string skyboxLabel = "";

    
    [Header("[ Start Position ]")]
    [Space(10)]
    public Vector3 position;
    public Vector3 rotation;
}
