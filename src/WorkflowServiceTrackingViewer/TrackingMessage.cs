using System;
using System.Activities.Tracking;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PeteGoo.WorkflowServiceTrackingViewer {
    /// <summary>
    /// A tracking message
    /// </summary>
    [DataContract]
    public class TrackingMessage {

        /// <summary>
        /// Constructs a tracking message
        /// </summary>
        public TrackingMessage() { }

        /// <summary>
        /// Constructs a tracking message
        /// </summary>
        /// <param name="trackingRecord">The message to track</param>
        public TrackingMessage(TrackingRecord trackingRecord) {
            RecordType = trackingRecord.GetType().Name;
            InstanceId = trackingRecord.InstanceId.ToString();
            RecordNumber = trackingRecord.RecordNumber;
            EventTime = trackingRecord.EventTime;
            Content = trackingRecord.ToString().Replace("<null>", "null");
            if(trackingRecord is WorkflowInstanceRecord){
                ActivityDefinitionId = ((WorkflowInstanceRecord)trackingRecord).ActivityDefinitionId;
                State = ((WorkflowInstanceRecord)trackingRecord).State;
            }
            if (trackingRecord is ActivityScheduledRecord) {
                Activity = new ActivityMessage(((ActivityScheduledRecord)trackingRecord).Activity);
                ChildActivity = new ActivityMessage(((ActivityScheduledRecord)trackingRecord).Child);
            }
            if (trackingRecord is ActivityStateRecord) {
                Activity = new ActivityMessage(((ActivityStateRecord)trackingRecord).Activity);
                State = ((ActivityStateRecord)trackingRecord).State;
                Variables = ((ActivityStateRecord)trackingRecord).Variables.ToDictionary(kvp => kvp.Key, kvp => kvp.Value == null ? null : kvp.Value.ToString());
                Arguments = ((ActivityStateRecord)trackingRecord).Arguments.ToDictionary(kvp => kvp.Key, kvp => kvp.Value == null ? null : kvp.Value.ToString());
            }
            if (trackingRecord is CustomTrackingRecord) {
                Activity = new ActivityMessage(((CustomTrackingRecord)trackingRecord).Activity);
                Name = ((CustomTrackingRecord)trackingRecord).Name;
                Data = string.Join(", ", ((CustomTrackingRecord)trackingRecord).Data.Select(kvp => string.Format("{0} = {1}", kvp.Key, kvp.Value)));
            }
            if (trackingRecord is WorkflowInstanceUnhandledExceptionRecord) {
                Activity = new ActivityMessage(((WorkflowInstanceUnhandledExceptionRecord)trackingRecord).FaultSource);
                Data = ((WorkflowInstanceUnhandledExceptionRecord)trackingRecord).UnhandledException.ToString();
            }
        }
        /// <summary>
        /// The tracking record name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The Tracking Record Type
        /// </summary>
        [DataMember]
        public string RecordType { get; set; }

        /// <summary>
        /// The WorkflowInstance Id that produced the tracking record
        /// </summary>
        [DataMember]
        public string InstanceId { get; set; }

        /// <summary>
        /// The Record Number of the tracking record
        /// </summary>
        [DataMember]
        public long RecordNumber { get; set; }

        /// <summary>
        /// The time the tracking record was raised
        /// </summary>
        [DataMember]
        public DateTime EventTime { get; set; }

        /// <summary>
        /// The activity Definition Id
        /// </summary>
        [DataMember]
        public string ActivityDefinitionId { get; set; }

        /// <summary>
        /// The state of the workflow or activity
        /// </summary>
        [DataMember]
        public string State { get; set; }

        /// <summary>
        /// The activity which raised the tracking record
        /// </summary>
        [DataMember]
        public ActivityMessage Activity {get;set;}

        /// <summary>
        /// The child activity which is being referred to
        /// </summary>
        [DataMember]
        public ActivityMessage ChildActivity { get; set; }

        /// <summary>
        /// The Arguments that are in scope, if logged
        /// </summary>
        [DataMember]
        public IDictionary<string, string> Arguments { get; set; }

        /// <summary>
        ///  The variabled that are in scope if logged
        /// </summary>
        [DataMember]
        public IDictionary<string, string> Variables { get; set; }

        /// <summary>
        /// The specific data associated with the tracking record
        /// </summary>
        [DataMember]
        public string Data { get; set; }

        /// <summary>
        /// The content of the message
        /// </summary>
        [DataMember]
        public string Content { get; set; }


    }
}