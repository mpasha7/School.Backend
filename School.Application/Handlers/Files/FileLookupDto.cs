using AutoMapper;
using School.Application.Common.Mappings;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Files
{
    public class FileLookupDto : IMapWith<FileObject>
    {
        public string? UniqueFileName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FileObject, FileLookupDto>()
                .ForMember(dto => dto.UniqueFileName, opt => opt.MapFrom(f => f.UniqueFileName));
        }
    }
}
