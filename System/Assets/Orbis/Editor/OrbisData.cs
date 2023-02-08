using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Orbis
{

    public class OrbisData : ScriptableObject
    {
        public List<OrbisAccount> account = new List<OrbisAccount>();

        private void OnValidate()
        {
            EditorUtility.SetDirty(this);            
        }
    }

}