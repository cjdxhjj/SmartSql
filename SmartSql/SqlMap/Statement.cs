﻿using SmartSql.Abstractions;
using SmartSql.Abstractions.Cache;
using SmartSql.Cache;
using SmartSql.Exceptions;
using SmartSql.SqlMap.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SmartSql.SqlMap
{
    public class Statement
    {
        [XmlIgnore]
        public SmartSqlMap SmartSqlMap { get; internal set; }
        [XmlAttribute]
        public String Id { get; set; }
        public String FullSqlId => $"{SmartSqlMap.Scope}.{Id}";
        public List<ITag> SqlTags { get; set; }
        public Cache Cache { get; set; }

        public ICacheProvider CacheProvider { get; internal set; }

        public String BuildSql(RequestContext context)
        {
            context.SmartSqlMap = SmartSqlMap;
            string smartPrefix = SmartSqlMap.SmartSqlMapConfig.Settings.ParameterPrefix;
            String dbPrefix = SmartSqlMap.SmartSqlMapConfig.Database.DbProvider.ParameterPrefix;
            StringBuilder sqlStrBuilder = new StringBuilder();
            foreach (ITag tag in SqlTags)
            {
                sqlStrBuilder.Append(tag.BuildSql(context));
            }
            return sqlStrBuilder.Replace(smartPrefix, dbPrefix).ToString();
        }
    }
}
