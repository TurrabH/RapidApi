using AutoMapper;
using RapidPayService.Core.Dtos.Input;
using RapidPayService.Core.Dtos.Output;
using RapidPayService.EntityFramework.Entities;

namespace RapidPayService.Services
{
    public class AutomapperService : Profile
    {
        public AutomapperService()
        {
            CreateProfiles();
        }

        private void CreateProfiles()
        {
            CreateMap<CardHolder, InCardHolderDto>();
            CreateMap<InCardHolderDto, CardHolder>()
                .ForMember(dest => dest.CardHolderId, src => src.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CardHolderSince, src => src.MapFrom(src => src.CardHolderSince ?? DateTime.Now));
            CreateMap<CardHolder, OutCardHolderDto>();

            CreateMap<Card, InCardDto>();
            CreateMap<InCardDto, Card>().ForMember(dest=>dest.CreationDate, src => src.MapFrom(src=>src.CreationDate??DateTime.Now));
            CreateMap<Card, OutCardDto>();

            CreateMap<Transaction, OutTransactionDto>();

        }
    }
}
