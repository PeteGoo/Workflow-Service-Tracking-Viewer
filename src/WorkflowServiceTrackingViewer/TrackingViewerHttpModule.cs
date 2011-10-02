using System.Web;
using System.Web.Routing;
using SignalR.Routing;

namespace PeteGoo.WorkflowServiceTrackingViewer {
    /// <summary>
    /// An <see cref="IHttpModule"/> that registers routes for handling async tracking messages and serving the viewer
    /// </summary>
    public class TrackingViewerHttpModule : IHttpModule {
        private static bool applicationStarted;
        private static object applicationStartLock = new object();

        /// <summary>
        /// Cleans up any unmanaged resources
        /// </summary>
        public void Dispose() {
            
        }

        /// <summary>
        /// Initializes the http module
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context) {
            if (!applicationStarted) {
                lock (applicationStartLock) {
                    if (!applicationStarted) {
                        // this will run only once per application start
                        OnStart();
                        applicationStarted = true;
                    }
                }
            }
            // this will run on every HttpApplication initialization in the application pool
            OnInit();

        }

        private void OnInit() {
            
        }

        private void OnStart() {
            RegisterRoutes(RouteTable.Routes);
        }

        private void RegisterRoutes(RouteCollection routeCollection) {
            routeCollection.MapConnection<TrackingViewerConnection>("tracking", "tracking/{*operation}");

            Route route = new Route("trackingviewer", new TrackingViewHttpHandlerRouteHandler());
            Route route2 = new Route("trackingviewer/{*value}", new TrackingViewHttpHandlerRouteHandler());

            routeCollection.Add("trackingviewerroute", route);
            routeCollection.Add("trackingviewerroute2", route2);

        }
        
    }
}
