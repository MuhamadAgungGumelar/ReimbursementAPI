﻿using ReimbursementAPI.Models;
using ReimbursementAPI.Utilities.Enums;

namespace ReimbursementAPI.DTO.Account
{
    public class RegisterAccountsRequestDto : GeneralDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public GenderLevel Gender { get; set; }

        public DateTime HiringDate { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Major { get; set; }

        public string Degree { get; set; }

        public float GPA { get; set; }

        public string UniversityCode { get; set; }

        public string UniversityName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public static implicit operator Employees(RegisterAccountsRequestDto dto) //implementasi implicit Operator
        {
            return new Employees
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                HiringDate = dto.HiringDate,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}