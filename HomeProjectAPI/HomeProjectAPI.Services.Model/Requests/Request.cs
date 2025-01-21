using System;

namespace HomeProjectAPI.Services.Model.Requests
{
    public class Request<T>
    {
        public DateTime Date { get; set; }

        public string CorrelationId { get; set; }

        public T Payload { get; set; }
    }
}