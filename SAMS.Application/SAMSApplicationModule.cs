using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using SAMS.Users.Dto;
using SAMS.Users;

namespace SAMS
{
    [DependsOn(typeof(SAMSCoreModule), typeof(AbpAutoMapperModule))]
    public class SAMSApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
                mapper.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //Custom DTO auto-mappings
          //  CustomDtoMapper.CreateMappings();
        }
    }
}
