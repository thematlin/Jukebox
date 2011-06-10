using System.Collections.Generic;
using System.Web.Mvc;

namespace Jukebox.Web.ExtensionMethods
{
    public static class InputExtensions
    {
        public static MvcHtmlString Search(this HtmlHelper helper, string name, object htmlAttributes)
        {
            var realAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(realAttributes);
            tagBuilder.MergeAttribute("type", "search");
            tagBuilder.MergeAttribute("name", name);
            tagBuilder.GenerateId(name);

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}