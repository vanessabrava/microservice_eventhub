using iBeach.Framework.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NerdAllDebug.Sample.App.Messages
{
    /// <summary>
    /// Request message for a new sample.
    /// </summary>
    public class NewSampleRequestMessage : RequestMessage
    {
        /// <summary>
        /// Represents name of sample.
        /// </summary>
        /// <value>String value.</value>
        [JsonPropertyName("name"), Required]
        public string Name { get; set; }
    }
}
