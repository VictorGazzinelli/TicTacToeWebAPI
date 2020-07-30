using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToeWebAPI.Utils.InversionControl
{
    public class DependencyResolver
    {
        private static volatile DependencyResolver _instance;
        private static volatile object _sync = new object();
        private readonly IUnityContainer _container = new UnityContainer();

        private DependencyResolver() 
        {
            UnityContainerExtensions.AddNewExtension<Interception>(this._container);
            UnityContainerExtensions.Configure<Interception>(this._container);
        }

        public static DependencyResolver Instance()
        {
            if (_instance == null)
            {
                object obj2 = _sync;
                lock (obj2)
                {
                    if (_instance == null)
                    {
                        _instance = new DependencyResolver();
                    }
                }
            }
            return _instance;
        }

        private static ResolverOverride[] BuildParameters(IOverrideMapping[] overrideMappings)
        {
            if (overrideMappings == null || !overrideMappings.Any())
                return null;
            List<ResolverOverride> resolverOverrides = new List<ResolverOverride>();
            resolverOverrides.AddRange(BuildParametersType(overrideMappings.OfType<OverrideMappingType>()));
            resolverOverrides.AddRange(BuildParametersName(overrideMappings.OfType<OverrideMappingName>()));
            return resolverOverrides.ToArray();
        }

        private static IEnumerable<ResolverOverride> BuildParametersType(IEnumerable<OverrideMappingType> overrideMappingTypes) =>
            (from OverrideMappingType in overrideMappingTypes select new DependencyOverride(OverrideMappingType.From, OverrideMappingType.To));

        private static IEnumerable<ResolverOverride> BuildParametersName(IEnumerable<OverrideMappingName> overrideMappingTypes) =>
            (from OverrideMappingType in overrideMappingTypes select new ParameterOverride(OverrideMappingType.ParameterName, OverrideMappingType.To));

        public void LoadMappings(params Mapping[] mappings)
        {
            object obj2 = _sync;
            lock (obj2)
            {
                foreach (Mapping mapping in mappings)
                    this.RegisterMapping(mapping, false);
            }
        }

        public T GetInstanceOf<T>() =>
            this.GetInstanceOf<T>(string.Empty);

        public T GetInstanceOf<T>(params IOverrideMapping[] overrideMappings) =>
            this.GetInstanceOf<T>(string.Empty, overrideMappings);

        public T GetInstanceOf<T>(string nome) =>
            this.GetInstanceOf<T>(nome, null);

        public T GetInstanceOf<T>(string name, params IOverrideMapping[] overrideMappings)
        {
            T local2;
            try
            {
                T local;
                ResolverOverride[] overrideArray = BuildParameters(overrideMappings);
                if (string.IsNullOrEmpty(name))
                {
                    local = (overrideArray == null) ? UnityContainerExtensions.Resolve<T>(this._container, new ResolverOverride[0]) : UnityContainerExtensions.Resolve<T>(this._container, overrideArray);
                }
                else
                {
                    local = (overrideArray == null) ? UnityContainerExtensions.Resolve<T>(this._container, name, new ResolverOverride[0]) : UnityContainerExtensions.Resolve<T>(this._container, name, overrideArray);
                }
                local2 = local;
            }
            catch (Exception exception)
            {
                throw new Exception($"Ocorreu erro ao tentar obter o tipo {typeof(T).Name}, verifique o injetor.", exception);
            }
            return local2;
        }

        private void RegisterMapping(Mapping mapping, bool writeOver)
        {
            if (string.IsNullOrEmpty(mapping.Name))
            {
                if (!writeOver && UnityContainerExtensions.IsRegistered(this._container, mapping.From))
                    throw new Exception($"Já existe registro de mapeamento para este tipo ( {mapping.From.FullName} ), verifique o tipo ou faça o registro explicitando um nome.");
                InjectionMember[] injectionMembers = new InjectionMember[] { new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<PolicyInjectionBehavior>() };
                UnityContainerExtensions.RegisterType(this._container, mapping.From, mapping.To, injectionMembers);
            }
            else
            {
                if (!writeOver && UnityContainerExtensions.IsRegistered(this._container, mapping.From))
                    throw new Exception($"Já existe registro de mapeamento nomeado para este tipo ( {mapping.From.FullName} ), verifique o tipo.");
                InjectionMember[] injectionMembers = new InjectionMember[] { new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<PolicyInjectionBehavior>() };
                UnityContainerExtensions.RegisterType(this._container, mapping.From, mapping.To, mapping.Name, injectionMembers);
            }
        }

    }
}
