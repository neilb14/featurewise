using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileHelpers;
using GF.FeatureWise.Services.Models;

namespace GF.FeatureWise.Services.Services
{
    public class UserEventCsvParser : IParseUserEvents
    {
        private readonly FileHelperEngine<UserEventCsvRecord> engine = new FileHelperEngine<UserEventCsvRecord>();

        public UserEventCsvParser()
        {
            engine.Options.IgnoreFirstLines = 1;
        }

        public IEnumerable<UserEvent> FromString(string contents)
        {
            return CreateUserEvent(engine.ReadString(contents));
        }

        public IEnumerable<UserEvent> FromStream(Stream stream)
        {
            using(var reader = new StreamReader(stream))
            {
                return CreateUserEvent(engine.ReadStream(reader));
            }
        }

        private static IEnumerable<UserEvent> CreateUserEvent(IEnumerable<UserEventCsvRecord> records)
        {
            return records.Select(r => r.CreateUserEvent());
        }
    }

    [DelimitedRecord(",")]
    public class UserEventCsvRecord
    {
        public string Feature;
        public string Type;
        [FieldConverter(ConverterKind.Date, "yyyy-MM-ddTHH:mm:ss")]
        public DateTime At;

        public UserEvent CreateUserEvent()
        {
            return new UserEvent
            {
                Id = Guid.NewGuid(),
                Feature = Feature,
                Type = Type,
                At = At
            };
        }
    }
}