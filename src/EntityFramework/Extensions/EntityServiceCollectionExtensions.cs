// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Identity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Framework.DependencyInjection
{
    public static class EntityServiceCollectionExtensions
    {
        public static EntityServicesBuilder AddEntityFramework([NotNull] this IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, "serviceCollection");

            serviceCollection
                .AddSingleton<IModelSource, DefaultModelSource>()
                .AddSingleton<ModelBuilderFactory>()
                .AddSingleton<SimpleValueGeneratorFactory<TemporaryValueGenerator>>()
                .AddSingleton<SimpleValueGeneratorFactory<GuidValueGenerator>>()
                .AddSingleton<DbSetFinder>()
                .AddSingleton<DbSetInitializer>()
                .AddSingleton<EntityKeyFactorySource>()
                .AddSingleton<ClrPropertyGetterSource>()
                .AddSingleton<ClrPropertySetterSource>()
                .AddSingleton<ClrCollectionAccessorSource>()
                .AddSingleton<CollectionTypeFactory>()
                .AddSingleton<EntityMaterializerSource>()
                .AddSingleton<CompositeEntityKeyFactory>()
                .AddSingleton<ForeignKeyValueGenerator>()
                .AddSingleton<MemberMapper>()
                .AddSingleton<FieldMatcher>()
                .AddSingleton<OriginalValuesFactory>()
                .AddSingleton<RelationshipsSnapshotFactory>()
                .AddSingleton<StoreGeneratedValuesFactory>()
                .AddSingleton<ValueGeneratorSelector>()
                .AddScoped<DataStoreSelector>()
                .AddScoped<StateEntryFactory>()
                .AddScoped<IEntityStateListener, NavigationFixer>()
                .AddScoped<StateEntryNotifier>()
                .AddScoped<ChangeDetector>()
                .AddScoped<StateEntrySubscriber>()
                .AddScoped<DbContextConfiguration>()
                .AddScoped<ContextSets>()
                .AddScoped<StateManager>();

            return new EntityServicesBuilder(serviceCollection);
        }

        public static EntityServicesBuilder AddDbContext<TContext>(
            [NotNull] this EntityServicesBuilder builder,
            [CanBeNull] Action<DbContextOptions> optionsAction = null)
            where TContext : DbContext
        {
            Check.NotNull(builder, "builder");

            if (optionsAction != null)
            {
                builder.ServiceCollection.Configure<DbContextOptions<TContext>>(optionsAction);
            }

            builder.ServiceCollection.AddScoped(typeof(TContext), DbContextActivator.CreateInstance<TContext>);

            return builder;
        }
    }
}
