using System.Net;
using ArtSchools.App.Globalization;

namespace ArtSchools.App.Exceptions;

public class UIException : Exception
{
    public int StatusCode { get; }
    public Language Message { get; }

    public UIException(Language message, int statusCode) : base(message.Oz)
    {
        StatusCode = statusCode;
        Message = message;
    }
}