// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace ContentService.Core.AggregateModel.FieldConfigAggregate;

[Owned]
public class Props
{

    public string Label { get; set; }
    public string Placeholder { get; set; }
    public bool Required { get; set; }
}

