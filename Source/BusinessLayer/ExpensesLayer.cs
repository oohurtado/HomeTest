using AutoMapper;
using Home.Models.DTOs;
using Home.Models.Entities.Money;
using Home.Source.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Home.Source.BusinessLayer
{
    public partial class ExpensesLayer
    {
        private readonly IMapper mapper;
        private readonly SqlRepository sqlRepository;
        private readonly ExpenseRepository expenseRepository;

        public ExpensesLayer(
            IMapper mapper,
            SqlRepository sqlRepository,
            ExpenseRepository expenseRepository)
        {
            this.mapper = mapper;
            this.sqlRepository = sqlRepository;
            this.expenseRepository = expenseRepository;
        }   
    }
}
