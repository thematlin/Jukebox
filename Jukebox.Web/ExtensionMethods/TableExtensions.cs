using System.Collections.Generic;
using System.Web.Mvc;
using Jukebox.Business.Models;
using Jukebox.Web.Code;

namespace Jukebox.Web.ExtensionMethods
{
    public static class TableExtensions
    {
        public static MvcHtmlString LibraryTable(this HtmlHelper helper, IList<JukeboxTrack> tracks)
        {
            return MvcHtmlString.Create(TableBuilder.TrackTableBuilder(tracks, TrackTableKind.Library));
        }

        public static MvcHtmlString SearchTable(this HtmlHelper helper, IList<JukeboxTrack> tracks)
        {
            return MvcHtmlString.Create(TableBuilder.TrackTableBuilder(tracks, TrackTableKind.Search));
        }
    }
}