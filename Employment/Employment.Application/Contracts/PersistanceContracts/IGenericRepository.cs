﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Contracts.PersistanceContracts
{
    public interface IGenericRepository<T> where T : class
    {


        // get ---
        IEnumerable<T> GetAll(List<string>? includes = null);
        T Get(int id, List<string>? includes = null);
        /// <summary>
        /// get all entities as queryable and as no tracking.
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<T> GetAllAsQueryable(List<string>? includes = null);

        // update ---
        Task UpdateAsync(T entity);
        void Update(T entity);

        // delete ---
        void Delete(T entity);
        void Delete(int id);

        // add ---
        Task AddAsync(T entity);
        void Add(T entity);
    }
}
