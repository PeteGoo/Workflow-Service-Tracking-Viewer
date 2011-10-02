************************************
* Workflow Service Tracking Viewer *
************************************

Copyright Peter Goodman 2011

http://blog.petegoo.com

After installing the package, the following changes are made

- A reference to WorkflowServiceTrackingViewer has been added
- A tracking participant is registered in the web.config
- A HttpModule is registered in the web.config to allow the trackingviewer url

To access the tracking information visit the /trackingviewer relative URL on your web application
e.g. if your workflow service is hosted at http://localhost:2334/Service1.xamlx then you can get to 
the tracking viewer at http://localhost:2334/trackingviewer


Known Issues
=========================================

1. After applying and clearing a filter the grid is no longer the full length of the page.

2. After a while the page will slow down, clear the grid to get the performance back

3. When hosting in IIS on Win 7 the maximum connections to the tracking viewer is 2. 
   This seems to be related to the maximum connection for IIS as it affects any SignalR project

