# Duo Log Analyzer
The Duo Log Analyzer pulls sign-in event logs via the DUO API and scans all the IP addresses using the ipwhois.io API. The following security concerns are recognized:

- Logon from Anonymous IPs, Proxy Servers, VPNs, Tor Nodes and Hosted servers such as AWS EC2.
- Logins outside home country.
- Distance from home site with dual geolocation providers. ipwhois.io primary and geolocaion.io for secondary. Configurable options include ignore if second geo provider is within range or provides data for both geo providers.
- User marked the logon as fraud in the mobile app.
- Failed logon with no corresponding successful logon within a configurable time.

# Additional useful features include:

- Alerts when an unenrolled users signs in.
- Alerts when a user signs in that is in bypass mode with option to filter users via Regex and list.
- Ability to filter IP addresses.
- iCloud Private Relay detection notification and ignore options.

# iCloud Private Relay
New features will pull the iCloud Private Relay egress list from Apple and gives the option via the GUI to either tag the logon event as a Private Relay logon or ignore the logon event all together. Apple claims that Private Relay Users are all verified and must have an iCloud account account to access the services plus it only works on apple browsers so abuse in not likely (Ie, script running). We recommand that you ignore logon events from Private Relay since this is most likely a valid user or have the logon event tagged for informational use.

The way the new feature works is we pull the ip egress list from Apples servers once in a 24 hour period from the following address: https://mask-api.icloud.com/egress-ip-ranges.csv. It's a large CSV files with several CDIR ip ranges. Because it's not just a simple lookup, we need to compare each IP address against the CDIR range which takes approximately 5 seconds per lookup. I will be optimizing this in the future but for now, it seems to work well. I reached out to Ipwhois.io and based on their response, I do not think they plan on adding support for iCloud Private Relay anytime soon so we will keep pulling egress lists manually until support is added. 

# Notifications

Notifications are sent via AWS SES.

An AWS account and SNS is required.


# Setup and Deployment
1. [Download](https://github.com/joef12345/Duo-Log-Analyzer/releases "Download") the released files and extract to a folder of your choice.
2. From a command prompt, execute `duo log analyzer.exe -setup`
3. The GUI will open.
4. Follow the directions [here](https://duo.com/docs/adminapi "here") to create the duo api application. The only permission that is required is  `read log files`. Enter the DUO API keys and hostname in the GUI.
5. Sign up for [ipwhois.io](https://ipwhois.io "ipwhois.io"). Make sure you open a business plan or higher, as the basic plan does not include security tags. Enter the API key in the GUI. Click the test button to confirm API connectivity.
6. Create an AWS ARN Topic and add email addresses to the subscribers. More info [here](https://docs.aws.amazon.com/sns/latest/dg/sns-getting-started.html "here"). Enter the ARN in the GUI. 
7. Create an IAM identity so the program can send SNS messages. Use the following policy to restrict the keys to only be allowed to publish messages: 

```{
"Version": "2012-10-17",
  "Id": "__DUO_Alerts",
  "Statement":[{
    "Sid":"AllowPublishToMyTopic",
    "Effect":"Allow",
    "Action":"sns:Publish",
    "Resource":"TOPIC_ARN_HERE"
  }]
}
```

8. Enter the keys in the GUI and click the test button to confirm connectivity. Follow the directions [here](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_credentials_access-keys.html#Using_CreateAccessKey "here").
8. Add any Ip addresses you would like to ignore and add them to the list. It's a good idea to add your organization's public IP to the ignore list, as it will prevent using IPWHOIS credits on known addresses. 
9. Add any users to be ignored to the list to prevent unenrolled alerts. I have found that LDAP login accounts sometimes get sent to DUO, causing several unenrolled notifications. 
10. You can create a regex filter to prevent unenrolled alerts from getting sent. For example, students normally do not have DUO enrolled in a school setting, which will cause several alerts. To prevent this, create a regex filter for example, if all your student accounts are numeric, use the following filter `[0-9]`. When creating a filter use the test button to confirm your regex expression.
11. Create a windows task to run the program every 5 minutes. More often than 5 minutes will cause DUO API rate limiting. Have the task execute `duo log analyzer.exe -run` Make sure the scheduled task runs as the same user that configured the GUI since the application settings are stored per user. 
12. For testing, you can run `duo log analyzer.exe -run -back X` where X is the number of hours to pull previously from the current time. In `-run` mode, the program will pull all the logs since the last time the program was executed.
13. If you have any problems or suggestions, please visit the discussions page here: https://github.com/joef12345/Duo-Log-Analyzer/discussions or report problems here: https://github.com/joef12345/Duo-Log-Analyzer/issues


## Upgrading From Previous Release
- Simply extract the zip file in the same directory as your previous installation and overwrite all files.  Your configuration will be automatically upgraded to the new version. 
- Run the program in `-setup` mode to take advantage of any new features.

## Checking More Than One DUO Instance
- Create a folder for each instance and run the program in `-setup` mode to create a different configuration for each DUO instance. 
- Configure task scheduler to run both executables in `-run` mode. 
