﻿/*
 * SonarAnalyzer for .NET
 * Copyright (C) 2015-2023 SonarSource SA
 * mailto: contact AT sonarsource DOT com
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using SonarAnalyzer.Rules.CSharp;

namespace SonarAnalyzer.UnitTest.Rules
{
    [TestClass]
    public class DisposableTypesNeedFinalizersTest
    {
        private readonly VerifierBuilder builder = new VerifierBuilder<DisposableTypesNeedFinalizers>();

        [TestMethod]
        public void DisposableTypesNeedFinalizers() =>
            builder.AddPaths("DisposableTypesNeedFinalizers.cs").Verify();

#if NET

        [TestMethod]
        public void DisposableTypesNeedFinalizers_CSharp9() =>
            builder.AddPaths("DisposableTypesNeedFinalizers.CSharp9.cs").WithOptions(ParseOptionsHelper.FromCSharp9).Verify();

        [TestMethod]
        public void DisposableTypesNeedFinalizers_CSharp11() =>
            builder.AddPaths("DisposableTypesNeedFinalizers.CSharp11.cs").WithOptions(ParseOptionsHelper.FromCSharp11).Verify();

#endif

        [TestMethod]
        public void DisposableTypesNeedFinalizers_InvalidCode() =>
            builder.AddSnippet(@"
public class Foo_05 : IDisposable
{
    private HandleRef;
}").WithErrorBehavior(CompilationErrorBehavior.Ignore).Verify();
    }
}
