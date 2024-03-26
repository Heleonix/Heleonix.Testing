// <copyright file="BaseAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents the base attribute for all other tests attributes.
/// </summary>
/// <seealso cref="Attribute" />
public abstract class BaseAttribute : Attribute
{
    /// <summary>
    /// Gets the properties.
    /// </summary>
    /// <value>
    /// The properties.
    /// </value>
    protected virtual IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

    /// <summary>
    /// Gets the name of the test.
    /// </summary>
    /// <value>
    /// The name of the test.
    /// </value>
    protected abstract string TestName { get; }
}
