using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MarketData", menuName = "Metaverse/MarketData", order = int.MaxValue)]
public class MarketData : ScriptableObject
{
    [Header("[ Key ]")]
    public string marketID;

    [Space(5)]
    [Header("[ Value ]")]
    public string marketCharacter;
    public string marketName;
    public string marketPrice;
    public string marketDiscountPercentage;
    public string marketDiscountPrice;
    public string marketDelivery;
    public string marketUrl;
    public string[] marketThumbnailLabel;
    public string[] marketDetailLabel;
}
