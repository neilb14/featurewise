using System;

namespace GF.FeatureWise.Client
{
    public class JsonContent
    {
        public static string BuildEvent(Guid id, string feature, string type, DateTime at)
        {
            return string.Format("{{\"Id\":\"{0}\",\"Feature\":\"{1}\",\"Type\":\"{2}\",\"At\":\"{3}\"}}", id,feature, type, at.ToString("o"));
        }

        public static string BuildScope(Guid id, string feature, string type, DateTime start, DateTime stop)
        {
            return string.Format("{{\"Id\":\"{0}\",\"Feature\":\"{1}\",\"Type\":\"{2}\",\"Start\":\"{3}\",\"Stop\":\"{4}\"}}", id, feature, type, start.ToString("o"), stop.ToString("o"));
        }
    }
}