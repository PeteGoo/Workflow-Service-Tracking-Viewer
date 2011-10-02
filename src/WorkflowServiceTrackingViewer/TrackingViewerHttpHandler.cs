using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace PeteGoo.WorkflowServiceTrackingViewer {
    /// <summary>
    /// An <see cref="IHttpHandler">Http Handler</see> that renders an html page with the tracking viewer and its associated resources
    /// </summary>
    public class TrackingViewerHttpHandler : IHttpHandler {
        /// <summary>
        /// Determines if the http handler can be re-used
        /// </summary>
        public bool IsReusable {
            get { return true; }
        }

        /// <summary>
        /// Processes the specified request
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context) {
            
            if (context.Request.FilePath.ToLower().EndsWith("trackingviewer") || context.Request.FilePath.ToLower().EndsWith("trackingviewer/")) {
                Stream resourceStream = typeof(TrackingViewerHttpHandler).Assembly.GetManifestResourceStream("PeteGoo.WorkflowServiceTrackingViewer.TrackingViewer.htm");
                StreamReader sr = new StreamReader(resourceStream);
                string htmlContent = sr.ReadToEnd();
                htmlContent = htmlContent.Replace("{urlprefix}", context.Request.FilePath.EndsWith("/") ? "" : "trackingviewer/");
                htmlContent = htmlContent.Replace("{parentprefix}", context.Request.FilePath.EndsWith("/") ? "../" : "");
                context.Response.Write(htmlContent);
            }
            else if (context.Request.FilePath.ToLower().Contains("trackingviewer/")) {
                string resourcePath = context.Request.FilePath.Substring(context.Request.FilePath.ToLower().IndexOf("trackingviewer/") + 15);
                resourcePath = string.Format("{0}.{1}", "PeteGoo.WorkflowServiceTrackingViewer", resourcePath.Replace("/", "."));
                if (typeof(TrackingViewerHttpHandler).Assembly.GetManifestResourceNames().Contains(resourcePath, StringComparer.InvariantCultureIgnoreCase)) {
                    switch (resourcePath.Split('.').Last()) { 
                        case "css":
                            context.Response.ContentType = "text/css";
                            break;
                        case "js":
                            context.Response.ContentType = "application/x-javascript";
                            break;
                        case "xml":
                            context.Response.ContentType = "text/xml";
                            break;
                        case "png":
                            context.Response.ContentType = "image/png";
                            break;
                        default:
                            context.Response.ContentType = "text/css";
                            break;

                    }
                    Stream resourceStream = typeof(TrackingViewerHttpModule).Assembly.GetManifestResourceStream(resourcePath);
                    byte[] bytes = new Byte[resourceStream.Length];
                    resourceStream.Read(bytes, 0, (int)resourceStream.Length);
                    context.Response.BinaryWrite(bytes);
                    context.Response.Flush();
                }
            }
        }

       
    }

    /// <summary>
    /// An <see cref="IRouteHandler"/> that routes to the <see cref="TrackingViewerHttpHandler"/>
    /// </summary>
    public class TrackingViewHttpHandlerRouteHandler : IRouteHandler {
        /// <summary>
        /// Gets the Http Handler
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext) {
            return new TrackingViewerHttpHandler();
        }
    }
}
