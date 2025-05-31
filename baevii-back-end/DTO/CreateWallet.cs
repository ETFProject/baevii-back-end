using System.Text.Json.Serialization;

namespace baevii_back_end.DTO;

public class CreateWallet
{
    [JsonIgnore]
    public const string RouteTemplate = "wallets";

    [JsonPropertyName("chain_type")]
    public string? ChainType { get; set; }

    public class Response
    {
        public string id { get; set; }
        public string address { get; set; }
        public string chain_type { get; set; }
        public object[] policy_ids { get; set; }
        public object[] additional_signers { get; set; }
        public object exported_at { get; set; }
        public long created_at { get; set; }
        public object owner_id { get; set; }
    }
}