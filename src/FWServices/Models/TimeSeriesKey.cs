namespace GF.FeatureWise.Services.Models
{
    public class TimeSeriesKey
    {
        private readonly int day;
        private readonly string feature;
        private readonly int month;
        private readonly int year;

        public TimeSeriesKey(int year, int month, int day, string feature)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.feature = feature;
        }

        protected bool Equals(TimeSeriesKey other)
        {
            return year == other.year && month == other.month && day == other.day &&
                   string.Equals(feature, other.feature);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TimeSeriesKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = year;
                hashCode = (hashCode*397) ^ month;
                hashCode = (hashCode*397) ^ day;
                hashCode = (hashCode*397) ^ feature.GetHashCode();
                return hashCode;
            }
        }
    }
}