Web Crawler - C# Console Application to travers through a specified domain URL and generate site map
======================================================================================================
The following are the step to complie and execute the program
	* Download code to a Windows machine 
	* Open the *localpath*\WebCrawler.sln using Visiual Studio 2017
	* Pres Ctrl + F5 to build and run the application
	* A console window with message "Started crawling of https://wiprodigital.com"
	* Wait till you see the second message "Crawling of https://wiprodigital.com completed successfully"
	* Go to *localpath*\WebCrawler\WebCrawler\bin\Debug folder
	* You will find three files
		* SiteMap.txt - Containing site map of https://wiprodigital.com
		* ExternalUrls.txt - Containing list of links to external websites
		* StaticContent.txt - Containing list of static item on all pages.
	
Next Version
======================================================================================================
The next iternation of the application would have following 
	* Proper hierarchy site map
	* Test cases of the methods
	* Migration of some parameters to app.config file
======================================================================================================
