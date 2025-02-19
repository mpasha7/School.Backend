﻿using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Interfaces;
using School.Persistence;

namespace School.Tests.Common
{
    public class TestQueriesFixtire : IDisposable
    {        
        public SchoolDbContext Context;
        public IMapper Mapper;

        public TestQueriesFixtire()
        {
            Context = CoursesContextFactory.Create();

            var configuratinProvider = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new AssemblyMappingProfile(typeof(ISchoolDbContext).Assembly));
            });
            Mapper = configuratinProvider.CreateMapper();
        }

        public void Dispose()
        {
            CoursesContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<TestQueriesFixtire>
    { }
}
