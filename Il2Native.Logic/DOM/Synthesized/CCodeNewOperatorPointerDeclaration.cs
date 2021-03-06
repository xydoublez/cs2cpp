﻿// Mr Oleksandr Duzhar licenses this file to you under the MIT license.
// If you need the License file, please send an email to duzhar@googlemail.com
// 
namespace Il2Native.Logic.DOM.Synthesized
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using DOM2;
    using Implementations;
    using Microsoft.CodeAnalysis;

    public class CCodeNewOperatorPointerDeclaration : CCodeInternalImplementationMethodDeclaration
    {
        public CCodeNewOperatorPointerDeclaration(INamedTypeSymbol type)
            : base(type, new NewOperatorMethod(type))
        {
            var parameterSymbols = new List<IParameterSymbol>();
            var arguments = new List<Expression>();

            var parameterSymbolSize = "_size".ToParameter();
            var parameterSize = new Parameter { ParameterSymbol = parameterSymbolSize };
            var parameterSymboPtr = "_ptr".ToParameter();
            var parameterPtr = new Parameter { ParameterSymbol = parameterSymboPtr };

            parameterSymbols.Add(parameterSymbolSize);
            parameterSymbols.Add(parameterSymboPtr);

            arguments.Add(parameterSize);
            arguments.Add(parameterPtr);

            MethodBodyOpt = new MethodBody(Method) { Statements = { new ReturnStatement { ExpressionOpt = parameterPtr } } };
        }

        public class NewOperatorMethod : MethodImpl
        {
            public NewOperatorMethod(INamedTypeSymbol type)
            {
                Name = "new";
                MethodKind = MethodKind.BuiltinOperator;
                ReceiverType = type;
                ContainingType = type;
                Parameters = ImmutableArray.Create("size_t".ToCType().ToParameter("_size"), SpecialType.System_Void.ToType().ToPointerType().ToParameter("_ptr"));
                ReturnType = SpecialType.System_Void.ToType().ToPointerType();
            }
        }
    }
}
