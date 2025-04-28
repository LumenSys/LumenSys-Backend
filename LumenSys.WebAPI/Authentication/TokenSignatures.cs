namespace LumenSys.Objects.Contracts
{
    public class TokenSignatures
    {
        public string Issuer { get; } = "LumenSys API";
        public string Audience { get; } = "LumenSys API Website";
        public string Key { get; } = "LumenSys_Barrament_api";
    }
}