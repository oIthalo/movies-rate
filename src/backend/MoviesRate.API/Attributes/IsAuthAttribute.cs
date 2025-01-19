using Microsoft.AspNetCore.Mvc;
using MoviesRate.API.Filters;

namespace MoviesRate.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IsAuthAttribute : TypeFilterAttribute
{
    public IsAuthAttribute() : base(typeof(IsAuthFilter))
    {
    }
}