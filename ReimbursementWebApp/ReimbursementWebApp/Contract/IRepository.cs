﻿using ReimbursementAPI.Utilities.Handler;

namespace ReimbursementWebApp.Contract
{
    public interface IRepository<T, X>
        where T : class
    {
        Task<ResponseOKHandler<IEnumerable<T>>> Get();
        Task<ResponseOKHandler<T>> Get(X id);
        Task<ResponseOKHandler<T>> Post(T entity);
        Task<ResponseOKHandler<T>> Put(X id, T entity);
        Task<ResponseOKHandler<T>> Delete(X id);
    }
}
