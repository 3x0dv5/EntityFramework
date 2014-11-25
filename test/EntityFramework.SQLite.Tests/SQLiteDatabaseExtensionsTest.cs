// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;
using Moq;
using Xunit;

namespace Microsoft.Data.Entity.Sqlite.Tests
{
    public class SqliteDatabaseExtensionsTest
    {
        [Fact]
        public void Returns_typed_database_object()
        {
            var database = new SqliteDatabase(
                new LazyRef<IModel>(() => null),
                Mock.Of<SqliteDataStoreCreator>(),
                Mock.Of<SqliteConnection>(),
                Mock.Of<SqliteMigrator>(),
                new LoggerFactory());

            Assert.Same(database, database.AsSqlite());
        }

        [Fact]
        public void Throws_when_non_relational_provider_is_in_use()
        {
            var database = new ConcreteDatabase(
                new LazyRef<IModel>(() => null),
                Mock.Of<DataStoreCreator>(),
                Mock.Of<DataStoreConnection>(),
                new LoggerFactory());

            Assert.Equal(
                Strings.SqliteNotInUse,
                Assert.Throws<InvalidOperationException>(() => database.AsSqlite()).Message);
        }

        private class ConcreteDatabase : Database
        {
            public ConcreteDatabase(
                LazyRef<IModel> model,
                DataStoreCreator dataStoreCreator,
                DataStoreConnection connection,
                ILoggerFactory loggerFactory)
                : base(model, dataStoreCreator, connection, loggerFactory)
            {
            }
        }
    }
}
