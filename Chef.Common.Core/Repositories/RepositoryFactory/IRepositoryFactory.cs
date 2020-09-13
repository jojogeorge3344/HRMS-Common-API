﻿using Chef.Common.Core;

namespace Chef.Common.Repositories
{
    public interface IRepositoryFactory
    {
        /// <summary>
        ///This is a Generic repository function. It will handle all the generic database repository actions such as Get, Getall, Insert, Update and Delete
        /// </summary>
        /// <typeparam name="TModel">Model</typeparam>
        /// <returns>IGenericRepository</returns>
        ICommonRepository<TModel> GenericRepository<TModel>()
            where TModel : Model;
    }
}
