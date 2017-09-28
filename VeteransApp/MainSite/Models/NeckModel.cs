
namespace MainSite.Models
{
    public class NeckModel : IBaseModel
    {
        public string VarianceHistory { get; set; }
        public string VarianceHistoryWriteIn { get; set; }
        public string VarianceFlareUps { get; set; }
        public string VarianceFlareUpsWriteIn { get; set; }
        public string VarianceFunctionLoss { get; set; }
        public string VarianceFunctionLossWriteIn { get; set; }

        public bool IsFormReadonly { get; set; }

        public long ContentID { get; set; }
        public long ContentTypeID { get; set; }
        public long ContentStateID { get; set; }

        public string TemplatePath { get; set; }
        public long UserID { get; set; }

        public string NameOfPatient { get; set; }
        public string SocialSecurity { get; set; }


        public bool S45 { get; set; }
        public bool S46 { get; set; }
        public bool S47 { get; set; }
        public bool S48 { get; set; }
        public bool S49 { get; set; }
        public bool S50 { get; set; }
        public bool S51 { get; set; }
        public bool S52 { get; set; }
        public bool S53 { get; set; }
        public bool S7 { get; set; }
        public bool S6 { get; set; }
        public bool S1 { get; set; }
        public string S54 { get; set; }

        public string S58 { get; set; }

        public string SDominantHand { get; set; }

        public string S96 { get; set; }
        public string S86 { get; set; }
        public string S87 { get; set; }
        public string S90 { get; set; }
        public string S78 { get; set; }
        public string S82 { get; set; }

        public string S109 { get; set; }
        public string S108 { get; set; }
        public string S110 { get; set; }
        public string S111 { get; set; }
        public string S117 { get; set; }
        public string S118 { get; set; }


        public bool S135Tenderness { get; set; }

        public bool S145MuscleSpasmQuestion { get; set; }
        public bool S145GuardingQuestion { get; set; }
        public bool S145MuscleSpasm { get; set; }
        public bool S145Guarding { get; set; }

        public bool S159 { get; set; }
        public bool S164 { get; set; }
        public bool S158 { get; set; }
        public bool S157 { get; set; }
        public bool S171 { get; set; }
        public bool S170 { get; set; }
        public bool S169 { get; set; }
        public bool S168 { get; set; }
        public bool S167 { get; set; }
        public bool S160 { get; set; }
        public bool S163 { get; set; }
        public bool S161 { get; set; }
        public bool S162 { get; set; }

        public string S184 { get; set; }
        public string S185 { get; set; }
        public string S174 { get; set; }
        public string S180 { get; set; }
        public string S175 { get; set; }
        public string S177 { get; set; }

        public bool S316 { get; set; }

        public string SRadiculopathyLegSide { get; set; }
        public string SRadiculopathyConstantPainLevel { get; set; }
        public string SRadiculopathyIntermittentPainLevel { get; set; }
        public string SRadiculopathyDullPainLevel { get; set; }
        public string SRadiculopathyTinglingPainLevel { get; set; }
        public string SRadiculopathyNumbnessPainLevel { get; set; }

        public bool SRadiculopathyLegSideAnswer { get; set; }
        public bool SRadiculopathyConstantPainLevelAnswer { get; set; }
        public bool SRadiculopathyIntermittentPainLevelAnswer { get; set; }
        public bool SRadiculopathyDullPainLevelAnswer { get; set; }
        public bool SRadiculopathyTinglingPainLevelAnswer { get; set; }
        public bool SRadiculopathyNumbnessPainLevelAnswer { get; set; }

        public string SRadiculopathySeverityLevel { get; set; }
        public string S14C { get; set; }

        public bool S414 { get; set; }

        public bool S416 { get; set; }
        public bool S428 { get; set; }
        public bool S417 { get; set; }
        public bool S421 { get; set; }

        public string S416Choice { get; set; }
        public string S428Choice { get; set; }
        public string S417Choice { get; set; }
        public string S421Choice { get; set; }

        public string S12AChoice { get; set; }
        public string S2BChoice { get; set; }

        public string S12AChoiceLeftArm { get; set; }
        public string S12AChoiceRightArm { get; set; }

        // right arm
        public string S12AChoice189 = "5";
        public string S12AChoice190 = "5";
        public string S12AChoice191 = "5";
        public string S12AChoice192 = "5";
        public string S12AChoice193 = "5";
        public string S12AChoice198 = "5";
        public string S12AChoice197 = "5";
        public string S12AChoice196 = "5";

        public string S12AChoice195 = "4";
        public string S12AChoice194 = "4";

        public string S12AChoice327 = "2";
        public string S12AChoice322 = "2";
        public string S12AChoice326 = "1";



        // left arm
        public string S12AChoice218 = "5";
        public string S12AChoice219 = "5";
        public string S12AChoice220 = "5";
        public string S12AChoice221 = "5";
        public string S12AChoice222 = "5";
        public string S12AChoice227 = "5";
        public string S12AChoice226 = "5";
        public string S12AChoice225 = "5";

        public string S12AChoice224 = "4";
        public string S12AChoice223 = "4";

        public string S12AChoice323 = "2";
        public string S12AChoice324 = "2";
        public string S12AChoice325 = "1";

    }
}