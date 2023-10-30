using AutoMapper;
using Home.Models.DTOs;
using Home.Models.Entities;
using Home.Models.Entities.Other;
using Home.Source.Common;
using Home.Source.Helpers;
using Home.Source.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Home.Source.BusinessLayer
{
    public class ActivityLayer
    {
        private readonly ActivityRepository activityRepository;
        private readonly IMapper mapper;

        public ActivityLayer(
            ActivityRepository activityRepository,
            IMapper mapper)
        {
            this.activityRepository = activityRepository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<ActivityDTO>> GetActivityAsync(string userId, int id)
        {
            var activity = await activityRepository.GetActivity(userId, id).FirstOrDefaultAsync();

            if (activity == null)
            {
                return BaseResponseHelper.RecordNotFound_OnGet<ActivityDTO>();
            }

            return new BaseResponse<ActivityDTO>()
            {
                Data = mapper.Map<ActivityDTO>(activity)
            };
        }

        public async Task<ActionResult<PageData<ActivityDTO>>> GetActivitiesByDateAsync(string userId, int month, int year, Paginator page)
        {
            var dateMin = new DateTime(year, month, 1);
            var dateMax = new DateTime(year, month, DateTime.DaysInMonth(year, month));          

            var data = await activityRepository.GetActivitiesByDate(userId, page.OrderColumn, page.Order, page.Term, page.SearchColumn, dateMin, dateMax)
                .Select(p => new ActivityDTO()
                {
                    Id = p.Id,
                    Date = p.Date,
                    Description = p.Description,
                    IsDone = p.IsDone,
                    Tag = p.Tag,
                    Time = p.Time
                }).ToListAsync();

            return new PageData<ActivityDTO>(data, data.Count);
        }

        public async Task<BaseResponse> CreateActivityAsync(string userId, ActivityEditorDTO dto)
        {
            var activity = new Activity()
            {
                UserId = userId,
            };

            mapper.Map(dto, activity);
            await activityRepository.CreateActivityAsync(activity);
            return await activityRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> UpdateActivityAsync(string userId, int id, ActivityEditorDTO dto)
        {
            var activity = await activityRepository.GetActivity(userId, id).FirstOrDefaultAsync();

            if (activity == null)
            {
                return BaseResponseHelper.RecordNotFound_OnUpdate();
            }

            mapper.Map(dto, activity);
            return await activityRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> UpdateActivityStatusAsync(string userId, int id, ActivityStatusEditorDTO dto)
        {
            var activity = await activityRepository.GetActivity(userId, id).FirstOrDefaultAsync();

            if (activity == null)
            {
                return BaseResponseHelper.RecordNotFound_OnUpdate();
            }

            mapper.Map(dto, activity);
            return await activityRepository.SaveChangesAsync();
        }        

        public async Task<BaseResponse> DeleteActivityAsync(string userId, int id)
        {
            var activity = await activityRepository.GetActivity(userId, id).FirstOrDefaultAsync();

            if (activity == null)
            {
                return BaseResponseHelper.RecordNotFound_OnDelete();
            }

            activityRepository.DeleteActivity(activity!);
            return await activityRepository.SaveChangesAsync();
        }

        public async Task<List<string>> GetTagsAsync(string userId)
        {
            var tags = await activityRepository.GetTags(userId).ToListAsync();
            return tags;
        }
    }
}
