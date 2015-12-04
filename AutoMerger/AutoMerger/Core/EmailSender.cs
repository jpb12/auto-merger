using AutoMerger.Results;
using AutoMerger.Shared.Core;
using AutoMerger.Shared.Types;
using AutoMerger.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace AutoMerger.Core
{
	interface IEmailSender
	{
		void SendSummaryEmail(string summary);
		void SendIndividualMergeEmail(Merge merge, MergeResult result);
	}

	class EmailSender : IEmailSender
	{
		private readonly bool _sendEmails;
		private readonly EmailSettings<SummaryEmail> _settings;

		public EmailSender(IConfigurationManager<ConfigKey> configManager, IConfigGetter configGetter)
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

			if (_settings.ToAddresses.Count + _settings.CcAddresses.Count + _settings.BccAddresses.Count == 0)
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

		public void SendIndividualMergeEmail(Merge merge, MergeResult result)
		{
			if (!_sendEmails || merge.EmailSettings == null)
			{
				return;
			}

			if (merge.EmailSettings.FromAddress == null && (_settings == null || !_settings.FromAddress.InheritInMergeEmails))
			{
				throw new InvalidOperationException("No from email address could be configured or inherited");
			}

			using (var client = new SmtpClient())
			using (var message = new MailMessage())
			{
				message.From = new MailAddress(
					merge.EmailSettings.FromAddress != null
						? merge.EmailSettings.FromAddress.Value 
						: _settings.FromAddress.Value);

				AddEmailsToCollection(message.To, merge.EmailSettings.ToAddresses);
				AddEmailsToCollection(message.CC, merge.EmailSettings.CcAddresses);
				AddEmailsToCollection(message.Bcc, merge.EmailSettings.BccAddresses);

				if (_settings != null)
				{
					AddEmailsToCollection(message.To, _settings.ToAddresses.Where(a => a.InheritInMergeEmails));
					AddEmailsToCollection(message.CC, _settings.CcAddresses.Where(a => a.InheritInMergeEmails));
					AddEmailsToCollection(message.Bcc, _settings.BccAddresses.Where(a => a.InheritInMergeEmails));
				}

				message.Subject = string.Format(
					"{0} merge {1} to {2} - {3}",
					result.Success ? "Successful" : "Failed",
					merge.Parent,
					merge.Child,
					DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
				message.Body = result.Message;

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
