using System;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Timeline
{
    public interface ITimelineRestClient
    {
        int MaxRows { get; }

        TimelineSearchResults Scroll(string userId, string previousId, List<string> types, int rows = 500);

        TimelineSearchResults GetTimelineEvents(string userId, DateTime startDate, int offset, int rows, List<string> types);
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element