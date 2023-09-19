using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAdminMVC.Services;

public interface IResponseFormatter
{
    string Format();
}

public class TimeResponseFormatter : IResponseFormatter
{
    private IWeatherEndpoint endpoint;

    public TimeResponseFormatter(IWeatherEndpoint endpoint)
    {
        this.endpoint = endpoint;
    }

    public string Format()
    {
        return endpoint.TimeStamp.ToString();
    }
}
