﻿using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Models.AccountProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.AccessData.Repositories
{
    internal class PropertiesRepository : IPropertiesRepository
    {
        private readonly AppDbContext _context;
        public PropertiesRepository ( AppDbContext dbContext )
        {
            _context = dbContext;
        }

        public async Task<Email?> GetEmailAsync (string email)
        {
            return await _context.Emails.FirstOrDefaultAsync(e=>e.Value ==email);
        }

        public async Task<FirstName?> GetFirstNameAsync (string firstName)
        {
            return await _context.FirstNames.FirstOrDefaultAsync (e=>e.Value == firstName);
        }

        public async Task<LastName?> GetLastNameAsync (string lastName)
        {
            return await _context.LastNames.FirstOrDefaultAsync(l=> l.Value == lastName);
        }

        public async Task<PersonalCode?> GetPersonalCodeAsync (string personalCode)
        {
            return await _context.PersonalCodes.FirstOrDefaultAsync(e=>e.Value == personalCode);
        }

        public async Task<Phone?> GetPhoneAsync (string phoneNumber)
        {
            return await _context.Phones.FirstOrDefaultAsync(p => p.Value == phoneNumber);
        }
    }
}
