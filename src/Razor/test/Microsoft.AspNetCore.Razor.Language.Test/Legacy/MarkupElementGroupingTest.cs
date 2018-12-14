// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Xunit;

namespace Microsoft.AspNetCore.Razor.Language.Legacy
{
    public class MarkupElementGroupingTest : CsHtmlMarkupParserTestBase
    {
        [Fact]
        public void Handles_ValidTags()
        {
            // Arrange
            var content = @"
<div>Foo</div>
<p>Bar</p>
";

            // Act & Assert
            ParseDocumentTest(content);
        }

        [Fact]
        public void Handles_ValidNestedTags()
        {
            // Arrange
            var content = @"
<div>
    Foo
    <p>Bar</p>
    Baz
</div>";

            // Act & Assert
            ParseDocumentTest(content);
        }

        [Fact]
        public void Handles_ValidNestedTagsMixedWithCode()
        {
            // Arrange
            var content = @"
<div>
    Foo
    <p>@Bar</p>
    @{ var x = Bar; }
</div>
";

            // Act & Assert
            ParseDocumentTest(content);
        }

        [Fact]
        public void Handles_EndTagsWithMissingStartTags()
        {
            // Arrange
            var content = @"
Foo</div>
";

            // Act & Assert
            ParseDocumentTest(content);
        }

        [Fact]
        public void Handles_StartTagsWithMissingEndTags()
        {
            // Arrange
            var content = @"
<div>
    Foo
    <p>
        Bar
        </strong>
";

            // Act & Assert
            ParseDocumentTest(content);
        }

        [Fact]
        public void Handles_SelfClosingTags()
        {
            // Arrange
            var content = @"
<br/>Foo<custom />
";

            // Act & Assert
            ParseDocumentTest(content);
        }

        [Fact]
        public void Handles_MalformedTags_RecoversSuccessfully()
        {
            // Arrange
            var content = @"
<div>content</span>footer</div>
";

            // Act & Assert
            ParseDocumentTest(content);
        }

        [Fact]
        public void Handles_MisplacedEndTags_RecoversSuccessfully()
        {
            // Arrange
            var content = @"
<div>content<span>footer</div></span>
";

            // Act & Assert
            ParseDocumentTest(content);
        }

        [Fact]
        public void Handles_DoesNotSpecialCase_VoidTags()
        {
            // Arrange
            var content = @"
<input>Foo</input>
";

            // Act & Assert
            ParseDocumentTest(content);
        }

        [Fact]
        public void Handles_IncompleteTags()
        {
            // Arrange
            var content = @"
<<div>>Foo</</div><   >
";

            // Act & Assert
            ParseDocumentTest(content);
        }
    }
}