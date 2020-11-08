using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.OperationTypeModels;
using FinanceManagmentApplication.Models.PaymentType;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Models.ScoreModel;
using FinanceManagmentApplication.Models.TransactionModels;
using FinanceManagmentApplication.Models.UserModels;

namespace FinanceManagmentApplication
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            ProjectMapping();
            CounterPartyMapping();
            UserMapping();
            OperationMapping();
            TransactionMapping();
            ScoreTransaction();
            PaymentTypeMapper();
            OperationTypeMapper();
        }

        private void ProjectMapping()
        {
            CreateMap<ProjectCreateModel, Project>();

            CreateMap<Project, ProjectIndexModel>();

            CreateMap<ProjectIndexModel, Project>();

            CreateMap<Project, ProjectFinanceModel>();
        }

        private void CounterPartyMapping()
        {
            CreateMap<CounterParty, CounterPartyIndexModel>();
            CreateMap<CounterPartyCreateModel, CounterParty>();
            CreateMap<CounterPartyEditModel, CounterParty>();
            CreateMap<CounterParty, CounterPartyEditModel>();
        }

        private void UserMapping()
        {
            CreateMap<User, UserIndexModel>();
        }
        private void OperationMapping()
        {
            CreateMap<Operation, OperationIndexModel>();
            CreateMap<Operation, OperationDetailsModel>();
            CreateMap<OperationCreateModel, Operation>();    
            CreateMap<OperationDetailsModel, Operation>();
            CreateMap<OperationEditModel, Operation>();
            CreateMap<Operation, OperationEditModel>();
           
        }

        private void TransactionMapping()
        {
            CreateMap<Transaction, TransactionIndexModel>()
                .ForMember(source => source.Score, target => target.MapFrom(src => src.Score.Code))
                .ForMember(source => source.CounterPartyName, target => target.MapFrom(src => src.CounterParty.Name))
                .ForMember(source => source.OperationName, target => target.MapFrom(src => src.Operation.Name))
                .ForMember(source => source.ProjectName, target => target.MapFrom(src => src.Project.Name))
                .ForMember(source => source.TransactionType, target => target.MapFrom(src => src.Operation.OperationType.Name));
            CreateMap<Transaction, TransactionDetailsModel>();
            CreateMap<TransactionCreateModel, Transaction>();
            CreateMap<TransactionEditModel, Transaction>();
            CreateMap<Transaction,TransactionEditModel>();
        }

        private void ScoreTransaction()
        {
            CreateMap<Score, ScoreIndexModel>();
            CreateMap<Score, ScoreDetailsModel>()
                .ForMember(Source => Source.PaymentType, target => target.MapFrom(src => src.PaymentType.Name));
            CreateMap<ScoreCreateModel, Score>();
            CreateMap<ScoreEditModel, Score>();
            CreateMap<Score,ScoreEditModel>();
        }

        private void PaymentTypeMapper()
        {
            CreateMap<PaymentType, PaymentTypeIndexModel>();
        }

        private void OperationTypeMapper()
        {
            CreateMap<OperationType, OperationTypeIndexModel>();
        }
    }
}
