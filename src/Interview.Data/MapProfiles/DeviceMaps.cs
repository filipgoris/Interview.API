using ABB.Interview.Data.Dto;
using ABB.Interview.Domain;
using AutoMapper;

namespace ABB.Interview.Data.MapProfiles
{
    public class DeviceMaps : Profile
    {
        public DeviceMaps()
        {
            CreateMap<DeviceReadingDto, Device>();
        }
    }
}
