var baseUrl = '';

function UpdateQueueList() {

    baseUrl = $('#applicationUrl').val();

    var deleteImageHtml = '<a href="#" class="deleteSong"><img class="media-icon" src="' + baseUrl + 'Content/images/delete-shred.png" alt="Delete" title="Remove song from queue." /></a>';
    var infoSongHtml = '<td><a href="#" class="infoSong"><img class="media-icon" src="' + baseUrl + 'Content/images/information.png" alt="Info" /></</a><td>';
    var tableHtml = '';

    $.ajax({
        url: baseUrl + 'Playback/GetCustomQueue',
        success: function (result) {

            var alternatedRow = false;

            $.each(result, function () {
                if (alternatedRow) {
                    tableHtml += '<tr class="alternatedRow" id="' + this.PlaylistPosition + '">';
                    alternatedRow = false;
                }
                else {
                    tableHtml += '<tr id="' + this.PlaylistPosition + '">';
                    alternatedRow = true;
                }

                tableHtml += '<td>' + this.ArtistsAndName + '</td>';
                tableHtml += '<td>';
                tableHtml += deleteImageHtml + '</td>' + infoSongHtml;
                tableHtml += '</tr>';
            });

            $.ajax({
                url: baseUrl + 'Playback/GetLibraryQueue',
                success: function (result) {
                    $.each(result, function () {
                        if (alternatedRow) {
                            tableHtml += '<tr class="alternatedRow" id="' + this.PlaylistPosition + '">';
                            alternatedRow = false;
                        }
                        else {
                            tableHtml += '<tr id="' + this.PlaylistPosition + '">';
                            alternatedRow = true;
                        }

                        tableHtml += '<td>' + this.ArtistsAndName + '</td>';
                        tableHtml += '<td colspan="3">';
                        tableHtml += deleteImageHtml;
                        tableHtml += '</td>';
                        tableHtml += '</tr>';
                    });

                    $('#tracksQueue').html(tableHtml);
                }
            });
        }
    });
}