// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Microsoft.Dynamics.CRM.attachment
    /// </summary>
    public partial class MicrosoftDynamicsCRMattachment
    {
        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMattachment
        /// class.
        /// </summary>
        public MicrosoftDynamicsCRMattachment()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMattachment
        /// class.
        /// </summary>
        /// <param name="mimetype">MIME type of the attachment.</param>
        /// <param name="filename">File name of the attachment.</param>
        /// <param name="attachmentid">Unique identifier of the
        /// attachment.</param>
        /// <param name="body">Contents of the attachment.</param>
        /// <param name="bodyBinary">Contents of the attachment.</param>
        /// <param name="filesize">File size of the attachment.</param>
        /// <param name="subject">Subject associated with the
        /// attachment.</param>
        /// <param name="versionnumber">Version number of the
        /// attachment.</param>
        public MicrosoftDynamicsCRMattachment(string mimetype = default(string), string filename = default(string), string attachmentid = default(string), string body = default(string), byte[] bodyBinary = default(byte[]), int? filesize = default(int?), string subject = default(string), string versionnumber = default(string), IList<MicrosoftDynamicsCRMsyncerror> attachmentSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), IList<MicrosoftDynamicsCRMactivitymimeattachment> attachmentActivityMimeAttachments = default(IList<MicrosoftDynamicsCRMactivitymimeattachment>))
        {
            Mimetype = mimetype;
            Filename = filename;
            Attachmentid = attachmentid;
            Body = body;
            BodyBinary = bodyBinary;
            Filesize = filesize;
            Subject = subject;
            Versionnumber = versionnumber;
            AttachmentSyncErrors = attachmentSyncErrors;
            AttachmentActivityMimeAttachments = attachmentActivityMimeAttachments;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets MIME type of the attachment.
        /// </summary>
        [JsonProperty(PropertyName = "mimetype")]
        public string Mimetype { get; set; }

        /// <summary>
        /// Gets or sets file name of the attachment.
        /// </summary>
        [JsonProperty(PropertyName = "filename")]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the attachment.
        /// </summary>
        [JsonProperty(PropertyName = "attachmentid")]
        public string Attachmentid { get; set; }

        /// <summary>
        /// Gets or sets contents of the attachment.
        /// </summary>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets contents of the attachment.
        /// </summary>
        [JsonConverter(typeof(Base64UrlJsonConverter))]
        [JsonProperty(PropertyName = "body_binary")]
        public byte[] BodyBinary { get; set; }

        /// <summary>
        /// Gets or sets file size of the attachment.
        /// </summary>
        [JsonProperty(PropertyName = "filesize")]
        public int? Filesize { get; set; }

        /// <summary>
        /// Gets or sets subject associated with the attachment.
        /// </summary>
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets version number of the attachment.
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Attachment_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> AttachmentSyncErrors { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "attachment_activity_mime_attachments")]
        public IList<MicrosoftDynamicsCRMactivitymimeattachment> AttachmentActivityMimeAttachments { get; set; }

    }
}
