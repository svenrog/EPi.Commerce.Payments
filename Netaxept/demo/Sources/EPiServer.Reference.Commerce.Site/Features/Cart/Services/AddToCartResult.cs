﻿using System.Collections.Generic;
using System.Text;

namespace EPiServer.Reference.Commerce.Site.Features.Cart.Services
{
    public class AddToCartResult
    {
        public AddToCartResult()
        {
            ValidationMessages = new List<string>();
        }

        public bool EntriesAddedToCart { get; set; }
        public IList<string> ValidationMessages { get; private set; }

        /// <summary>
        /// Merges all warning messages into one string and makes sure it is not longer then 512 characters which is the maximum length to be used in an HttpStatusCodeResult.
        /// </summary>
        /// <returns>The validation messages represented as a single string.</returns>
        public string GetComposedValidationMessage()
        {
            var allowedMessageLength = 512;
            var composedMessage = new StringBuilder();
            foreach (var message in ValidationMessages)
            {
                var messageText = message.Length + 2 < allowedMessageLength ? message : message.Substring(allowedMessageLength);
                allowedMessageLength -= message.Length;
                composedMessage.AppendLine(messageText);

                if (allowedMessageLength <= 0)
                {
                    break;
                }
            }

            return composedMessage.ToString();
        }
    }
}