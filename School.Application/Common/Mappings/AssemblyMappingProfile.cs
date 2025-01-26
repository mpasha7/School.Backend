using AutoMapper;
using System.Reflection;

namespace School.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            ApplyMappingsFromAssembly(assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            // Сканируем сборку и ищем типы, которые реализуют IMapWith<> 
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            // Вызываем метод Mapping() такого типа (или из интерфейса, если тип не реализует этот метод)
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
