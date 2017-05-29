
namespace MainSite.Models
{
    public interface IBaseModel
    {
        long ContentID { get; set; }
        long ContentTypeID { get; set; }
        long ContentStateID { get; set; }

        string TemplatePath { get; set; }
        long UserID { get; set; }

        string NameOfPatient { get; set; }
        string SocialSecurity { get; set; }

        string VarianceHistory { get; set; }
        string VarianceHistoryWriteIn { get; set; }
        string VarianceFlareUps { get; set; }
        string VarianceFlareUpsWriteIn { get; set; }
        string VarianceFunctionLoss { get; set; }
        string VarianceFunctionLossWriteIn { get; set; }

    }

    //public class BaseModel : IBaseModel
    //{
    //    public long ContentID { get; set; }
    //    public long ContentTypeID { get; set; }
    //    public long ContentStateID { get; set; }

    //    public string TemplatePath { get; set; }
    //    public long UserID { get; set; }

    //    public string NameOfPatient { get; set; }
    //    public string SocialSecurity { get; set; }
    //}
}