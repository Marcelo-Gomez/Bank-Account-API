using Application.Commands.Response;
using AutoMapper;
using Domain.AggregatesModel.BankAccountAggregate;
using System.Diagnostics.CodeAnalysis;

namespace Application.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<BankAccount, StatementResponse>()
                .ForMember(dst => dst.ExtractDateUtc, opt => opt.Ignore());

            CreateMap<Account, AccountResponse>();

            CreateMap<AccountHistory, AccountHistoryResponse>();
        }
    }
}