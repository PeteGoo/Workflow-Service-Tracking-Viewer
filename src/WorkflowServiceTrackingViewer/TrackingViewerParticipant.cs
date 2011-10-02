using System;
using System.Activities.Tracking;
using SignalR;

namespace PeteGoo.WorkflowServiceTrackingViewer {
    /// <summary>
    /// Provides a <see cref="TrackingParticipant"/> that will emit tracking information to a SignalR connection
    /// </summary>
    public class TrackingViewerParticipant : TrackingParticipant {
        /// <summary>
        /// Dispatched the tracking record
        /// </summary>
        /// <param name="record"></param>
        /// <param name="timeout"></param>
        protected override void Track(TrackingRecord record, TimeSpan timeout) {

            IConnection connection = Connection.GetConnection<TrackingViewerConnection>();
            
            if(connection != null) {
                connection.Broadcast(new TrackingMessage(record)).Wait();
            }
            
        }

        
    }
}
