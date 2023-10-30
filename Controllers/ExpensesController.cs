using AutoMapper.Configuration.Annotations;
using Home.Models.DTOs;
using Home.Source.BusinessLayer;
using Home.Source.Common;
using Home.Source.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Home.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public partial class ExpensesController : ControllerBase
    {
        private readonly ExpensesLayer expenseLayer;

        public ExpensesController(ExpensesLayer expenseLayer)
        {
            this.expenseLayer = expenseLayer;
        }        

        private string GetUserId()
        {            
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
