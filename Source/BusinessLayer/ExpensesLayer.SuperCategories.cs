using Home.Models.DTOs;
using Home.Models.Entities.Money;
using Home.Models.Entities.Other;
using Home.Source.Common;
using Home.Source.Helpers;
using Home.Source.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Home.Source.BusinessLayer
{
    public partial class ExpensesLayer
    {
        public async Task<BaseResponse<SuperCategoryDTO>> GetSuperCategoryAsync(string userId, int id)
        {
            var supercategory = await expenseRepository.GetSuperCategory(userId, id).FirstOrDefaultAsync();

            if (supercategory == null)
            {
                return BaseResponseHelper.RecordNotFound_OnGet<SuperCategoryDTO>();
            }

            return new BaseResponse<SuperCategoryDTO>()
            {
                Data = mapper.Map<SuperCategoryDTO>(supercategory)
            };
        }
        
        public async Task<ActionResult<List<SuperCategoryDTO>>> GetSuperCategoriesAsync(string userId, bool includeCategories)
        {
            var data = await expenseRepository.GetSuperCategories(userId, includeCategories)
                .Select(p => new SuperCategoryDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsRegular = p.IsRegular,
                    IsService = p.IsService,                    
                    Categories = p.Categories.Select(c => new CategoryDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Budget = c.Budget,
                        SuperCategoryId = c.SuperCategoryId,
                    }).ToList()
                })
                .ToListAsync();

            return data;
        }

        public async Task<BaseResponse> CreateSuperCategoryAsync(string userId, SuperCategoryEditorDTO dto)
        {
            var superCategory = new SuperCategory()
            {
                UserId = userId,
            };

            mapper.Map(dto, superCategory);
            await expenseRepository.CreateSuperCategoryAsync(superCategory);
            return await expenseRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> UpdateSuperCategoryAsync(string userId, int id, SuperCategoryEditorDTO dto)
        {
            var superCategory = await expenseRepository.GetSuperCategory(userId, id).FirstOrDefaultAsync();
            if (superCategory == null)
            {
                return BaseResponseHelper.RecordNotFound_OnUpdate();
            }

            mapper.Map(dto, superCategory);
            return await expenseRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> DeleteSuperCategoryAsync(string userId, int id)
        {
            var superCategory = await expenseRepository.GetSuperCategory(userId, id).FirstOrDefaultAsync();
            if (superCategory == null)
            {
                return BaseResponseHelper.RecordNotFound_OnDelete();
            }

            expenseRepository.DeleteSuperCategoryAsync(superCategory!);
            return await expenseRepository.SaveChangesAsync();
        }
    }
}
