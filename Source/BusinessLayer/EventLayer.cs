using AutoMapper;
using Home.Models.DTOs;
using Home.Models.Entities;
using Home.Models.Entities.Other;
using Home.Source.Common;
using Home.Source.Helpers;
using Home.Source.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Home.Source.BusinessLayer
{
    public class EventLayer
    {
        private readonly EventRepository eventRepository;
        private readonly PersonRepository personRepository;
        private readonly IMapper mapper;

        public EventLayer(
            EventRepository eventRepository,
            PersonRepository personRepository,
            IMapper mapper
            )
        {
            this.eventRepository = eventRepository;
            this.personRepository = personRepository;
            this.mapper = mapper;
        }



        public async Task<BaseResponse<EventDTO>> GetEventAsync(string userId, int personId, int eventId)
        {
            var @event = await eventRepository.GetEvent(userId, personId, eventId).FirstOrDefaultAsync();

            if (@event == null)
            {
                return BaseResponseHelper.RecordNotFound_OnGet<EventDTO>();
            }

            return new BaseResponse<EventDTO>()
            {
                Data = mapper.Map<EventDTO>(@event)
            };
        }

        public async Task<ActionResult<PageData<EventDTO>>> GetEventsByPageAsync(string userId, int personId, Paginator page)
        {
            var data = await eventRepository.GetEventsByPage(userId, personId, page.OrderColumn, page.Order, page.PageNumber, page.PageSize, page.Term, page.SearchColumn, out int grandTotal)
                .Select(p => new EventDTO()
                {
                    Id = p.Id,
                    Date = p.Date,
                    Description = p.Description,
                    ReminderTime = p.ReminderTime,
                    RemainingTime = p.RemainingTime,
                    RunningTime = p.RunningTime,                   
                    Time = p.Time
                })
                .ToListAsync();

            return new PageData<EventDTO>(data, grandTotal);
        }

        public async Task<BaseResponse> CreateEventAsync(string userId, int personId, EventEditorDTO dto)
        {
            var exists = await personRepository.GetPerson(userId, personId).AnyAsync();
            if (!exists)
            {
                return BaseResponseHelper.RecordNotFound_OnCreate();
            }

            var @event = new Event()
            {                
                PersonId = personId,
            };

            mapper.Map(dto, @event);
            await eventRepository.CreateEventAsync(@event);
            return await eventRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> UpdateEventAsync(string userId, int personId, int eventId, EventEditorDTO dto)
        {
            var @event = await eventRepository.GetEvent(userId, personId, eventId).FirstOrDefaultAsync();
            if (@event == null)
            {
                return BaseResponseHelper.RecordNotFound_OnUpdate();
            }

            mapper.Map(dto, @event);
            return await eventRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> DeleteEventAsync(string userId, int personId, int eventId)
        {
            var @event = await eventRepository.GetEvent(userId, personId, eventId).FirstOrDefaultAsync();

            if (@event == null)
            {
                return BaseResponseHelper.RecordNotFound_OnDelete();
            }

            eventRepository.DeleteEvent(@event!);
            return await eventRepository.SaveChangesAsync();
        }
    }
}
