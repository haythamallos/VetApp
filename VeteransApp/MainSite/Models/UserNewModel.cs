

namespace MainSite.Models
{
    public class UserNewModel
    {
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Note { get; set; }
        public string ErrorMessage { get; set; }
        public bool Exists { get; set; }
        public int CurrentRating { get; set; }
    }
}