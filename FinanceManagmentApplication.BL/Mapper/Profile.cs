using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.OperationTypeModels;
using FinanceManagmentApplication.Models.PaymentType;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Models.RemittanceModels;
using FinanceManagmentApplication.Models.ScoreModel;
using FinanceManagmentApplication.Models.TransactionModels;
using FinanceManagmentApplication.Models.UserModels;
using FinanceManagmentApplication.Models.FinanceModels;

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
            ScoreMapper();
            PaymentTypeMapper();
            OperationTypeMapper();
            RemittanceMapping();
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
            CreateMap<User, FinanceActiveUserIndexModel>();

        }
        private void OperationMapping()
        {
            CreateMap<Operation, OperationIndexModel>();
            CreateMap<Operation, OperationDetailsModel>();
            CreateMap<OperationCreateModel, Operation>();    
            CreateMap<OperationDetailsModel, Operation>();
            CreateMap<OperationEditModel, Operation>();
            CreateMap<Operation, OperationEditModel>();
            CreateMap<Operation, OperationFinanceModel>();
           
        }

        private void TransactionMapping()
        {
            CreateMap<Transaction, FinanceActiveIndexModel>()
                .ForMember(source => source.Score, target => target.MapFrom(src => src.Score.IsDelete ? src.Score.Name + "( удален)" : src.Score.Name))
                .ForMember(source => source.TargetEntity, target => target.MapFrom(src => src.CounterParty.IsDelete ? src.CounterParty.Name + "( удален)" : src.CounterParty.Name))
                .ForMember(source => source.OperationName, target => target.MapFrom(src => src.Operation.IsDelete ? "( удалена)" : src.Operation.Name))
                .ForMember(source => source.ProjectName, target => target.MapFrom(src => src.Project.IsDelete ? "( удален)" : src.Project.Name))
                .ForMember(source => source.TransactionType, target => target.MapFrom(src => src.Operation.OperationType.Name));
            CreateMap<Transaction, FinanceActiveUserIndexModel>()
                .ForMember(source => source.Score, target => target.MapFrom(src => src.Score.Name))
                .ForMember(source => source.TargetEntity, target => target.MapFrom(src => src.CounterParty.Name))
                .ForMember(source => source.OperationName, target => target.MapFrom(src => src.Operation.Name))
                .ForMember(source => source.ProjectName, target => target.MapFrom(src => src.Project.Name))
                .ForMember(source => source.TransactionType, target => target.MapFrom(src => src.Operation.OperationType.Name))
                .ForMember(source => source.ActionDate, target => target.MapFrom(src => src.ActionDate.ToString("d")));
            CreateMap<Transaction, TransactionDetailsModel>();
            CreateMap<TransactionCreateModel, Transaction>();
            CreateMap<TransactionEditModel, Transaction>();
            CreateMap<Transaction,TransactionEditModel>()
                .ForMember(source => source.CounterPartyName, target => target.MapFrom(src => src.CounterParty.Name))
                .ForMember(source => source.OperationName, target => target.MapFrom(src => src.Operation.Name))
                .ForMember(source => source.ProjectName, target => target.MapFrom(src => src.Project.Name))
                .ForMember(source => source.ScoreName, target => target.MapFrom(src => src.Score.Name));

            CreateMap<Transaction, TransactionExcelModel>()
                .ForMember(source => source.Score, target => target.MapFrom(src => src.Score.Name))
                .ForMember(source => source.CounterPartyName, target => target.MapFrom(src => src.CounterParty.Name))
                .ForMember(source => source.OperationName, target => target.MapFrom(src => src.Operation.Name))
                .ForMember(source => source.ProjectName, target => target.MapFrom(src => src.Project.Name))
                .ForMember(source => source.TransactionType, target => target.MapFrom(src => src.Operation.OperationType.Name));


        }

        private void ScoreMapper()
        {
            CreateMap<Score, ScoreIndexModel>();
            CreateMap<Score, ScoreDetailsModel>()
                .ForMember(Source => Source.PaymentType, target => target.MapFrom(src => src.PaymentType.Name));
            CreateMap<ScoreCreateModel, Score>();
            CreateMap<ScoreEditModel, Score>();
            CreateMap<Score, ScoreEditModel>();

        }

        private void RemittanceMapping()
        {
            CreateMap<Remittance, FinanceActiveIndexModel>()
               .ForMember(source => source.Score, target => target.MapFrom(src => src.Score.IsDelete ? src.Score.Name + "( удален)" : src.Score.Name))
               .ForMember(source => source.TargetEntity, target => target.MapFrom(src => src.Score2.IsDelete ? "( удален)" : src.Score2.Name))
               .ForMember(source => source.OperationName, target => target.MapFrom(src => src.Operation.IsDelete ? "( удалена)" : src.Operation.Name))
               .ForMember(source => source.ProjectName, target => target.MapFrom(src => src.Project.IsDelete ? "( удален)" : src.Project.Name))
               .ForMember(source => source.TransactionType, target => target.MapFrom(src => src.Operation.OperationType.Name))
               .ForMember(source => source.ActionDate, target => target.MapFrom(src => src.ActionDate.ToString("d")))
               .ForMember(source => source.UserName, target => target.MapFrom(src => src.User.UserName));
            CreateMap<RemittanceCreateModel, Remittance>();
            CreateMap<RemittanceEditModel, Remittance>();
            CreateMap<Remittance, RemittanceEditModel>()
                .ForMember(source => source.Score1Name, target => target.MapFrom(src => src.Score.Name))
                .ForMember(source => source.Score2Name, target => target.MapFrom(src => src.Score2.Name));

            CreateMap<Remittance, RemittanceExcelModel>()
                .ForMember(source => source.Score, target => target.MapFrom(src => src.Score.Name))
               .ForMember(source => source.Score2, target => target.MapFrom(src => src.Score2.Name))
               .ForMember(source => source.Sum, target => target.MapFrom(src => src.Sum.ToString()));
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
