﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Enum
{
    public class SQL
    {
        public SQL()
        {

        }

        public class Function
        {

            public enum Aggregate
            {
                Max,
                Min,
                Count,
                Distinct,
                Sum,
                Avg
            }

            public Aggregate Aggregrate
            {
                get;
                set;
            }

        }

    }
}
