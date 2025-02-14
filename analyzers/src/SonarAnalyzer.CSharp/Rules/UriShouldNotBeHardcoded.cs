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

namespace SonarAnalyzer.Rules.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class UriShouldNotBeHardcoded
        : UriShouldNotBeHardcodedBase<ExpressionSyntax, LiteralExpressionSyntax,
            SyntaxKind, BinaryExpressionSyntax, ArgumentSyntax, VariableDeclaratorSyntax>
    {
        private static readonly DiagnosticDescriptor Rule =
            DescriptorFactory.Create(DiagnosticId, MessageFormat);
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(Rule);
        protected override SyntaxKind StringLiteralSyntaxKind { get; } = SyntaxKind.StringLiteralExpression;
        protected override SyntaxKind[] StringConcatenateExpressions { get; } = { SyntaxKind.AddExpression };
        protected override GeneratedCodeRecognizer GeneratedCodeRecognizer => CSharpGeneratedCodeRecognizer.Instance;
        protected override string GetLiteralText(LiteralExpressionSyntax literalExpression) => literalExpression?.Token.ValueText;
        protected override ExpressionSyntax GetLeftNode(BinaryExpressionSyntax binaryExpression) => binaryExpression.Left;
        protected override ExpressionSyntax GetRightNode(BinaryExpressionSyntax binaryExpression) => binaryExpression.Right;
        protected override bool IsInvocationOrObjectCreation(SyntaxNode node) =>
            node.IsKind(SyntaxKind.InvocationExpression)
            || node.IsKind(SyntaxKind.ObjectCreationExpression);

        protected override int? GetArgumentIndex(ArgumentSyntax argument) =>
            (argument?.Parent as ArgumentListSyntax)?.Arguments.IndexOf(argument);

        protected override string GetDeclaratorIdentifierName(VariableDeclaratorSyntax declarator) =>
            declarator?.Identifier.ValueText;
    }
}
