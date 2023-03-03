using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lambdaApi.Domain.Entities.Orders
{
    public class ProfileOrders
    {
        public string UserName { get; set; }

        public List<AddressOrders> Addresses { get; set; }   
    }
}
