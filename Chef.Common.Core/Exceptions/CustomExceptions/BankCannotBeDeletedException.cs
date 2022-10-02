﻿using System;
using System.Runtime.Serialization;

namespace Chef.Common.Exceptions
{
    [Serializable]
    public class BankCannotBeDeletedException : Exception
    {
        public BankCannotBeDeletedException(string message) : base(message)
        {
        }
    }
}