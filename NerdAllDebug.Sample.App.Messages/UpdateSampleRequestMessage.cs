using Microsoft.AspNetCore.Mvc;
using iBeach.Framework.Services;
using System.Text.Json.Serialization;

namespace NerdAllDebug.Sample.App.Messages
{
    /// <summary>
    /// Request message for update sample.
    /// </summary>
    public class UpdateSampleRequestMessage : RequestMessage
    {
        /// <summary>
        /// Represents the identification of sample.
        /// </summary>
        /// <value>Int32 values.</value>
        [BindProperty(Name = "idSample")]
        public int IdSample { get; set; }

        /// <summary>
        /// Represents the update sample message.
        /// </summary>
        /// <value>Object sample</value>
        [FromBody, JsonPropertyName("sample")]
        public UpdateSampleMessage UpdateSampleMessage { get; set; }
    }
}
