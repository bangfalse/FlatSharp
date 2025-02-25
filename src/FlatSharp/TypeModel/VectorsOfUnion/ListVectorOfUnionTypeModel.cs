﻿/*
 * Copyright 2021 James Courtney
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace FlatSharp.TypeModel;

/// <summary>
/// Defines a vector of union type model.
/// </summary>
public class ListVectorOfUnionTypeModel : BaseVectorOfUnionTypeModel
{
    public ListVectorOfUnionTypeModel(Type clrType, TypeModelContainer container)
        : base(clrType, container)
    {
    }

    public override string LengthPropertyName => nameof(List<byte>.Count);

    public override CodeGeneratedMethod CreateParseMethodBody(ParserCodeGenContext context)
    {
        var (classDef, className) = FlatBufferVectorHelpers.CreateVectorOfUnionItemAccessor(
            this.ItemTypeModel,
            context);

        string itemAccessorTypeName = $"{className}<{context.InputBufferTypeName}>";

        string createFlatBufferVector =
            $@"FlatBufferVectorBase<{this.ItemTypeModel.GetGlobalCompilableTypeName()}, {context.InputBufferTypeName}, {itemAccessorTypeName}>.GetOrCreate(
                    {context.InputBufferVariableName}, 
                    new {itemAccessorTypeName}(
                        {context.InputBufferVariableName},
                        {context.OffsetVariableName}.offset0 + {context.InputBufferVariableName}.{nameof(InputBufferExtensions.ReadUOffset)}({context.OffsetVariableName}.offset0), 
                        {context.OffsetVariableName}.offset1 + {context.InputBufferVariableName}.{nameof(InputBufferExtensions.ReadUOffset)}({context.OffsetVariableName}.offset1)),
                    {context.RemainingDepthVariableName},
                    {context.TableFieldContextVariableName},
                    {typeof(FlatBufferDeserializationOption).GetGlobalCompilableTypeName()}.{context.Options.DeserializationOption})";

        return new CodeGeneratedMethod(ListVectorTypeModel.CreateParseBody(
            this.ItemTypeModel,
            createFlatBufferVector,
            itemAccessorTypeName,
            context))
        { 
            ClassDefinition = classDef
        };
    }

    public override Type OnInitialize()
    {
        var genericDef = this.ClrType.GetGenericTypeDefinition();

        FlatSharpInternal.Assert(
            genericDef == typeof(IList<>) || genericDef == typeof(IReadOnlyList<>),
            "List vector of union must be IList or IReadOnlyList.");

        return this.ClrType.GetGenericArguments()[0];
    }
}
