using MediatR;
using ProviderWks.Domain.Responses;

namespace ProviderWks.Domain.Requests
{
    public class TestRequest : IRequest<TestResponse>
    {
        public string Value { get; set; }
    }
}