﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SmartSql.Common;
using System.Collections;
using SmartSql.Abstractions;

namespace SmartSql.SqlMap.Tags
{
    public class IsEmpty : Tag
    {
        public override TagType Type => TagType.IsEmpty;

        public override bool IsCondition(RequestContext context)
        {
            Object reqVal = GetPropertyValue(context);
            if (reqVal == null)
            {
                return true;
            }
            if (reqVal is string)
            {
                return String.IsNullOrEmpty(reqVal as string);
            }
            if (reqVal is IEnumerable)
            {
                return !(reqVal as IEnumerable).GetEnumerator().MoveNext();
            }
            return reqVal.ToString().Length == 0;
        }
    }
}
