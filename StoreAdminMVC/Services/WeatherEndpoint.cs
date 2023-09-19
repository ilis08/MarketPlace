using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAdminMVC.Services;

public interface IWeatherEndpoint
{
    string TimeStamp { get; }
}

public class WeatherEndpoint : IWeatherEndpoint
{
    public string TimeStamp
    {
        get
        {
            return DateTime.Now.ToShortTimeString();
        }
    }
}
