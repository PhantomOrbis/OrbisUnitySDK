using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Metaverse/MapData", order = int.MaxValue)]
public class MapData : ScriptableObject
{
    [Header("[ Key ]")]
    public string mapID;

    [Space(5)]
    [Header("[ Value ]")]    
    public string mapImageLabel;

    [Space(5)]
    [Header("[ Information ]")]
    public string mapName;

    [Space(5)]
    [Header("[Character]")]
    public Vector3 characterPoint;

    [Space(5)]
    [Header("[Camera]")]
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
    public float cameraSize;

    [Space(5)]
    [Header("[Map]")]
    public Vector3 mapAnchoredPosition;
    public Vector2 mapSizeDelta;
}
