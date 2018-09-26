namespace Postogram.InstagramClient.Actors
{
    public class PostContentMessage
    {
        public Content Content { get; }
        public InstagramProfile Profile { get; }
        public  int Number { get; }

        public PostContentMessage(Content content, InstagramProfile profile, int number)
        {
            Content = content;
            Profile = profile;
            Number = number;
        }

        public PostContentMessage WithNumber(int number) => new PostContentMessage(Content, Profile, number);
    }

    public abstract class PostContentResult
    {
        public Content Content { get; }
        public int TaskNumber { get; }
        public int ThreadId { get; }

        public PostContentResult(Content content, int taskNumber, int threadId)
        {
            Content = content;
            TaskNumber = taskNumber;
            ThreadId = threadId;
        }
    }

    public class PostContentResultSuccess : PostContentResult
    {
        public PostContentResultSuccess(Content content, int taskNumber, int threadId)
            : base(content, taskNumber, threadId)
        {
        }
    }

    public class PostContentResultFail : PostContentResult
    {
        public  string Reason { get; }

        public PostContentResultFail(Content content, int taskNumber, int threadId, string reason)
            : base(content, taskNumber, threadId)
        {
            Reason = reason;
        }
    }
}
