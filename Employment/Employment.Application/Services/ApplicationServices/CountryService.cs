﻿using Employment.Application.Contracts.ApplicationServicesContracts;
using Employment.Application.Contracts.PersistanceContracts;
using Employment.Application.Dtos.ApplicationServicesDtos;
using Employment.Application.Dtos.Validations;
using Employment.Common;
using Employment.Common.Dtos;
using Employment.Common.Exceptions;
using Employment.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Services.ApplicationServices
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResule<int>> AddAsync(AddCountryDto addCountryDto)
        {
            var validationResult = await new AddCountryDtoValidator(_unitOfWork).ValidateAsync(addCountryDto);
            var country = new Country()
            {
                Name = addCountryDto.Name,
            };
            await _unitOfWork.CountryRepository.AddAsync(country);
            return new CommandResule<int>()
            {
                IsSuccess = true,
                Message = ApplicationMessages.CountryAdded,
                Data = country.Id
            };
        }
    }
}
