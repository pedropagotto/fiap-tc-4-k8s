using System.Diagnostics.CodeAnalysis;

namespace Common.Config;
[ExcludeFromCodeCoverage]
public class TechChallengeFiapConfiguration : ITechChallengeFiapConfiguration
{
    public string AuthSecret { get; set; }
}