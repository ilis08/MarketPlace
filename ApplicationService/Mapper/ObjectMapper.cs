using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>

        {

            var config = new MapperConfiguration(cfg =>

            {

                // This line ensures that internal properties are also mapped over.

                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                cfg.AddProfile<AutoMappings>();

            });

            var mapper = config.CreateMapper();

            return mapper;

        });

        public static IMapper Mapper => Lazy.Value;
    }
}
