using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractiveMode
{
    None,
    Animation,
    Telescope,
    Gallery,
    Npc,
    Riding,
    Market
}

public enum InteractiveTelescope
{
    None,
    Clip,
    Url
}

public enum InteractiveType
{
    None,
    Label,
    Url
}

public class InteractiveCloud : MonoBehaviour
{
    public InteractiveMode interactive = InteractiveMode.None;

    // ==================================================
    // [ Key ]
    // ==================================================
    [Header("[ Key ]")]
    public string interactiveID;

    // ==================================================
    // [ Value ]
    // ==================================================
    [Space(5)]
    [Header("[ Value ]")]
    public InteractiveType targetType = InteractiveType.None;
    public string targetLabel;
    public Vector3 targetPosition;
    public Vector3 targetRotation;
    
}
