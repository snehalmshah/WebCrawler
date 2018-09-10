Web Crawler - C# Console Application to travers through a specified domain URL and generate site map
======================================================================================================
The following are the step to complie and execute the program
	1. Download code to a Windows machine 
	2. Open the *localpath*\WebCrawler.sln using Visiual Studio 2017
	3. Pres Ctrl + F5 to build and run the application
	4. A console window with message "Started crawling of https://wiprodigital.com"
	5. Wait till you see the second message "Crawling of https://wiprodigital.com completed successfully"
	6. Go to *localpath*\WebCrawler\WebCrawler\bin\Debug folder
	7. You will find three files
		* SiteMap.txt - Containing site map of https://wiprodigital.com
		* ExternalUrls.txt - Containing list of links to external websites
		* StaticContent.txt - Containing list of static item on all pages.
	
Next Version
======================================================================================================
The next iternation of the application would have following 
	1. Proper hierarchy site map
	2. Test cases of the methods
	3. Migration of some parameters to app.config file
======================================================================================================
