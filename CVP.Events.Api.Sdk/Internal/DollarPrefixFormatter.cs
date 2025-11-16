using Refit;

namespace CVP.Events.Api.Sdk.Internal;

public sealed class DollarPrefixFormatter : IUrlParameterKeyFormatter
{
    private static readonly CamelCaseUrlParameterKeyFormatter _camelCaseFormatter = new();
    public string Format(string key) => $"${_camelCaseFormatter.Format(key)}";
}
