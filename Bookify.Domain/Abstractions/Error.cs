using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstractions;
public record Error(string Code, string Message)
{
    public static Error None = new(String.Empty, String.Empty);

    public static Error NullValue = new("Error.NullValue", "Null Value was Provided");
    public static Error InvalidInput(string message) => new("InvalidInput", message);
    public static Error NotFound(string message) => new("NotFound", message);
    public static Error Conflict(string message) => new("Conflict", message);
    public static Error Unauthorized(string message) => new("Unauthorized", message);
    public static Error InternalError(string message) => new("InternalError", message);
}