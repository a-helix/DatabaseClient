using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseClient.Tests
{
    public class SubscriptionMog : Subscription
    {
        public Subscription subscription;
        public SubscriptionMog(Subscription sub)
        {
            subscription = sub;
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            SubscriptionMog compare = (SubscriptionMog) obj;
            if (compare.GetHashCode() != this.GetHashCode())
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            int sum = 0;
            sum += charSum(subscription.ID);
            sum += charSum(subscription.UserID);
            sum += charSum(subscription.Location);
            sum += charSum(Convert.ToString(subscription.RequestsPerHour));
            sum += charSum(Convert.ToString(subscription.Active));
            sum += charSum(subscription.Status);
            sum += charSum(Convert.ToString(subscription.CreatedAt));
            sum += charSum(Convert.ToString(subscription.ExpiredAt));
            sum += charSum(Convert.ToString(subscription.LastSent));
            return sum;


        }

        private int charSum(string input)
        {
            int sum = 0;
            char[] array = input.ToCharArray();
            foreach (char i in array)
            {
                sum += Convert.ToInt32(char.GetNumericValue(i));
            }
            return sum;
        }

        public SubscriptionMog Clone()
        {
            return (SubscriptionMog) this.MemberwiseClone();
        }
    }
}
