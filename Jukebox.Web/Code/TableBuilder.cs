using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jukebox.Business.Models.Contracts;
using System.Web.Mvc;
using System.Web;
namespace Jukebox.Web.Code
{
    public static class TableBuilder
    {
        public static string TrackTableBuilder(IList<IJukeboxTrack> tracks, TrackTableKind kind)
        {
            var table = new StringBuilder();

            var lettersHash = new HashSet<string>(tracks.Select(x => x.ArtistsAndName.Substring(0, 1)));
            var letters = lettersHash.ToList();
            letters.Sort((x, y) => string.Compare(x, y));


            var alternateRow = false;
            foreach (var letter in letters)
            {
                var currentLetterTracks = tracks.Where(x => x.ArtistsAndName.StartsWith(letter));

                table.Append("<h3>" + letter.ToUpper() + "</h3>");
                table.Append("<table class=\"" + GetTableCssClass(kind) + "\">");

                if (alternateRow)
                {
                    table.Append("<tr class=\"alternatedRow\">");
                    alternateRow = false;
                }
                else
                {
                    table.Append("<tr>");
                    alternateRow = true;
                }

                var columnsAdded = 0;
                foreach (var track in currentLetterTracks)
                {
                    if (columnsAdded == 3)
                    {
                        table.Append("</tr>");

                        if (alternateRow)
                        {
                            table.Append("<tr class=\"alternatedRow\">");
                            alternateRow = false;
                        }
                        else
                        {
                            table.Append("<tr>");
                            alternateRow = true;
                        }

                        columnsAdded = 0;
                    }

                    table.Append(AddNewColumns(track, track.Equals(currentLetterTracks.Last()), columnsAdded, kind));

                    columnsAdded++;
                }

                table.Append("</tr></table>");
            }

            return table.ToString();
        }

        private static string AddNewColumns(IJukeboxTrack track, bool lastColumnOfTable, int columnsAdded, TrackTableKind kind)
        {
            var columnPart = new StringBuilder();

            columnPart.Append("<td>");
            columnPart.Append(track.ArtistsAndName);
            columnPart.Append("</td>");

            var fictional = (columnsAdded + 1);

            if (lastColumnOfTable)
            {
                if (fictional == 1)
                {
                    columnPart.Append("<td class=\"iconColumn\" colspan=\"5\">");
                }
                else if (fictional == 2)
                {
                    columnPart.Append("<td class=\"iconColumn\" colspan=\"3\">");
                }
                else
                {
                    columnPart.Append("<td class=\"iconColumn\">");
                }
            }
            else
            {
                columnPart.Append("<td class=\"iconColumn\">");
            }


            columnPart.Append("<a href=\"#\" id=\"trackId_" + track.PlaylistPosition + "\" class=\"" + GetAddButtonClass(kind) + "\">");
            columnPart.Append(GetImage(kind) + "</a></td>");

            return columnPart.ToString();
        }

        private static string GetTableCssClass(TrackTableKind kind)
        {
            switch (kind)
            {
                case TrackTableKind.Library:
                    return "libraryAllSongs";
                case TrackTableKind.Search:
                    return "searchAllSongs";
            }

            return "libraryAllSongs";
        }

        private static string GetAddButtonClass(TrackTableKind kind)
        {
            switch (kind)
            {
                case TrackTableKind.Library:
                    return "AddToQueue";
                case TrackTableKind.Search:
                    return "AddToLibrary";
            }

            return string.Empty;
        }

        private static string GetImage(TrackTableKind kind)
        {
            var virtaulPath = HttpRuntime.AppDomainAppVirtualPath.Equals("/") ? "" : HttpRuntime.AppDomainAppVirtualPath;

            switch (kind)
            {
                case TrackTableKind.Library:
                    return "<img src=\"" + virtaulPath + "/Content/images/add.png\" class=\"media-icon\" alt=\"Add to queue\" title=\"Add to queue\" />";
                case TrackTableKind.Search:
                    return "<img src=\"" + virtaulPath + "/Content/images/add.png\" class=\"media-icon\" alt=\"Add to library\" title=\"Add to library\" />";
            }

            return "";
        }
    }

    public enum TrackTableKind
    {
        Library,
        Search
    }
}