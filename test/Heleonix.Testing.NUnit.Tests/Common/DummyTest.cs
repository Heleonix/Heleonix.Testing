// <copyright file="DummyTest.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Tests.Common
{
    using System.Collections.Generic;
    using global::NUnit.Framework.Internal;

    /// <summary>
    /// A dummy test.
    /// </summary>
    public class DummyTest : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyTest"/> class.
        /// </summary>
        /// <param name="testName">A name of a test.</param>
        public DummyTest(string testName)
            : base(testName)
        {
        }

        /// <inheritdoc/>
        public override object[] Arguments => null;

        /// <inheritdoc/>
        public override string XmlElementName => string.Empty;

        /// <inheritdoc/>
        public override bool HasChildren => false;

        /// <inheritdoc/>
        public override IList<global::NUnit.Framework.Interfaces.ITest> Tests => null;

        /// <inheritdoc/>
        public override global::NUnit.Framework.Interfaces.TNode AddToXml(global::NUnit.Framework.Interfaces.TNode parentNode, bool recursive)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override TestResult MakeTestResult()
        {
            throw new System.NotImplementedException();
        }
    }
}
