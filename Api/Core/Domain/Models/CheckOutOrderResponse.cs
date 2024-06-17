using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Domain.Models
{
    public class CheckOutOrderResponse
    {
        public string ? SessionId { get; set; }
        public string ? PubKey { get; set; }
    }
}