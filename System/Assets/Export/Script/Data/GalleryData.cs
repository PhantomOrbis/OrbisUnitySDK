using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GalleryData", menuName = "Metaverse/GalleryData", order = int.MaxValue)]

public class GalleryData : ScriptableObject
{
    [Serializable]
    public class GalleryWriter
    {
        public string writerCategory;
        public string writerName;
        public string writerLink;
        public string writerExplanation;
    }

    [Header("[ Key ]")]
    public string galleryID;
    public string artID;

    [Space(5)]
    [Header("[ Value ]")]    
    public string artTitle;
    public string artCategory;
    public string artDeepLink;
    public string artOutLink;

    public List<GalleryWriter> artWriter;

    [TextArea(3, 10)]
    public string artDate;
}
