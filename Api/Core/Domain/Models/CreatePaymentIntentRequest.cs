using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Domain.Models
{
    public class CreatePaymentIntentRequest
    {
        public int OrderId { get; set; }
            public string Currency { get; set; }
    }
}