using System.Collections.Generic;

namespace MainSite.Classes
{
    public class ICDCodes
    {
        public static Dictionary<string, ICDCode> backICDCodes = new Dictionary<string, ICDCode>()
                {
                    { "mechanical back pain syndrome", new ICDCode {Diagnosis="Mechanical Back Pain Syndrome", RefNumber="724.2"}},
                    { "lumbosacral sprain/strain", new ICDCode {Diagnosis="Lumbosacral Sprain/Strain", RefNumber="847.2"}},
                    { "facet joint arthropathy", new ICDCode {Diagnosis="Facet joint arthropathy", RefNumber="721.3"}},
                    { "degenerative disc disease", new ICDCode {Diagnosis="Degenerative Disc Disease", RefNumber="722.52"}},
                    { "degenerative scoliosis", new ICDCode {Diagnosis="Degenerative Scoliosis", RefNumber="737.39"}},
                    { "foraminal/lateral recess/central stenosis", new ICDCode {Diagnosis="Foraminal/lateral recess/central stenosis", RefNumber="724.02"}},
                    { "degenerative spondylolisthesis", new ICDCode {Diagnosis="Degenerative spondylolisthesis", RefNumber="756.12"}},
                    { "spondylolysis/isthmic spondylolisthesis", new ICDCode {Diagnosis="Spondylolysis/isthmic spondylolisthesis", RefNumber="738.4"}},
                    { "intervertebral disc syndrome", new ICDCode {Diagnosis="Intervertebral disc syndrome", RefNumber="722.71"}},
                    { "radiculopathy", new ICDCode {Diagnosis="Radiculopathy", RefNumber="724.4"}},
                    { "ankylosis of thoracolumbar spine", new ICDCode {Diagnosis="Ankylosis of thoracolumbar spine", RefNumber="718.56"}},
                    { "ankylosing spondylitis of the thoracolumbar spine", new ICDCode {Diagnosis="Ankylosing spondylitis of the thoracolumbar spine", RefNumber="720.9"}},
                    { "vertebral fracture", new ICDCode {Diagnosis="Vertebral Fracture", RefNumber="805.01"}}
                };
    }

    public class ICDCode
    {
        public string Diagnosis { get; set; }
        public string RefNumber { get; set; }
    }
}