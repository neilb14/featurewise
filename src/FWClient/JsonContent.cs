using System;

namespace GF.FeatureWise.Client
{
    public class JsonContent
    {
        public static string Build(string project, string name, string type, DateTime at)
        {
            return string.Format("{{\"project\":\"{0}\",\"name\":\"{1}\",\"type\":\"{2}\",\"at\":\"{3}\"}}", project,name, type, at.ToString("yyyyMMddHHmmss"));
        }
    }
}