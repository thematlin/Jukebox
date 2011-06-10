using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jukebox.Business.Models.Contracts;
using System.Text;
using System.Diagnostics;
using Jukebox.Web.Code;

namespace Jukebox.Web.ExtensionMethods
{
    public static class TableExtensions
    {
        public static MvcHtmlString LibraryTable(this HtmlHelper helper, IList<IJukeboxTrack> tracks)
        {
            return MvcHtmlString.Create(TableBuilder.TrackTableBuilder(tracks, TrackTableKind.Library));
        }

        public static MvcHtmlString SearchTable(this HtmlHelper helper, IList<IJukeboxTrack> tracks)
        {
            return MvcHtmlString.Create(TableBuilder.TrackTableBuilder(tracks, TrackTableKind.Search));
        }
    }
}