﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Common
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class NomeColunaAttribute : Attribute
    {
        public string Header { get; }

        public NomeColunaAttribute(string header)
        {
            this.Header = header;
        }
    }
}
