using System;
using System.Collections.Generic;

namespace Code.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();

        private readonly Dictionary<Type, IService> _services = new();

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            _services[typeof(TService)] = implementation;
        }

        public TService Single<TService>() where TService : class, IService
        {
            return _services[typeof(TService)] as TService;
        }
    }
}