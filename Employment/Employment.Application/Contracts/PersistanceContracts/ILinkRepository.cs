﻿using Employment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Contracts.PersistanceContracts
{
    public interface ILinkRepository : IGenericRepository<Link>
    {
        void AddToResume(Link link, Resume resume);
        Task AddToResumeAsync(int linkId, int resumeId);
    }
}
