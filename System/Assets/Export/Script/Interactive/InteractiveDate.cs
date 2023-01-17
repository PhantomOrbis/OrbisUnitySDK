using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDate : MonoBehaviour
{
    public InteractiveMode interactive = InteractiveMode.None;
    public string interactiveID;

    // ==================================================
    // [ Mode = Animation ]
    // ==================================================
    [Header("[ Animation ]")]
    public Vector3 animationPosition;
    public Vector3 animationRotation;
    public string animationLabel;

    // ==================================================
    // [ Mode = Telescope ]        
    // ==================================================
    [Space(2)]
    [Header("[ Telescope ]")]
    public InteractiveTelescope telescope;
    public string telescopeLabel;

    // ==================================================
    // [ Mode = Gallery ]        
    // ==================================================
    [Space(2)]
    [Header("[ Gallery ]")]
    public string galleryLabel;

    // ==================================================
    // [ Mode = NPC ]        
    // ==================================================       
    [Space(2)]
    [Header("[ Npc ]")]
    public string npcLabel;    
}
