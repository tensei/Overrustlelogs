using System;

namespace Overrustlelogs.Api.Interfaces {
    public interface IMentionModel {
        double date { get; set; }
        string nick { get; set; }
        string text { get; set; }
        DateTime Date { get; }
    }
}