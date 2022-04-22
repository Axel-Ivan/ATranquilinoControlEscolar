﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SL_WCF
{
    public class Result
    {
        public bool Correct { get; set; }
        public Exception Ex { get; set; }
        public string ErrorMessage { get; set; }
        public object Object { get; set; }
        public List<object> Objects { get; set; }
    }
}