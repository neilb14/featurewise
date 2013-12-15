using System;
using System.Collections.Generic;
using System.Linq;

namespace GF.FeatureWise.Services.Models
{    
    public class GenerateFeatureDecorator<T> : IGenerate<T>
    {
        private readonly IGenerate<T> realGenerator;
        private readonly IFeatureRepository repository;

        public GenerateFeatureDecorator(IGenerate<T> realGenerator, IFeatureRepository repository)
        {
            this.realGenerator = realGenerator;
            this.repository = repository;
        }

        public IEnumerable<T> Generate(IEnumerable<UserEvent> userEvents)
        {
            var enumerable = userEvents as UserEvent[] ?? userEvents.ToArray();
            foreach (var userEvent in enumerable.ToArray().Where(userEvent => !repository.Exists(userEvent.Feature)))
            {
                repository.Add(new Feature{Id = Guid.NewGuid(), Name = userEvent.Feature});
            }
            return realGenerator.Generate(enumerable);
        }
    }

}
