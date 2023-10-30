using Home.Models.DTOs;
using Home.Models.Entities.Money;
using Home.Source.Common;
using Home.Source.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Home.Source.BusinessLayer
{
    public partial class ExpensesLayer
    {
        public async Task<BaseResponse<EntryDTO>> GetEntryAsync(string userId, int id)
        {
            var entry = await expenseRepository.GetEntry(userId, id).FirstOrDefaultAsync();

            if (entry == null)
            {
                return BaseResponseHelper.RecordNotFound_OnGet<EntryDTO>();
            }

            return new BaseResponse<EntryDTO>()
            {
                Data = mapper.Map<EntryDTO>(entry)
            };
        }

        public async Task<BaseResponse> CreateEntryAsync(string userId, EntryEditorDTO dto)
        {
            var entry = new Entry()
            {
                UserId = userId,
            };

            // account updated
            var account = await expenseRepository.GetAccount(userId, dto.AccountId).FirstOrDefaultAsync();
            if (account == null)
            {
                return BaseResponseHelper.RecordNotFound_OnCreate();
            }
            account.Amount += dto.Amount * -1;

            // activity created
            await expenseRepository.CreateAccountActivityAsync(account.Id, dto.Amount * -1, DateTime.Today);

            // entry created
            mapper.Map(dto, entry);
            await expenseRepository.CreateEntryAsync(entry);
            return await expenseRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> UpdateEntryAsync(string userId, int id, EntryEditorDTO dto)
        {
            var entry = await expenseRepository.GetEntry(userId, id).FirstOrDefaultAsync();
            if (entry == null)
            {
                return BaseResponseHelper.RecordNotFound_OnUpdate();
            }

            // TODO: ...
            //var diff = entry.Amount - dto.Amount;
            //sameAccount.Amount += diff;
            //await expenseRepository.CreateAccountActivityAsync(sameAccount.Id, diff, entry.Date);          

            mapper.Map(dto, entry);
            return await expenseRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> DeleteEntryAsync(string userId, int id)
        {
            var entry = await expenseRepository.GetEntry(userId, id).FirstOrDefaultAsync();
            if (entry == null)
            {
                return BaseResponseHelper.RecordNotFound_OnDelete();
            }

            // TODO: ...
            // restar entry.amount a account.amount
            // crear actividad con entry.amount*-1

            expenseRepository.DeleteEntry(entry!);
            return await expenseRepository.SaveChangesAsync();
        }
    }
}
