using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace PeteGoo.WorkflowServiceTrackingViewer {
    /// <summary>
    /// The Tracking Viewer Extension Configuration Element
    /// </summary>
    public class TrackingViewerExtensionElement : BehaviorExtensionElement {

        /// <summary>
        /// The name of the tracking profile to use
        /// </summary>
        [ConfigurationProperty("profileName", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string ProfileName {
            get { return (string)this["profileName"]; }
            set { this["profileName"] = value; }

        }

        /// <summary>
        /// Whether the tracking participant is enabled, default is true
        /// </summary>
        [ConfigurationProperty("enabled", DefaultValue = true, IsKey = false, IsRequired = false)]
        public bool Enabled {
            get {
                return (bool)this["enabled"];
            }
            set { this["enabled"] = value; }

        }

        /// <summary>
        /// The behavior type
        /// </summary>
        public override Type BehaviorType { get { return typeof(TrackingViewerBehavior); } }
        /// <summary>
        /// Creates the behavior
        /// </summary>
        /// <returns></returns>
        protected override object CreateBehavior() { return new TrackingViewerBehavior(ProfileName, Enabled); }
    }

}
