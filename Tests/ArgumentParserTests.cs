using Presentation;
using FluentAssertions;

namespace Tests;

public sealed class ArgumentParserTests
{
    [Test]
    public void TryParse_ValidArguments_ReturnsWriterAndClientIds()
    {
        var parsed = ArgumentParser.TryParse(
            ["console", "1010", "1020"],
            out var arguments,
            out var error);

        parsed.Should().BeTrue();
        error.Should().BeNull();
        arguments!.Writer.Should().Be("console");
        arguments.ClientIds.Should().Equal(1010L, 1020L);
    }

    [Test]
    public void TryParse_NoArguments_ReturnsExpectedError()
    {
        var parsed = ArgumentParser.TryParse(
            [],
            out var arguments,
            out var error);

        parsed.Should().BeFalse();
        arguments.Should().BeNull();
        error.Should().Be("Specify writer: console or file");
    }

    [TestCase("wrong")]
    [TestCase("console", "not-a-number")]
    public void TryParse_InvalidArguments_ReturnsError(params string[] args)
    {
        var parsed = ArgumentParser.TryParse(
            args,
            out var arguments,
            out var error);

        parsed.Should().BeFalse();
        arguments.Should().BeNull();
        error.Should().NotBeNullOrEmpty();
    }
}
