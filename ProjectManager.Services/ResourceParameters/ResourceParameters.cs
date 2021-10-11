using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Services.ResourceParameters
{
    public class ResourceParameters
    {
        const int maxPageSize = 20;
        public Dictionary<string,string> SearchQuery { get; set; }
        public Dictionary<string, string> FilterQuery { get; set; }
        public bool IncludeAll { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;

            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
