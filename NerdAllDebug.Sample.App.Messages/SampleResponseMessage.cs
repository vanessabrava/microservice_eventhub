using iBeach.Framework.Services;
using System.Text.Json.Serialization;

namespace NerdAllDebug.Sample.App.Messages
{
    /// <summary>
    /// Response that content the sample info.
    /// </summary>
    public class SampleResponseMessage : ResponseMessage
    {
        /// <summary>
        /// Represents the sample message.
        /// </summary>
        /// <value>Object sample</value>
        [JsonPropertyName("sample")]
        public SampleMessage Sample { get; set; }
    }
}
