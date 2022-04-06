using CenterDevice.Model.Registry;
using System;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Model.Document
{
    public class DocumentSearchResult
    {
        public string Id { get; set; }

        public string Filename { get; set; }

        public DateTime? VersionDate { get; set; }

        public Lazy<List<string>> Paths { get; set; }

        public IconStatus IconOverlayStatus { get; set; }

        public string VersionDateString => VersionDate?.ToString();

        public long Version { get; set; }

        public bool ExistsLocally { get; set; }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element