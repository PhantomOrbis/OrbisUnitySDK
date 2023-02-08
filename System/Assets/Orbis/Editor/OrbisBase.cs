using System;

namespace Orbis
{
    public enum OrbisAccountConnect
    {        
        None,
        Connect,
        Disconnect
    }

    public enum OrbisAccountRemember
    {
        None,        
        Remember        
    }

    public enum OrbisAccountSecurity
    {
        None,
        Manager,
        Master,
        Mimor
    }

    [Serializable]
    public class OrbisAccount
    {
        public OrbisAccountConnect connect;
        public OrbisAccountRemember remember;
        public OrbisAccountSecurity security;
        public string user;
        public string password;
        public string email;
    }

}