﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Query
{
    public class SimpleQuerySqlServerTest : SimpleQueryRelationalTestBase
    {
        protected override ITestStoreFactory TestStoreFactory => SqlServerTestStoreFactory.Instance;

        public override async Task Multiple_nested_reference_navigations(bool async)
        {
            await base.Multiple_nested_reference_navigations(async);

            AssertSql(
                @"@__p_0='3'

SELECT TOP(1) [s].[Id], [s].[Email], [s].[Logon], [s].[ManagerId], [s].[Name], [s].[SecondaryManagerId]
FROM [Staff] AS [s]
WHERE [s].[Id] = @__p_0",
                //
                @"@__id_0='1'

SELECT TOP(2) [a].[Id], [a].[Complete], [a].[Deleted], [a].[PeriodEnd], [a].[PeriodStart], [a].[StaffId], [s].[Id], [s].[Email], [s].[Logon], [s].[ManagerId], [s].[Name], [s].[SecondaryManagerId], [s0].[Id], [s0].[Email], [s0].[Logon], [s0].[ManagerId], [s0].[Name], [s0].[SecondaryManagerId], [s1].[Id], [s1].[Email], [s1].[Logon], [s1].[ManagerId], [s1].[Name], [s1].[SecondaryManagerId]
FROM [Appraisals] AS [a]
INNER JOIN [Staff] AS [s] ON [a].[StaffId] = [s].[Id]
LEFT JOIN [Staff] AS [s0] ON [s].[ManagerId] = [s0].[Id]
LEFT JOIN [Staff] AS [s1] ON [s].[SecondaryManagerId] = [s1].[Id]
WHERE [a].[Id] = @__id_0");
        }

        public override async Task Comparing_enum_casted_to_byte_with_int_parameter(bool async)
        {
            await base.Comparing_enum_casted_to_byte_with_int_parameter(async);

            AssertSql(
                @"@__bitterTaste_0='1'

SELECT [i].[IceCreamId], [i].[Name], [i].[Taste]
FROM [IceCreams] AS [i]
WHERE [i].[Taste] = @__bitterTaste_0");
        }

        public override async Task Comparing_enum_casted_to_byte_with_int_constant(bool async)
        {
            await base.Comparing_enum_casted_to_byte_with_int_constant(async);

            AssertSql(
                @"SELECT [i].[IceCreamId], [i].[Name], [i].[Taste]
FROM [IceCreams] AS [i]
WHERE [i].[Taste] = 1");
        }

        public override async Task Comparing_byte_column_to_enum_in_vb_creating_double_cast(bool async)
        {
            await base.Comparing_byte_column_to_enum_in_vb_creating_double_cast(async);

            AssertSql(
                @"SELECT [f].[Id], [f].[Taste]
FROM [Food] AS [f]
WHERE [f].[Taste] = CAST(1 AS tinyint)");
        }
    }
}
