using System.Collections.Generic;
using UnityEngine;

namespace Orbis
{

    public class OrbisData : ScriptableObject
    {        
        public List<OrbisAccount> accounts = new List<OrbisAccount>() 
        { 
            new OrbisAccount() 
            {
                manager = "root",
                id = "phantom",
                password = "!ch1365159"
            } 
        };
    }

}