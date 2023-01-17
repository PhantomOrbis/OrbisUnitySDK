using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RidingData", menuName = "Metaverse/RidingData", order = int.MaxValue)]
public class RidingData : ScriptableObject
{
    [Header("[ Key ]")]
    public string ridingID;

    [Space(5)]
    [Header("[ Value ]")]
    public string ridingLable;    

    [Space(5)]
    [Header("[ Data - animation ]")]
    public string ridingIdleSoundLable;
    public string ridingWalkSoundLable;
    public string ridingRunSoundLable;

    [Space(10)]
    [Header("[ Data - riding ]")]
    public Vector3 ridingPosition;
    public Vector3 ridingRotation;

    [Space(3)]
    [Header("[ Data - character ]")]
    public Vector3 characterPosition;
    public Vector3 characterRotation;
}
