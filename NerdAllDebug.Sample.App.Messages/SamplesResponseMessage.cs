using iBeach.Framework.Services;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NerdAllDebug.Sample.App.Messages
{
    /// <summary>
    /// Response that content the samples.
    /// </summary>
    public class SamplesResponseMessage : ResponseMessage
    {
        /// <summary>
        /// Represents the sample message.
        /// </summary>
        /// <value>Object sample</value>
        [JsonPropertyName("samples")]
        public IEnumerable<SampleMessage> Samples { get; set; }
    }
}
