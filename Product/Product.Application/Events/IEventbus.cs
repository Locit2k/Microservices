using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Events
{
    public interface IEventbus
    {
        void Publish(string routingKey, object @event);
    }
}
