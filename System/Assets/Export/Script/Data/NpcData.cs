using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Metaverse/NpcData", order = int.MaxValue)]
public class NpcData : ScriptableObject
{
    [Header("[ Key ]")]
    public string npcID;

    [Space(5)]
    [Header("[ Value ]")]
    public string npcBackgroundLable;
    public string npcIconLable;
    public string npcTTSLable;

    [Space(2)]    
    public string npcName;
    [TextArea(3, 5)]
    public string npcTitle;
    [TextArea(5, 10)]    
    public string npcContent;
}
