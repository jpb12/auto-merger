using AutoMerger.Types;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace AutoMerger.Core
{
	interface IEmailSender
	{
		void SendSummaryEmail(string summary, EmailSettings settings);
	}

	class EmailSender : IEmailSender
	{
		private readonly bool _sendEmails;

		public EmailSender(IConfigurationManager configManager)
		{
			_sendEmails = configManager.GetBoolValue(ConfigKey.SendEmails);
		}

		public void SendSummaryEmail(string summary, EmailSettings settings)
		{
			if (!_sendEmails || settings == null)
			{
				return;
			}

			using (var client = new SmtpClient())
			using (var message = new MailMessage())
			{
				message.From = new MailAddress(settings.FromAddress.Value);

				AddEmailsToCollection(message.To, settings.ToAddresses);
				AddEmailsToCollection(message.CC, settings.CcAddresses);
				AddEmailsToCollection(message.Bcc, settings.BccAddresses);

				message.Subject = "AutoMerge Summary - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
				message.Body = summary;

				client.Send(message);
			}
		}

		private void AddEmailsToCollection(MailAddressCollection collection, IEnumerable<Email> emails)
		{
			if (emails == null)
			{
				return;
			}

			foreach (var email in emails)
			{
				collection.Add(new MailAddress(email.Value));
			}
		}
	}
}
