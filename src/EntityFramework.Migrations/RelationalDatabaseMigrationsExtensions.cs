﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Microsoft.Data.Entity.Migrations.Utilities;
using Microsoft.Data.Entity.Relational;
using Microsoft.Framework.DependencyInjection;

namespace Microsoft.Data.Entity
{
    public static class RelationalDatabaseMigrationsExtensions
    {
        public static void ApplyMigrations([NotNull] this RelationalDatabase database)
        {
            Check.NotNull(database, "database");

            var config = ((IDbContextConfigurationAdapter)database).Configuration;
            var migrator = config.Services.ServiceProvider.GetService<Migrator>();
            migrator.ApplyMigrations();
        }
    }
}
