using Amazon.SimpleNotificationService.Model;
using Amazon.SimpleNotificationService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon.Runtime;
using static Duo_Log_Analyzer.Program;
using Amazon.SimpleNotificationService.Util;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Duo_Log_Analyzer
{
    public partial class FormSetup : Form
    {
        public FormSetup()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxDuoHostName.Text = Properties.Settings.Default.DuoHost;
            textBoxDuoiKey.Text = Properties.Settings.Default.DUOIKey;
            textBoxDuoSKey.Text = Properties.Settings.Default.DuoSKey;

            textBoxIPWhoIsAPIKey.Text = Properties.Settings.Default.IPWhoisioAPIKey;

            textBoxAWSAccessCode.Text = Properties.Settings.Default.SNSAccessKeyID;
            textBoxAWSSecretAccessKey.Text = Properties.Settings.Default.SNSSecretAccessKey;
            textBoxAWSSNSARN.Text = Properties.Settings.Default.SNSTopicARN;

            string[] IgnoreUsers = Properties.Settings.Default.IgnoreUsers.Split(char.Parse("|"));
            foreach (var item in IgnoreUsers)
            {
                if (item.Trim() != "") { listBoxIgnoreUser.Items.Add(item); }
            }

            string[] IgnoreIP = Properties.Settings.Default.IgnoreIPList.Split(char.Parse("|"));
            foreach (var item in IgnoreIP)
            {

                if (item.Trim() != "") { listBoxIgnoreIP.Items.Add(item); }
            }

            checkBoxSuspiciousLocation.Checked = Properties.Settings.Default.SuspiciousLocation;
            checkBoxUserInBypass.Checked = Properties.Settings.Default.UserInBypassMode;
            checkBoxCountryEnabled.Checked = Properties.Settings.Default.OutsideCountryEnabled;
            textBoxCountryCode.Text = Properties.Settings.Default.OutsideCountryCode;
            checkBoxUnenrolledUser.Checked = Properties.Settings.Default.UnenrolledUser;
            checkBoxRegExFilter.Checked = Properties.Settings.Default.UnenrolledUserRegExEnabled;
            textBoxRegEx.Text = Properties.Settings.Default.UnenrolledUserRegExExpression;
            checkBoxPrivateRelay.Checked = Properties.Settings.Default.iCloudPrivateRelayEnabled;
            if (Properties.Settings.Default.iCloudPrivateRelayIgnore)
            {
                radioButtonPrivateRelayIgnore.Checked = true;
            }
            else
            {
                radioButtonPrivateRelayTag.Checked = true;
            }

            if (Properties.Settings.Default.GeoAlertsEnabled)
            {
                checkBoxGeoAlerts.Checked = true;
                textBoxLatitude.Text = Properties.Settings.Default.GeoAlertsLat.ToString();
                textBoxLongitude.Text = Properties.Settings.Default.GeoAlertsLong.ToString();
                numericUpDownDistance.Value = Properties.Settings.Default.GeoAlertsDistance;

            }
            else
            {
                checkBoxGeoAlerts.Checked = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
        public void SaveSettings()

        {
            if (checkBoxGeoAlerts.Checked)
            {
                Properties.Settings.Default.GeoAlertsEnabled = true;

                Properties.Settings.Default.GeoAlertsDistance = numericUpDownDistance.Value;
                double Longitude = 0;
                double Latitude = 0;
                if (textBoxLatitude.Text.Length > 0 || textBoxLongitude.Text.Length > 0)
                {
                    if (double.TryParse(textBoxLongitude.Text, out Longitude))
                    {
                        if (Longitude >= -180.0 && Longitude <= 180.0)
                        {
                            Properties.Settings.Default.GeoAlertsLong = Longitude;
                        }
                        else { MessageBox.Show("Longitude is not in the correct format!"); }
                    }
                    else { MessageBox.Show("Longitude is not in the correct format!"); }

                    if (double.TryParse(textBoxLatitude.Text, out Latitude))
                    {
                        if (Latitude >= -90.0 && Latitude <= 90.0)
                        {
                            Properties.Settings.Default.GeoAlertsLat = Latitude;
                        }
                        else { MessageBox.Show("Latitude is not in the correct format!"); }
                    }
                    else { MessageBox.Show("Latitude is not in the correct format!"); }
                }
            }
            else { Properties.Settings.Default.GeoAlertsEnabled = false; }
            Properties.Settings.Default.DuoHost = textBoxDuoHostName.Text;
            Properties.Settings.Default.DUOIKey = textBoxDuoiKey.Text;
            Properties.Settings.Default.DuoSKey = textBoxDuoSKey.Text;

            Properties.Settings.Default.IPWhoisioAPIKey = textBoxIPWhoIsAPIKey.Text;

            Properties.Settings.Default.SNSAccessKeyID = textBoxAWSAccessCode.Text;
            Properties.Settings.Default.SNSSecretAccessKey = textBoxAWSSecretAccessKey.Text;
            Properties.Settings.Default.SNSTopicARN = textBoxAWSSNSARN.Text;


            string IgnoreIP = "";
            foreach (var item in listBoxIgnoreIP.Items)
            {
                IgnoreIP = IgnoreIP + item + "|";
            }
            Properties.Settings.Default.IgnoreIPList = IgnoreIP;

            string IgnoreUsername = "";
            foreach (var item in listBoxIgnoreUser.Items)
            {
                IgnoreUsername = IgnoreUsername + item + "|";
            }
            Properties.Settings.Default.IgnoreUsers = IgnoreUsername;

            Properties.Settings.Default.SuspiciousLocation = checkBoxSuspiciousLocation.Checked;
            Properties.Settings.Default.UserInBypassMode = checkBoxUserInBypass.Checked;
            Properties.Settings.Default.OutsideCountryEnabled = checkBoxCountryEnabled.Checked;
            Properties.Settings.Default.OutsideCountryCode = textBoxCountryCode.Text;
            Properties.Settings.Default.UnenrolledUser = checkBoxUnenrolledUser.Checked;
            Properties.Settings.Default.UnenrolledUserRegExEnabled = checkBoxRegExFilter.Checked;
            Properties.Settings.Default.UnenrolledUserRegExExpression = textBoxRegEx.Text;

            Properties.Settings.Default.iCloudPrivateRelayEnabled = checkBoxPrivateRelay.Checked;
            Properties.Settings.Default.iCloudPrivateRelayIgnore = radioButtonPrivateRelayIgnore.Checked;
            toolStripStatusLabelStatus.Text = "Settings Saved.";
            timerClearStatus.Enabled = true;

            Properties.Settings.Default.Save();

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                string pattern = textBoxRegEx.Text;
                string input = textBoxRegExTest.Text;
                Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    MessageBox.Show("Username would not generate an alert for unenrolled user.");
                }
                else
                {
                    MessageBox.Show("Username would generate an alert for unenrolled user.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(string.Format("Could not process RegEx. The error was: {0}", ex.Message));
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRegExFilter.Checked)
            {
                panelRegEx.Enabled = true;
            }
            else
            {
                panelRegEx.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxIgnoreUser.Text.Contains("|"))
            {
                MessageBox.Show("Username cannot contain | charactor.");
                return;
            }
            listBoxIgnoreUser.Items.Add(textBoxIgnoreUser.Text);
            textBoxIgnoreUser.Clear();
        }

        private void checkBoxHideDuoSKey_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHideDuoSKey.Checked)
            {
                textBoxDuoSKey.UseSystemPasswordChar = true;
            }
            else
            {
                textBoxDuoSKey.UseSystemPasswordChar = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxIgnoreIP.Text.Contains("|"))
            {
                MessageBox.Show("IP address cannot contain | charactor.");
                return;
            }
            listBoxIgnoreIP.Items.Add(textBoxIgnoreIP.Text);
            textBoxIgnoreIP.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBoxIgnoreIP.SelectedIndex == -1) { return; }

            listBoxIgnoreIP.Items.RemoveAt(listBoxIgnoreIP.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBoxIgnoreUser.SelectedIndex == -1) { return; }

            listBoxIgnoreUser.Items.RemoveAt(listBoxIgnoreUser.SelectedIndex);
        }

        private void checkBoxHideIPWHOISKey_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHideIPWHOISKey.Checked)
            {
                textBoxIPWhoIsAPIKey.UseSystemPasswordChar = true;
            }
            else
            {
                textBoxIPWhoIsAPIKey.UseSystemPasswordChar = false;
            }
        }

        private void checkBoxHideAWSSecretKey_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHideAWSSecretKey.Checked)
            {
                textBoxAWSSecretAccessKey.UseSystemPasswordChar = true;
            }
            else
            {
                textBoxAWSSecretAccessKey.UseSystemPasswordChar = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveSettings();

            try
            {
                string MessageID = Program.SendSNSMessage("Duo Alert - Test Message");
                MessageBox.Show(string.Format("Test message sent successfully with ID: \n{0}", MessageID), "Message Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

                MessageBox.Show(string.Format("Error sending SNS Message: {0}", ex.Message));
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSettings();
                bool SecurityEvent = false;
                IPWhoIS IP = new IPWhoIS();
                String IPINFO = Program.GetFormattedIOWHOINFO("", ref SecurityEvent, ref IP);
                MessageBox.Show(string.Format("Got the following return info from ipwhois.io: \n{0}", IPINFO), "IPWHOIS.IO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "IPWHOIS.IO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxUnenrolledUser_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUnenrolledUser.Checked)
            {
                checkBoxRegExFilter.Enabled = true;
                panelIgnoreIPList.Enabled = true;
            }
            else
            {
                checkBoxRegExFilter.Checked = false;
                checkBoxRegExFilter.Enabled = false;
                panelIgnoreIPList.Enabled = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                SaveSettings();
                bool SecurityEvent = false;
                IPWhoIS IP = new IPWhoIS();
                String IPINFO = Program.GetFormattedIOWHOINFO("", ref SecurityEvent, ref IP);
                textBoxLatitude.Text = IP.latitude.ToString();
                textBoxLongitude.Text = IP.longitude.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "IPWHOIS.IO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGeoAlerts.Checked) { panelGeoAlerts.Enabled = true; } else { panelGeoAlerts.Enabled = false; }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                SaveSettings();
                bool SecurityEvent = false;
                IPWhoIS IP = new IPWhoIS();
                String IPINFO = Program.GetFormattedIOWHOINFO("", ref SecurityEvent, ref IP);
                textBoxCountryCode.Text = IP.country_code;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "IPWHOIS.IO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxOutsideUS_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCountryEnabled.Checked) { panelOutsideCountry.Enabled = true; } else { panelOutsideCountry.Enabled = false; }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPrivateRelay.Checked) { panelPrivateRelay.Enabled = true; } else { panelPrivateRelay.Enabled = false; }
        }

        private void timerClearStatus_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelStatus.Text = "";
            timerClearStatus.Enabled = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (textBoxIgnoreUser.Text.Contains("|"))
            {
                MessageBox.Show("Username cannot contain | charactor.");
                return;
            }
            listBoxIgnoreUser.Items.Add(textBoxIgnoreUser.Text);
            textBoxIgnoreUser.Clear();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (listBoxIgnoreUser.SelectedIndex == -1) { return; }

            listBoxIgnoreUser.Items.RemoveAt(listBoxIgnoreUser.SelectedIndex);
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
