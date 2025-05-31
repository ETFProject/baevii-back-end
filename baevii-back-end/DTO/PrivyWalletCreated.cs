namespace baevii_back_end.DTO;

public class PrivyWalletCreated
{
    public string type { get; set; }
    public User user { get; set; }
    public Wallet wallet { get; set; }

    public class User
    {
        public int created_at { get; set; }
        public bool has_accepted_terms { get; set; }
        public string id { get; set; }
        public bool is_guest { get; set; }
        public Linked_Accounts[] linked_accounts { get; set; }
        public object[] mfa_methods { get; set; }
    }

    public class Linked_Accounts
    {
        public string address { get; set; }
        public int first_verified_at { get; set; }
        public int latest_verified_at { get; set; }
        public string type { get; set; }
        public int verified_at { get; set; }
        public string chain_id { get; set; }
        public string chain_type { get; set; }
        public string connector_type { get; set; }
        public bool delegated { get; set; }
        public object id { get; set; }
        public bool imported { get; set; }
        public string recovery_method { get; set; }
        public string wallet_client { get; set; }
        public string wallet_client_type { get; set; }
        public int wallet_index { get; set; }
    }

    public class Wallet
    {
        public string address { get; set; }
        public string chain_type { get; set; }
        public string type { get; set; }
    }
}