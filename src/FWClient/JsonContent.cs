using System;

namespace GF.FeatureWise.Client
{
    public class JsonContent
    {
        public static string Build(Guid id, string feature, string type, DateTime at)
        {
            return string.Format("{{\"Id\":\"{0}\",\"Feature\":\"{1}\",\"Type\":\"{2}\",\"At\":\"{3}\"}}", id,feature, type, at.ToString("yyyyMMddHHmmss"));
        }
    }
}