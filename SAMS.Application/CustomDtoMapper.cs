using AutoMapper;
using SAMS.Users;
using SAMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS
{
    internal static class CustomDtoMapper
    {
        private static volatile bool _mappedBefore;
        private static readonly object SyncObj = new object();
        public static void CreateMappings()
        {
            lock(SyncObj)
            {
                if(_mappedBefore)
                {
                    return;
                }

                _mappedBefore = true;
            }
        }
        private static void CreateMappingsInternal()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore()));
         

        }
    }
}
