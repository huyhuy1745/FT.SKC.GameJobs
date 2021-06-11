using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FT.NH88.GameJobs.Models
{
    public class UserProfile    {
        
        [JsonProperty("chatid")]
        public int? ChatId { get; set; } 
        
        [JsonProperty("createdtime")]
        public DateTime CreatedTime { get; set; } 
        
        [JsonProperty("createdevicetype")]
        public object CreateDeviceType { get; set; } 
        
        [JsonProperty("lasttimechangedphonenumber")]
        public object LastTimeChangedPhoneNumber { get; set; } 
        
        [JsonProperty("otpsafeid")]
        public object OtpSafeId { get; set; } 
        
        [JsonProperty("isverifiedphone")]
        public bool IsVerifiedPhone { get; set; } 
        
        [JsonProperty("unixupdatedat")]
        public int UnixUpdatedAt { get; set; } 
        
        [JsonProperty("refuserid")]
        public object RefUserId { get; set; } 
        
        [JsonProperty("note")]
        public object Note { get; set; } 
        
        [JsonProperty("@version")]
        public string Version { get; set; } 
        
        [JsonProperty("email")]
        public object Email { get; set; } 
        
        [JsonProperty("agencylevel")]
        public int AgencyLevel { get; set; } 
        
        [JsonProperty("userid")]
        public int UserId { get; set; } 
        
        [JsonProperty("nearestagencyid")]
        public long? NearestAgencyId { get; set; } 
        
        [JsonProperty("serviceid")]
        public int ServiceId { get; set; } 
        
        [JsonProperty("username")]
        public string Username { get; set; } 
        
        [JsonProperty("status")]
        public int Status { get; set; } 
        
        [JsonProperty("avatar")]
        public object Avatar { get; set; } 
        
        [JsonProperty("phoneotp")]
        public string PhoneOtp { get; set; } 
        
        [JsonProperty("authentype")]
        public object AuthenType { get; set; } 
        
        [JsonProperty("@timestamp")]
        public DateTime Timestamp { get; set; } 
        
        [JsonProperty("facebookid")]
        public object FacebookId { get; set; } 
        
        [JsonProperty("hierarchy")]
        public string Hierarchy { get; set; } 
        
        [JsonProperty("displayname")]
        public string Displayname { get; set; } 
        
        [JsonProperty("tags")]
        public List<string> Tags { get; set; } 
        
        [JsonProperty("isfirstuseotp")]
        public bool IsFirstUseOtp { get; set; } 
    }
}