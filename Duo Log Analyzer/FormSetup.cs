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
            checkBoxOutsideUS.Checked = Properties.Settings.Default.OutsideUS;
            checkBoxUnenrolledUser.Checked = Properties.Settings.Default.UnenrolledUser;
            checkBoxRegExFilter.Checked = Properties.Settings.Default.UnenrolledUserRegExEnabled;
            textBoxRegEx.Text = Properties.Settings.Default.UnenrolledUserRegExExpression;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveSettings();
            MessageBox.Show("Settings Saved.", "Duo Alerts");
        }
        public void SaveSettings()
        {
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
            Properties.Settings.Default.OutsideUS = checkBoxOutsideUS.Checked;
            Properties.Settings.Default.UnenrolledUser = checkBoxUnenrolledUser.Checked;
            Properties.Settings.Default.UnenrolledUserRegExEnabled = checkBoxRegExFilter.Checked;
            Properties.Settings.Default.UnenrolledUserRegExExpression = textBoxRegEx.Text;
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
                String IPINFO = Program.GetFormattedIOWHOINFO("8.8.8.8", ref SecurityEvent, ref IP);
                MessageBox.Show(string.Format("Got the following return info from ipwhois.io: \n{0}", IPINFO), "IPWHOIS.IO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "IPWHOIS.IO",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void checkBoxUnenrolledUser_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUnenrolledUser.Checked)
            {
                checkBoxRegExFilter.Enabled = true;
            }
            else
            {
                checkBoxRegExFilter.Checked = false;
                checkBoxRegExFilter.Enabled = false;
            }
        }
    }
}
