using CustomeModule.Interfaces.Services.Interface;
using Microsoft.Extensions.Options;
using System;
namespace CustomeModule.Services
{
    public class GlobalServices : IGlobalServices
    {
        private readonly IOptions<Model.Dto.TimeZone> _timeZone;

        public GlobalServices(IOptions<Model.Dto.TimeZone> timeZone)
        {
            _timeZone = timeZone;
        }
        public DateTime GetCurrentDateTimeInCurrentTimeZone()
        {
            //TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            var currenTimeZone = _timeZone.Value.CurrentTimeZone;
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(currenTimeZone);
            DateTime currentDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, timeZone);
            return currentDate;
        }

    }
}
