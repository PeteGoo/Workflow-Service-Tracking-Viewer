using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using PeteGoo.WorkflowServiceTrackingViewer;
using SignalR.Routing;

namespace TestWorkflow {
    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e) {

        }

        protected void Application_BeginRequest(object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

        }

        protected void Application_Error(object sender, EventArgs e) {

        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

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