using System;
using System.Collections.Generic;
using System.Linq;

namespace GF.FeatureWise.Services.Models
{
    public class SparklineBuilder : IBuildSparkline
    {
        private readonly IList<int> list = new List<int>();

        public void Add(int value)
        {
            list.Add(value);
        }

        public string Build(int length)
        {
            var skip = list.Count - length;
            if (skip < 0)
            {
                skip = 0;
            }
            return string.Join(",", list.Skip(skip).ToArray());
        }
    }
}