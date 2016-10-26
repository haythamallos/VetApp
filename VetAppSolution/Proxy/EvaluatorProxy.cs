

namespace Vetapp.Client.Proxy
{
    public class EvaluatorProxy
    {
        public long EvaluatorID { get; set; }
        public bool IsFirstTimeFiling { get; set; }
        public bool HasFiled { get; set; }
        public bool HasActiveAppeal { get; set; }
        public bool HasRating { get; set; }
        public int CurrentRating { get; set; }
    }
}
