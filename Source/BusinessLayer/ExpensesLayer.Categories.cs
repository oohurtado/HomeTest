using Home.Models.DTOs;
using Home.Models.Entities.Money;
using Home.Source.Common;
using Home.Source.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Home.Source.BusinessLayer
{
    public partial class ExpensesLayer
    {
        public async Task<BaseResponse<CategoryDTO>> GetCategoryAsync(string userId, int id)
        {
            var category = await expenseRepository.GetCategory(userId, id).FirstOrDefaultAsync();

            if (category == null)
            {
                return BaseResponseHelper.RecordNotFound_OnGet<CategoryDTO>();
            }

            return new BaseResponse<CategoryDTO>()
            {
                Data = mapper.Map<CategoryDTO>(category)
            };
        }

        public async Task<BaseResponse> CreateCategoryAsync(string userId, CategoryEditorDTO dto)
        {
            var exists = await expenseRepository.GetSuperCategory(userId, dto.SuperCategoryId ?? 0).AnyAsync();
            if (!exists)
            {
                return BaseResponseHelper.GetExceptionError(new Exception("Super Category is not valid for this user"));
            }

            var category = new Category()
            {
            };

            mapper.Map(dto, category);
            await expenseRepository.CreateCategoryAsync(category);
            return await expenseRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> UpdateCategoryAsync(string userId, int id, CategoryEditorDTO dto)
        {
            var exists = await expenseRepository.GetSuperCategory(userId, dto.SuperCategoryId ?? 0).AnyAsync();
            if (!exists)
            {
                return BaseResponseHelper.GetExceptionError(new Exception("Super Category is not valid for this user"));
            }

            var category = await expenseRepository.GetCategory(userId, id).FirstOrDefaultAsync();
            if (category == null)
            {
                return BaseResponseHelper.RecordNotFound_OnUpdate();
            }

            mapper.Map(dto, category);
            return await expenseRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> DeleteCategoryAsync(string userId, int id)
        {
            var exists = await expenseRepository.GetCategory(userId, id).AnyAsync();
            if (!exists)
            {
                return BaseResponseHelper.GetExceptionError(new Exception("Super Category is not valid for this user"));
            }

            var category = await expenseRepository.GetCategory(userId, id).FirstOrDefaultAsync();
            if (category == null)
            {
                return BaseResponseHelper.RecordNotFound_OnDelete();
            }

            expenseRepository.DeleteCategory(category!);
            return await expenseRepository.SaveChangesAsync();
        }
    }
}
