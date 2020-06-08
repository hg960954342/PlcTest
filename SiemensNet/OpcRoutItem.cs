using System.Collections.Generic;

namespace SiemensNet
{
    public class OpcRoutItem
    {
        public string GroupName { get; set; }

        public List<RouteItem> ItemIDList { get; set; }

    }

    public class RouteItem
    {
        public string CaseId { get; set; }

        public string ItemId { get; set; }

        public string ClientId { get; set; }
    }
}
