using System;

namespace GF.FeatureWise.Services.Models
{
    public interface ITrackUserEvents
    {
        void RegisterTick();
        void RegisterStart(DateTime at);
        void RegisterStop(DateTime at);
    }
}
