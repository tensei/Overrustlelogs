using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class MentionModel : IMentionModel {
        public double date { get; set; }
        public DateTime Date => UnixTimeStampToDateTime();
        public string nick { get; set; }
        public string text { get; set; }
        
        private DateTime UnixTimeStampToDateTime() {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(date).ToLocalTime();
            return dtDateTime;
        }
    }
}
