using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceGenerator.Interfaces
{
    public interface IRemoteServiceCall
    {
        List<ServiceCall> GetServiceCalls();
    }
}
