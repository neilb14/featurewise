using System;

namespace GF.FeatureWise.Services.Models
{
    public class Feature
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Notes { get; set; }
    }
}