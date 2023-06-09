﻿using Employment.Application.Contracts.PersistanceContracts;
using Employment.Application.Dtos.ApplicationServicesDtos;
using Employment.Common;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Dtos.Validations
{
    public class AddEducationHistoryDtoValidator : AbstractValidator<AddEducationHistoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddEducationHistoryDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(eh => eh.University)
                .NotNull().WithMessage("{PropertyName} نمی تواند خالی باشد")
                .NotEmpty().WithMessage("{PropertyName} نمی تواند خالی باشد")
                .MaximumLength(100).WithErrorCode("{PropertyName} نمی تواند بیشتر از 100 حرف داشته باشد.")
                .Must(value => value.Length > 3).WithErrorCode("{PropertyName} نمی تواند کمتر از 3 حرف داشته باشد.");

            RuleFor(eh => eh.Degree)
                .NotNull().WithMessage("{PropertyName} نمی تواند خالی باشد");

            RuleFor(eh => eh.GradePointAverage)
                .NotNull().WithMessage("{PropertyName} نمی تواند خالی باشد")
                .GreaterThanOrEqualTo(10).WithMessage("{PropertyName} باید بیشتر از 10 باشد.");

            RuleFor(eh => eh.StartDate)
                .NotNull().WithMessage("{PropertyName} نمی تواند خالی باشد")
                .NotEmpty().WithMessage("{PropertyName} نمی تواند خالی باشد");

            When(eh => eh.EndDate.HasValue, () =>
            {
                RuleFor(eh => eh.EndDate)
                        .NotEmpty().WithMessage("{PropertyName} نمی تواند خالی باشد")
                        .NotNull().WithMessage("{PropertyName} نمی تواند خالی باشد")
                        .GreaterThan(eh => eh.StartDate).WithMessage("{PropertyName} تاریخ پایان تحصیل باید بزرگ تر از شروع باشد.");
            });


            RuleFor(eh => eh.MajorId)
                .NotNull().WithMessage("{PropertyName} نمی تواند خالی باشد")
                .NotEmpty().WithMessage("{PropertyName} نمی تواند خالی باشد")
                .GreaterThan(0).WithMessage("{PropertyName} مقدار مناسبی وارد کنید")
                .Must(value => _isMajorExists(value)).WithMessage(ApplicationMessages.MajorNotFound);

            RuleFor(eh => eh.ResumeId)
                .NotEmpty().WithMessage("{PropertyName} نمی تواند خالی باشد")
                .NotNull().WithMessage("{PropertyName} نمی تواند خالی باشد")
                .GreaterThan(0).WithMessage("{PropertyName} مقدار مناسبی وارد کنید")
                .Must(value => _isResumeExists(value)).WithMessage(ApplicationMessages.ResumeNotFound);
        }

        private bool _isResumeExists(int resumeId)
        {
            return _unitOfWork.ResumeRepository.Get(resumeId) != null;
        }

        private bool _isMajorExists(int majorId)
        {
            return _unitOfWork.MajorRepository.Get(majorId) != null;
        }
    }
}
