﻿using System;

namespace MusicCatalog.Domain.Exceptions
{
    public class ArgumentNullValueException(string paramName)
        : ArgumentNullException(paramName, $"Argument \"{paramName}\" value is null")
    {
    }
}