using System.Activities.Tracking;
using System.Runtime.Serialization;

namespace PeteGoo.WorkflowServiceTrackingViewer {
    /// <summary>
    /// Activity information
    /// </summary>
    [DataContract]
    public class ActivityMessage {
        /// <summary>
        /// Constructs the activity message
        /// </summary>
        public ActivityMessage() { }

        /// <summary>
        /// Constructs the activity message
        /// </summary>
        /// <param name="activityInfo"></param>
        public ActivityMessage(ActivityInfo activityInfo) {
            if (activityInfo != null) {
                Name = activityInfo.Name;
                ActivityId = activityInfo.Id;
                ActivityInstanceId = activityInfo.InstanceId;
                TypeName = activityInfo.TypeName;
            }
        }

        /// <summary>
        /// The activity name
        /// </summary>
        [DataMember]
        public string Name{get;set;}

        /// <summary>
        /// The activity Id
        /// </summary>
        [DataMember]
        public string  ActivityId{get;set;}

        /// <summary>
        /// The activity instance id
        /// </summary>
        [DataMember]
        public string ActivityInstanceId { get; set; }

        /// <summary>
        /// The activity type name
        /// </summary>
        [DataMember]
        public string TypeName { get; set; }
    }
}