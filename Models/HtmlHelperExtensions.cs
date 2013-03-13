using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace MusicSite.Models
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString CustomValidationSummary(this HtmlHelper helper, ModelStateDictionary data)
        {
            var result = new StringBuilder();
            foreach (ModelState modelState in data.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    TagBuilder tag = new TagBuilder("div");
                    tag.MergeAttribute("class", "alert-box alert");
                    tag.MergeAttribute("data-alert", "");
                    tag.InnerHtml = string.Format("{0}<a href=\"#\" class=\"close\">x</a>", error.ErrorMessage);
                    result.Append(tag.ToString());
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}