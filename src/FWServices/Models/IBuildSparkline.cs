namespace GF.FeatureWise.Services.Models
{
    public interface IBuildSparkline
    {
        void Add(int value);
        string Build(int length);
    }
}
