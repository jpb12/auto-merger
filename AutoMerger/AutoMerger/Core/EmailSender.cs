using AutoMerger.Types;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace AutoMerger.Core
{
	interface IEmailSender
	{
		void SendSummaryEmail(string summary);
	}

	class EmailSender : IEmailSender
	{
		private readonly bool _sendEmails;
		private readonly EmailSettings _settings;

		public EmailSender(IConfigurationManager configManager, IConfigGetter configGetter)
		{
			_sendEmails = configManager.GetBoolValue(ConfigKey.SendEmails);
			_settings = configGetter.GetConfig().EmailSettings;
		}

		public void SendSummaryEmail(string summary)
		{
			if (!_sendEmails || _settings == null)
			{
				return;
			}

			using (var client = new SmtpClient())
			using (var message = new MailMessage())
			{
				message.From = new MailAddress(_settings.FromAddress.Value);

				AddEmailsToCollection(message.To, _settings.ToAddresses);
				AddEmailsToCollection(message.CC, _settings.CcAddresses);
				AddEmailsToCollection(message.Bcc, _settings.BccAddresses);

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
