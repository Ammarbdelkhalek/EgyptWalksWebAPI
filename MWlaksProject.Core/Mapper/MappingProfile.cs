using AutoMapper;
using MWlaksProject.Core.DTOS.DifficultiesDto;
using MWlaksProject.Core.DTOS.FavioriteWalk;
using MWlaksProject.Core.DTOS.FavioriteWalkDTOS;
using MWlaksProject.Core.DTOS.RegionDto;
using MWlaksProject.Core.DTOS.RegionDTOS;
using MWlaksProject.Core.DTOS.ReviewDto;
using MWlaksProject.Core.DTOS.WalksDTO;
using MWlaksProject.Core.DTOS.WalksDTOS;
using MWlaksProject.Core.Models;
using MWlaksProject.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.laksProject.Core.Mapper
{
    public class AutoMapperProfiling:Profile
    {
        public AutoMapperProfiling()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
           
            CreateMap<FaviouriteWalks, FaviouriteWalksDto>().ReverseMap();
            CreateMap<AddFaviouriteWalkDto, FaviouriteWalks>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();

            CreateMap<Difficulty, DifficultDTO>().ReverseMap();

            CreateMap<Walks, WalksDto>()
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Difficulty))
                .ReverseMap();

            CreateMap<AddWalkDto, Walks>()
                .ForMember(dest => dest.WalkImageUrl, opt => opt.Ignore());

            CreateMap<AddRegionDto, Region>()
                .ForMember(dest => dest.RegionImageUrl, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
