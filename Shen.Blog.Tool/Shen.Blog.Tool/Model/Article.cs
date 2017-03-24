﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.Blog.Tool.Model
{
    /// <summary>
    /// 文章
    /// </summary>
    class Article
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public bool HasChange { get; set; }

        public long CategoryId { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }
    }
}
