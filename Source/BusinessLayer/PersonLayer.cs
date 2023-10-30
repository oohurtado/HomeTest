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
    public class PersonLayer
    {
        private readonly PersonRepository personRepository;
        private readonly IMapper mapper;

        public PersonLayer(
            PersonRepository personRepository,
            IMapper mapper
            )
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<PersonDTO>> GetPersonAsync(string userId, int id)
        {
            var person = await personRepository.GetPerson(userId, id).FirstOrDefaultAsync();

            if (person == null)
            {
                return BaseResponseHelper.RecordNotFound_OnGet<PersonDTO>();
            }

            return new BaseResponse<PersonDTO>()
            {
                Data = mapper.Map<PersonDTO>(person)
            };
        }

        public async Task<ActionResult<PageData<PersonDTO>>> GetPeopleByPageAsync(string userId, Paginator page)
        {
            var data = await personRepository.GetPersonsByPage(userId, page.OrderColumn, page.Order, page.PageNumber, page.PageSize, page.Term, page.SearchColumn, out int grandTotal)
                .Select(p => new PersonDTO()
                {                    
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,             
                })
                .ToListAsync();

            return new PageData<PersonDTO>(data, grandTotal);
        }

        public async Task<BaseResponse> CreatePersonAsync(string userId, PersonEditorDTO dto)
        {
            var person = new Person()
            {
                UserId = userId,
            };

            mapper.Map(dto, person);
            await personRepository.CreatePersonAsync(person);
            return await personRepository.SaveChangesAsync();
        } 

        public async Task<BaseResponse> UpdatePersonAsync(string userId, int id, PersonEditorDTO dto)
        {
            var person = await personRepository.GetPerson(userId, id).FirstOrDefaultAsync();

            if (person == null)
            {
                return BaseResponseHelper.RecordNotFound_OnDelete();
            }

            mapper.Map(dto, person);
            return await personRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> DeletePersonAsync(string userId, int id)
        {
            var person = await personRepository.GetPerson(userId, id).FirstOrDefaultAsync();

            if (person == null)
            {
                return BaseResponseHelper.RecordNotFound_OnDelete();
            }

            personRepository.DeletePerson(person!);
            return await personRepository.SaveChangesAsync();
        }
    }
}
