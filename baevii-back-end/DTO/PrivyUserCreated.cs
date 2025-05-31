namespace baevii_back_end.DTO;

public class PrivyUserCreated
{
    public string type { get; set; }
    public User user { get; set; }

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
    }
}
