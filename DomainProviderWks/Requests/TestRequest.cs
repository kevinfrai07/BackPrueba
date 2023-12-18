using MediatR;
using ProviderWks.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderWks.Domain.Requests
{
    public class TestRequest : IRequest<TestResponse>
    {
        public string Value { get; set; }
    }
}
