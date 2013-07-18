using System;
using System.Net;

namespace GF.FeatureWise.Client
{
    public class FeatureWiseResponse
    {        
        public FeatureWiseResponse(Guid id, Uri location, HttpStatusCode statusCode)
        {
            Id = id;
            Location = location;
            StatusCode = statusCode;
        }

        public Guid Id { get; private set; }
        public Uri Location { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
    }
}