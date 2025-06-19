using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstractions;
public record Error(string Code, string Message)
{
    public static readonly Error None = new(String.Empty, String.Empty);

    public static readonly Error NullValue = new("Error.NullValue", "Null Value was Provided");

}