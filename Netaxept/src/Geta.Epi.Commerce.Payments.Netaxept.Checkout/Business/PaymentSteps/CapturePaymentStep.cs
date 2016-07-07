﻿using System;
using EPiServer.Logging;
using Mediachase.Commerce.Orders;

namespace Geta.Epi.Commerce.Payments.Netaxept.Checkout.Business.PaymentSteps
{
    /// <summary>
    /// Capture payment step
    /// </summary>
    public class CapturePaymentStep : PaymentStep
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(CapturePaymentStep));

        public CapturePaymentStep(Payment payment) : base(payment)
        { }

        /// <summary>
        /// Process 
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool Process(Payment payment, ref string message)
        {
            if (payment.TransactionType == "Capture")
            {
                var orderForm = payment.Parent;
                
                var amount = PaymentStepHelper.GetAmount(orderForm.Total);

                try
                {
                    var result = this.Client.Capture(payment.ProviderTransactionID, amount);
                    if (result.ErrorOccurred)
                    {
                        message = result.ErrorMessage;
                        AddNote(orderForm, "Payment Captured - Failed", "Error: " + result.ErrorMessage);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                    message = ex.Message;
                    AddNote(orderForm, "Payment Captured - Failed", "Error: " + ex.Message, true);
                    return false;
                }

                AddNote(orderForm, "Payment - Captured", "Payment - Amount is captured");

                return true;
            }
            else if (Successor != null)
            {
                return Successor.Process(payment, ref message);
            }
            return false;
        }
    }
}
