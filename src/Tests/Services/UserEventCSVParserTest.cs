using System.IO;
using System.Linq;
using System.Text;
using GF.FeatureWise.Services.Services;
using Xunit;

namespace Tests.Services
{
    public class UserEventCsvParserTest
    {
        private readonly UserEventCsvParser parser;
        private readonly StringBuilder sb;

        public UserEventCsvParserTest()
        {
            parser = new UserEventCsvParser();
            sb = new StringBuilder();
            sb.AppendLine(@"feature,type,at");
            sb.AppendLine(@"first,tick,2013-08-03T10:00:00");
            sb.AppendLine(@"first,tick,2013-08-03T10:01:00");
            sb.AppendLine(@"second,tick,2013-08-03T10:02:00");
        }

        [Fact]
        public void ShouldParseFromString()
        {
            var results = parser.FromString(sb.ToString());
            Assert.Equal(3, results.Count());
        }

        [Fact]
        public void ShouldParseFromStream()
        {
            using(var stream = new MemoryStream(Encoding.ASCII.GetBytes(sb.ToString())))
            {
                var results = parser.FromStream(stream);
                Assert.Equal(3, results.Count());
            }
        }
    }
}
    