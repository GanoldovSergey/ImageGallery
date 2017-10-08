using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ImageGallery.Models;

namespace ImageGallery.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            var result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("input");
                tag.MergeAttribute("type", "submit");
                tag.MergeAttribute("name", "page");
                tag.MergeAttribute("value", $"{i}");
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag);
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}