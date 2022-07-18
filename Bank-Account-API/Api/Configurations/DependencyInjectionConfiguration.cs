using Application.Commands.Request;
using Application.Helpers;
using Application.Helpers.Interfaces;
using Application.Queries;
using Application.Queries.Interfaces;
using Application.Validators;
using Domain.AggregatesModel.BankAccountAggregate;
using FluentValidation;
using Infrastructure.Data.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Api.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IValidator<DepositRequest>, DepositRequestValidator>();
            services.AddTransient<IValidator<WithdrawRequest>, WithdrawRequestValidator>();
            services.AddTransient<IValidator<TransferAccountRequest>, TransferAccountRequestValidator>();

            services.AddScoped<IAccountQuery, AccountQuery>();
            services.AddScoped<IStatementQuery, StatementQuery>();

            services.AddScoped<IAccountHistoryRepository, AccountHistoryRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<IAccountOperationsHelper, AccountOperationsHelper>();
        }
    }
}