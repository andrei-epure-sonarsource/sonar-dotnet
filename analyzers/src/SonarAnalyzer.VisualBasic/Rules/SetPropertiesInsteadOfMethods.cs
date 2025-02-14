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

namespace SonarAnalyzer.Rules.VisualBasic;

[DiagnosticAnalyzer(LanguageNames.VisualBasic)]
public sealed class SetPropertiesInsteadOfMethods : SetPropertiesInsteadOfMethodsBase<SyntaxKind, InvocationExpressionSyntax>
{
    protected override ILanguageFacade<SyntaxKind> Language => VisualBasicFacade.Instance;

    protected override bool HasCorrectArgumentCount(InvocationExpressionSyntax invocation) =>
        invocation.HasExactlyNArguments(1);

    // In VB you need to be explicit: Enumerable.Min(set), because set.Min() resolves to the property.
    protected override bool TryGetOperands(InvocationExpressionSyntax invocation, out SyntaxNode typeNode, out SyntaxNode methodNode)
    {
        typeNode = invocation.ArgumentList.Arguments[0].GetExpression();
        methodNode = invocation;
        return true;
    }
}
