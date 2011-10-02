using System.Activities.Tracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Activities.Tracking.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Web.Configuration;

namespace PeteGoo.WorkflowServiceTrackingViewer {
    /// <summary>
    /// A WCF <see cref="IServiceBehavior"/> that sets up the <see cref="TrackingViewerParticipant"/>
    /// </summary>
    public class TrackingViewerBehavior : IServiceBehavior {
        string profileName;
        
        bool enabled;

        /// <summary>
        /// Constructs a new instance of the <see cref="TrackingViewerBehavior"/>
        /// </summary>
        /// <param name="profileName">The name of the defined tracking profile</param>
        /// <param name="enabled">Whether the tracking behavior should be enabled</param>
        public TrackingViewerBehavior(string profileName, bool enabled) {
            this.profileName = profileName;
            this.enabled = enabled;
        }

        /// <summary>
        /// Applied the dispatch behavior
        /// </summary>
        /// <param name="serviceDescription">The service description</param>
        /// <param name="serviceHostBase">The service host</param>
        public virtual void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) {
            WorkflowServiceHost workflowServiceHost = serviceHostBase as WorkflowServiceHost;
            if (null != workflowServiceHost && enabled) {
                string workflowDisplayName = workflowServiceHost.Activity.DisplayName;
                TrackingProfile trackingProfile = GetProfile(profileName, workflowDisplayName);

                workflowServiceHost.WorkflowExtensions.Add(()
                        => new TrackingViewerParticipant {
                            TrackingProfile = trackingProfile
                        });

            }
        }

        TrackingProfile GetProfile(string trackingProfileName, string displayName) {
            TrackingProfile trackingProfile;
            TrackingSection trackingSection = (TrackingSection)WebConfigurationManager.GetSection("system.serviceModel/tracking");
            if (trackingSection == null) {
                return null;
            }

            if (trackingProfileName == null) {
                trackingProfileName = "";
            }

            //Find the profile with the specified profile name in the list of profile found in config
            var match = from p in new List<TrackingProfile>(trackingSection.TrackingProfiles)
                        where (p.Name == trackingProfileName) && ((p.ActivityDefinitionId == displayName) || (p.ActivityDefinitionId == "*"))
                        select p;

            if (match.Count() == 0) {
                //return an empty profile
                trackingProfile = new TrackingProfile {
                    ActivityDefinitionId = displayName
                };

            }
            else {
                trackingProfile = match.First();
            }

            return trackingProfile;
        }
        /// <summary>
        /// Adds Binding Parameters
        /// </summary>
        /// <param name="serviceDescription">The service description</param>
        /// <param name="serviceHostBase">The service host</param>
        /// <param name="endpoints"></param>
        /// <param name="bindingParameters"></param>
        public virtual void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }
        /// <summary>
        /// Validates
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public virtual void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }
    }
}
