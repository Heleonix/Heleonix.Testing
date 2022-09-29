// <copyright file="SpecNode.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Internal
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a spec node.
    /// </summary>
    internal class SpecNode
    {
        private readonly List<SpecNode> children = new List<SpecNode>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecNode"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="description">The description.</param>
        /// <param name="action">The action.</param>
        public SpecNode(SpecNodeType type, string description, Action action)
        {
            this.Type = type;
            this.Description = description;
            this.Action = action;
            this.NestingLevel = -1;
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public Action Action { get; private set; }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public IReadOnlyCollection<SpecNode> Children
        {
            get
            {
                return this.children.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the nesting level.
        /// </summary>
        /// <value>
        /// The nesting level.
        /// </value>
        public int NestingLevel { get; private set; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public SpecNode Parent { get; private set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public SpecNodeType Type { get; private set; }

        /// <summary>
        /// Adds the specified child.
        /// </summary>
        /// <param name="child">The child.</param>
        public void Add(SpecNode child)
        {
            child.Parent = this;

            child.NestingLevel = this.NestingLevel + 1;

            this.children.Add(child);
        }

        /// <summary>
        /// Removes the specified child.
        /// </summary>
        /// <param name="child">The child.</param>
        public void Remove(SpecNode child)
        {
            child.Parent = null;

            this.children.Remove(child);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => this.Type.ToString();
    }
}
