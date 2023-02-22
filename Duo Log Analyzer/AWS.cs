using Amazon.SimpleNotificationService.Model;
using Amazon.SimpleNotificationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Duo_Log_Analyzer
{
    internal class AWS
    {
        public static string SendSNSMessage(string Message, string SNSTopicARN = null)
        {
            string arn = Properties.Settings.Default.SNSTopicARN;

            if (SNSTopicARN != null) { arn = SNSTopicARN; }

            string[] arnParts = arn.Split(':');
            string region = arnParts[3];
            AmazonSimpleNotificationServiceConfig snsConfig = new AmazonSimpleNotificationServiceConfig();
            snsConfig.Timeout = TimeSpan.FromMinutes(10);

            AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient(Properties.Settings.Default.SNSAccessKeyID, Properties.Settings.Default.SNSSecretAccessKey, Amazon.RegionEndpoint.GetBySystemName(region));

            PublishRequest request = new PublishRequest
            {
                TopicArn = arn,
                Message = Message
            };
            try
            {
                PublishResponse response = snsClient.Publish(request);
                return response.MessageId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool IsArnValid(string arn)
        {
            string pattern = @"^arn:(aws|aws-cn|aws-us-gov):[a-zA-Z0-9_-]+:[0-9]+:[a-zA-Z0-9_-]+(/[\w-]+)*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(arn);
        }
    }
}
