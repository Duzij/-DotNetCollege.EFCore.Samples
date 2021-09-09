using DotNetCollege.EFCore.Sample1;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample01
{
    public class CustomSetBuilder : SqlServerConventionSetBuilder
    {
        public CustomSetBuilder([NotNullAttribute] ProviderConventionSetBuilderDependencies dependencies, [NotNullAttribute] RelationalConventionSetBuilderDependencies relationalDependencies, [NotNullAttribute] ISqlGenerationHelper sqlGenerationHelper) : base(dependencies, relationalDependencies, sqlGenerationHelper)
        {
        }

        public override ConventionSet CreateConventionSet()
        {
            var conventionSet = base.CreateConventionSet();
            var et = conventionSet.ForeignKeyAddedConventions.FirstOrDefault(f => f is ForeignKeyIndexConvention);
            if (et != null)
                conventionSet.ForeignKeyAddedConventions.Remove(et);
            return conventionSet;
        }
    }
}
