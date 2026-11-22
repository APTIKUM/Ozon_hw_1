using System.Globalization;

namespace Presentation;

public sealed record ParsedArguments(string Writer, IReadOnlyCollection<long> ClientIds);

public static class ArgumentParser
{
    public static bool TryParse(string[] args, out ParsedArguments? parsedArguments, out string? error)
    {
        if (args.Length == 0)
        {
            parsedArguments = null;
            error = "Specify writer: console or file";
            return false;
        }

        var writer = args[0];
        if (writer != "console" && writer != "file")
        {
            parsedArguments = null;
            error = "Writer must be 'console' or 'file'";
            return false;
        }

        var clientIds = new List<long>();
        for (var i = 1; i < args.Length; i++)
        {
            if (!long.TryParse(
                    args[i],
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out var clientId))
            {
                parsedArguments = null;
                error = $"Invalid client id: '{args[i]}'";
                return false;
            }

            clientIds.Add(clientId);
        }

        parsedArguments = new ParsedArguments(writer, clientIds);
        error = null;
        return true;
    }
}