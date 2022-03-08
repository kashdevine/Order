using Polly;
using Polly.Retry;
using System.Data.Entity.Core;

namespace Order.Policies
{
    public class ServerPolicy
    {
        public AsyncRetryPolicy RetryForever { get; }
        public AsyncRetryPolicy RetryLimited { get; }

        public ServerPolicy()
        {
            RetryLimited = Policy.Handle<EntityException>().RetryAsync
                (
                    5,
                    onRetry: (exception, retryCount) =>
                        {
                            Console.Error.WriteLine(String.Format("{0}: failed to connect to db {1} time(s). Retrying...", exception, retryCount));
                        }
                );

            RetryForever = Policy.Handle<EntityException>().WaitAndRetryForeverAsync
                (
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (exception, retryCount) =>
                    {
                        Console.Error.WriteLine(String.Format("{0}: failed to connect to db {1} time(s). Retrying...", exception, retryCount));
                    }
                );
        }
    }
}
