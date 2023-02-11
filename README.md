# Duo Log Analyzer
The Duo Log Analyzer pulls sign-in event logs via the DUO API and scans all the IP addresses using the ipwhois.io API. The following security concerns are recognized:

- anonymous 
- proxy 
- VPN 
- tor 
- hosting 
- Logins outside the US

# Aditional useful features include:

- Reporting on users who logon that is unenrolled.
- Reporting on users that are in bypass mode.

# Notifications

Notifications are sent via AWS SES.

An AWS account and SNS is required.


# Setup and Deployment
1. [Download](https://github.com/joef12345/Duo-Log-Analyzer/releases/tag/V1.0.0.0 "Download") the released files and extract to a folder of your choice.
2. From a command prompt, execute `duo log analyzer.exe -setup`
3. The GUI will open.
4. Follow the directions [here](https://duo.com/docs/adminapi "here") to create the dup api application. The only permission that is required is  `read log files`. Enter the DUO API keys and hostname in the GUO.
5. Sign up for [ipwhois.io](https://ipwhois.io "ipwhois.io"). Make sure you open a business plan or higher, as the basic plan does not include security tags. Enter the API key in the GUI. Click the test button to confirm API connectivity.
6. Create an AWS ARN Topic and add email addresses to the subscribers. More info [here](https://docs.aws.amazon.com/sns/latest/dg/sns-getting-started.html "here"). Enter the ARN in the GUI. 
7. Create an IAM identity so the program can send SNS messages. Use the following policy to restrict the keys to only be allowed to publish messages: `{
"Version": "2012-10-17",
  "Id": "__DUO_Alerts",
  "Statement":[{
    "Sid":"AllowPublishToMyTopic",
    "Effect":"Allow",
    "Action":"sns:Publish",
    "Resource":"TOPIC_ARN_HERE"
  }]
}
`
Enter the keys in the GUI and click the test button to confirm connectivity. Follow the directions [here](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_credentials_access-keys.html#Using_CreateAccessKey "here").
8. Add any Ip addresses you would like to ignore and add them to the list. Please leave 0.0.0.0 in, as applications that do not have an IP address will report with 0.0.0.0 like RDP. It's a good idea to add your organization's public IP to the ignore list, as it will prevent using IPWHOIS credits on known addresses. 
9. Add any users to be ignored to the list to prevent unenrolled alerts. I have found that LDAP login accounts sometimes get set to DUO, causing several unenrolled notifications. 
10. You can create a regex filter to prevent unenrolled alerts from getting sent. For example, students normally do not have DUO enrolled in a school setting, which will cause several alerts. To prevent this, create a regex filter for example, if all your student accounts are numeric, use the following filter `[0-9]`. Where creating a filter use the test button to confirm your regex expression.
11. Create a windows task to run the program every 5 minutes. Sooner than 5 minutes will cause DUO API rate limiting. Run `duo log analyzer.exe -run` 
12. for testing, you can run `duo log analyzer.exe -run -last7days` to pull the logs for the last 7 days. In `-run` mode, the program will pull all the logs since the last pull.
